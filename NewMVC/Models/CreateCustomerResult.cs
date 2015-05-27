using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewMVC.Models
{
    public class CreateCustomerResult
    {
        public string customerID { get; set; }
        public string errCode { get; set; }
        public string errMsg { get; set; }
    }
}