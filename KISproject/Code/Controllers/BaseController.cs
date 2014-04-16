using System;
using System.Reflection;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace KISproject.Code.Controllers
{
    // базовый контроллер, родитель для контроллеров отделов
    // реализует механику работы с БД
    abstract class BaseController
    {
        public string SAME = "";

        private MySqlConnection getConnection()
        {
            MySqlConnectionStringBuilder con_string = new MySqlConnectionStringBuilder();
            con_string.Server = "localhost";
            con_string.Port = 3307;
            con_string.Database = "kis_cinema_chain";
            con_string.UserID = "owner";
            con_string.Password = "54321";
            return new MySqlConnection(con_string.ToString());
        }

        public List<string> getPropStrings(object obj)
        {
            List<string> propsList = new List<string>();
            foreach (PropertyInfo prop in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public |
                        BindingFlags.NonPublic))
            {
                object propValue = prop.GetValue(obj, null);
                if (propValue == null) propsList.Add(null);
                else propsList.Add(propValue.ToString());
            }
            return propsList;
        }

        protected Dictionary<string, List<string>> dic;

        public Dictionary<string, List<string>> getDatabaseDictionary()
        {
            if (dic == null)
            {
                dic = new Dictionary<string, List<string>>();

                string query = "SELECT TABLE_NAME, COLUMN_NAME FROM information_schema.COLUMNS WHERE TABLE_SCHEMA='kis_cinema_chain'";
                MySqlConnection con = getConnection();
                MySqlCommand myCommand = new MySqlCommand(query, con);
                con.Open();

                MySqlDataReader dataReader = myCommand.ExecuteReader();

                string prev_table_name = null;
                List<string> column_names = new List<string>();

                while (dataReader.Read())
                {
                    if (dataReader["COLUMN_NAME"].ToString().Equals("id")) continue;

                    string cur_table_name = dataReader["TABLE_NAME"].ToString();
                    if (prev_table_name == null) prev_table_name = cur_table_name;

                    if (!cur_table_name.Equals(prev_table_name))
                    {
                        dic.Add(prev_table_name, column_names);
                        column_names = new List<string>();
                    }

                    column_names.Add(dataReader["COLUMN_NAME"].ToString());
                    prev_table_name = cur_table_name;
                }

                con.Close();
            }

            return dic;
        }

        protected DataTable select(string query)
        {
            DataTable datatable = new DataTable();

            MySqlConnection con = getConnection();
            con.Open();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = new MySqlCommand(query, con);
            adapter.Fill(datatable);

            con.Close();
            return datatable;
        }

        protected string insert(object obj, string tableName)
        {
            List<string> propVals = getPropStrings(obj);

            string query = "null";
            for (int i = 0; i < propVals.Count; i++)
            {
                if (propVals[i] == null) query += ", null";
                else query += ", '" + propVals[i] + "'";
            }
            query = "INSERT INTO " + tableName + " VALUES (" + query + "); SELECT last_insert_id();";

            MySqlConnection con = getConnection();
            MySqlCommand myCommand = new MySqlCommand(query, con);
            con.Open();
            string id = myCommand.ExecuteScalar().ToString();
            con.Close();

            return id;
        }

        protected void delete(string id, string tableName)
        {
            string query = "DELETE FROM " + tableName + " WHERE id=" + id;

            MySqlConnection con = getConnection();
            MySqlCommand myCommand = new MySqlCommand(query, con);
            con.Open();
            myCommand.ExecuteNonQuery();
            con.Close();
        }

        protected void update(string id, object obj, string tableName)
        {
            List<string> propVals = getPropStrings(obj);
            dic = getDatabaseDictionary();

            tableName = tableName.ToLower();
            List<string> columns = dic[tableName];

            string query = "";
            for (int i = 0; i < columns.Count; i++)
            {
                if (propVals[i] == null) query += columns[i] + "=null, ";
                else if (propVals[i] == SAME) continue;
                else query += columns[i] + "='" + propVals[i] + "', ";
            }
            query = query.Substring(0, query.Length - 2);
            query = "UPDATE " + tableName + " SET " + query + " WHERE id=" + id;

            MySqlConnection con = getConnection();
            MySqlCommand myCommand = new MySqlCommand(query, con);
            con.Open();
            myCommand.ExecuteNonQuery();
            con.Close();
        }

    }
}