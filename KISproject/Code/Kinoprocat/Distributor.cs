using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KISproject.Code.Kinoprocat
{
    public class Distributor
    {
        public string Name { get; set; }
        public int Contacts_id { get; set; }

        public Distributor() {}

        public Distributor(string name, int contacts_id)
        {
            Name = name;
            Contacts_id = contacts_id;
        }

        public Distributor(string name)
        {
            Name = name;
        }
    }
}