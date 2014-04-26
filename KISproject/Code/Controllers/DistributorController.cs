using System;
using System.Data;
using KISproject.Code.Kinoprocat;

namespace Controllers
{
    public class DistributorController : BaseController
    {
        // Возвращает id добавленного элемента.
        // В противном случае -1.
        public int addDistributor(ExtDistributor extDistributor)
        {
            int distributor_id;
            try
            {
                extDistributor.Distributor.Contacts_id = insert(extDistributor.Contact, "Contacts");
                distributor_id = insert(extDistributor.Distributor,
                    "Distributors");
            }
            catch (Exception)
            {
                distributor_id = -1;
            }

            return distributor_id;
        }

        public string removeDistributor(ExtDistributor extDistributor)
        {
            string message;
            try
            {
                bool result = hasAnyMovieOfDistributor(extDistributor.Distributor_id);

                // Если у дистрибьютора нет фильмов то
                if (!result)
                {
                    delete(extDistributor.Distributor_id, "Distributors");
                    delete(extDistributor.Distributor.Contacts_id,
                        "Contacts");

                    message = "success";
                }
                else
                {
                    message = "Нельзя удалить, т.к на данного дистрибьютора " +
                        "ссылаются фильмы";
                }
            }
            catch (Exception)
            {
                message = "Ошибка доступа к БД";
            }

            return message;
        }

        public bool updateDistributor(ExtDistributor extDistributor)
        {
            bool result;
            try
            {
                update(extDistributor.Distributor.Contacts_id,
                extDistributor.Contact, "Contacts");

                update(extDistributor.Distributor_id,
                    extDistributor.Distributor, "Distributors");
                
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool hasAnyMovieOfDistributor(int distributor_id)
        {
            DataTable movie = select("SELECT id FROM Movies WHERE Distributors_id = " + 
                distributor_id + " LIMIT 1");

            if (movie.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}