using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewMVC.Models
{
    public class CustomerDetails
    {
        public string customer_type { get; set; }
        public string salutation { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string customerName { get; set; }
        public string dob { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public string emailID { get; set; }
        public string tinNo { get; set; }
        public string mobileNo { get; set; }
        public string landlineNo { get; set; }
    }
}