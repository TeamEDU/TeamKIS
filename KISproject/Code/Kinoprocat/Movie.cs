using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KISproject.Code.Kinoprocat
{
    /* Атрибут сериализации. 
     * Дает возможность преобразовать элемент в поток байт, т.е
     * он может быть сохранен в состоянии представления.
     * Необходим для состояния представления ViewState
     */
    [Serializable]
    public class Movie
    {
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public string Genre { get; set; }
        public TimeSpan Duration { get; set; }
        public string Actors { get; set; }
        public string Age { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        public string Distributors_id { get; set; }
        public string MoviesAds_id { get; set; }

        public Movie() {}

        public Movie(string title, string releaseDate, string genre,
            TimeSpan duration, string actors, string age,
            string country, string director, string distributors_id)
        {
            Title = title;
            ReleaseDate = releaseDate;
            Genre = genre;
            Duration = duration;
            Actors = actors;
            Age = age;
            Country = country;
            Director = director;
            Distributors_id = distributors_id;
        }
    }
}