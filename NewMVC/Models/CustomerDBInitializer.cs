using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace NewMVC.Models
{
    public class CustomerDBInitializer : DropCreateDatabaseAlways<NewMVC.Models.CustomerDB>
    {
        protected override void Seed(CustomerDB context)
        {
            context.Customers.Add(new Customer { CustomerID=1, FirstName="abhi", LastName="chak", AddressLine1="Ranchi", AddressLine2="Main", City="Ranchi", State_Province="JHK", ZIP_Postal_Code="834001", Region_Country="IND" });
            base.Seed(context);
        }
    }
}