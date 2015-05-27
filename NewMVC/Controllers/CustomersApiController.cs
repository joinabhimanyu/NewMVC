using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Data.OracleClient;
using System.Data.Entity;
using NewMVC.Models;
using System.Web.Http;

namespace NewMVC.Controllers
{
    public class Cust
    {
        public string Customer_ID { get; set; }
        public string Company_Name { get; set; }
        public string Contact_Name { get; set; }
    }

    public class CustomersApiController : ApiController
    {
        private List<Cust> new_customers = null;
        //private NorthwindEntities db = new NorthwindEntities();

        public CustomersApiController()
        {
            
            if (new_customers == null)
            {
                new_customers = new List<Cust>();
                new_customers.Add(new Cust { Customer_ID = "cust1", Company_Name = "Company 1", Contact_Name = "Contact 1" });
                new_customers.Add(new Cust { Customer_ID = "cust2", Company_Name = "Company 2", Contact_Name = "Contact 2" });
                new_customers.Add(new Cust { Customer_ID = "cust3", Company_Name = "Company 3", Contact_Name = "Contact 3" });
                new_customers.Add(new Cust { Customer_ID = "cust4", Company_Name = "Company 4", Contact_Name = "Contact 4" });
                new_customers.Add(new Cust { Customer_ID = "cust5", Company_Name = "Company 5", Contact_Name = "Contact 5" });
                new_customers.Add(new Cust { Customer_ID = "cust6", Company_Name = "Company 6", Contact_Name = "Contact 6" });
            }

            //db.Configuration.ProxyCreationEnabled = false;
        }

        // GET api/<controller>
        public IEnumerable<Cust> GetAllCustomers()
        {
            var customers = new_customers.ToList<Cust>();
            return customers;

            //var customers = db.Customers.ToList<Customer>();
            //return customers;
        }

        // GET api/<controller>/5
        public Cust GetCustomerByID(string customerID)
        {
            var customer = new_customers.Where(m => m.Customer_ID == customerID.Trim());
            return customer.First();

            //var customer = db.Customers.Where(m => m.Customer_ID == customerID.Trim());
            //return customer.First<Customer>();
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}