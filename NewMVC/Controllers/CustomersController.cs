using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewMVC.Models;

namespace NewMVC.Controllers
{
    public class CustomersController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();
        public CustomersController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public ActionResult Index()
        {
            var customers = db.Customers.ToList<Customer>();
            return View(customers);
        }

        public ActionResult GetOrderDetails(int id)
        {
            var order_details = db.Order_Details.Where(m => m.Order_ID == id);
            if (order_details != null)
            {
                return View(order_details.First());
            }
            else
            {
                return new HttpNotFoundResult("No matching records found");
            }
        }

        public ActionResult OrdersList(string customerID)
        {
            var order = db.Orders.Where(m => m.Customer_ID == customerID.Trim());
            if (order != null)
            {
                return View(order.ToList<NewMVC.Models.Order>());
            }
            else
            {
                return new HttpNotFoundResult("No matching records found");
            }
        }

        [HttpPost]
        public ActionResult SearchCustomerByName(string name)
        {
            var found_customer = db.Customers.Where(m => m.Contact_Name.Contains(name.Trim()));
            if (found_customer != null)
            {
                return Json(found_customer, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpNotFoundResult("No matching records found");
            }
        }

        [HttpPost]
        public ActionResult SearchCustomerByCompany(string company)
        {
            var found_customer = db.Customers.Where(m => m.Company_Name == company.Trim());
            if (found_customer != null)
            {
                return Json(found_customer, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpNotFoundResult("No matching records found");
            }
        }

        [HttpPost]
        public ActionResult SearchCustomerByCity(string city)
        {
            var found_customer = db.Customers.Where(m => m.City == city.Trim());
            if (found_customer != null)
            {
                return Json(found_customer, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpNotFoundResult("No matching records found");
            }

        }

        [HttpPost]
        public ActionResult SearchCustomerByCountry(string country)
        {
            var found_customer = db.Customers.Where(m => m.Country == country.Trim());
            if (found_customer != null)
            {
                return Json(found_customer, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpNotFoundResult("No matching records found");
            }
        }

    }
}
