using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KISproject.Code.Kinoprocat
{
    // Extention Distributor - класс расширяющий классы 
    // по умолчанию: Distributor и Contact.

    /* Атрибут сериализации. 
     * Дает возможность преобразовать элемент в поток байт, т.е
     * он может быть сохранен в состоянии представления.
     * Необходим для состояния представления ViewState
     */
    [Serializable]
    public class ExtDistributor
    {
        // Идентификатор дистрибьютера
        public  int Distributor_id { get; set; }
               
        public Contact Contact { get; set; }
        public Distributor Distributor { get; set; }

        public ExtDistributor()
        {
            Contact = new Contact();
            Distributor = new Distributor();
        }

        public ExtDistributor(Distributor distributor, Contact contact)
        {
            Distributor = distributor;
            Contact = contact;
        }
    }
}