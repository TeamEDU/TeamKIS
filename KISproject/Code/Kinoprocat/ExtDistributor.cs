using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KISproject.Code.Kinoprocat
{
    // Extentron Distributor - класс расширяющий классы 
    // по умолчанию: Distributor и Contact.
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