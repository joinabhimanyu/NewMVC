using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace NewMVC.Models
{
    public class CustomerDB : DbContext
    {
        public CustomerDB()
            : base("name=DefaultConnection")
        { }

        public virtual DbSet<Customer> Customers { get; set; }
    }
}