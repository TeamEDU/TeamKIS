using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KISproject.Code.Kinoprocat
{
    // Extention Movie - класс расширяющий класс 
    // по умолчанию: Movie.

    /* Атрибут сериализации. 
     * Дает возможность преобразовать элемент в поток байт, т.е
     * он может быть сохранен в состоянии представления.
     * Необходим для состояния представления ViewState
     */
    [Serializable]
    public class ExtMovie
    {
        public int Movie_id { get; set; }

        public Movie Movie { get; set; }
        public Distributor Distributor { get; set; }

        public ExtMovie()
        {
            Movie = new Movie();
            Distributor = new Distributor();
        }

        public ExtMovie(Movie movie, Distributor distributor)
        {
            Movie = movie;
            Distributor = distributor;
        }
    }
}