using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewMVC.Models
{
    public class AuthenticationData
    {
        public string AuthenticationToken { get; set; }
        public string AuthenticationSessionID { get; set; }
    }
}