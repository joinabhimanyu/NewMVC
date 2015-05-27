using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewMVC.Models
{
    public class WCFResult
    {
        public string status { get; set; }
        public object response { get; set; }
        public List<string> errors { get; set; }

        public WCFResult()
        {
            errors = new List<string>();
        }

    }
    
}