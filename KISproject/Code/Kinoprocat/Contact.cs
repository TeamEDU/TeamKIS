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
    public class Contact
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public Contact() {}

        public Contact(string phone, string email, string address)
        {
            Phone = phone;
            Email = email;
            Address = address;
        }
    }
}