using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using KISproject.Code.Kinoprocat;


namespace Controllers
{
    public class MovieController : BaseController
    {
        // Возвращает id добавленного элемента.
        // В противном случае -1.
        public int addMovie(ExtMovie extMovie)
        {
            int movie_id;
            try
            {
                movie_id = insert(extMovie.Movie, "Movies");
            }
            catch (Exception)
            {
                movie_id = -1;
            }

            return movie_id;
        }

        public string removeMovie(ExtMovie extMovie)
        {
            string message;
            try
            {
                bool result = hasAnyCinemaShowsOfMovie(extMovie.Movie_id);

                if (!result)
                {
                    delete(extMovie.Movie_id, "Movies");
                    message = "success";
                }
                else
                {
                    message = "Нельзя удалить, т.к на данный фильм " +
                        "продаются билеты";
                }
            }
            catch (Exception)
            {
                message = "Ошибка доступа к БД";
            }

            return message;
        }


        public bool updateMovie(ExtMovie extMovie)
        {
            bool result;

            try
            {
                update(extMovie.Movie_id, extMovie.Movie, "Movies");
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool hasAnyCinemaShowsOfMovie(int movie_id)
        {
            DataTable cinemaShow = select("SELECT id FROM CinemaShows WHERE Movies_id = " +
                movie_id + " AND ShowDateTime > '" + 
                DateTime.Now.Date.ToString("yyyy-mm-dd") +
                "' LIMIT 1");

            if (cinemaShow.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}