using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using KISproject.Code.Kinoprocat;

namespace Controllers
{
    public class DistributorController : BaseController
    {
        // Возвращает id добавленного элемента.
        // В противном случае -1.
        public int insert(ExtDistributor extDistributor)
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

        public bool delete(ExtDistributor extDistributor)
        {
            bool result;
            try
            {
                delete(extDistributor.Distributor_id, "Distributors");
                delete(extDistributor.Distributor.Contacts_id,
                    "Contacts");
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool update(ExtDistributor extDistributor)
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
    }
}