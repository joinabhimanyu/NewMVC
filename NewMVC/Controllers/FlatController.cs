using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewMVC.Controllers
{
    public class FlatController : Controller
    {
        //
        // GET: /Flat/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomerAction()
        {
            return View("CustomerAngularView");
        }

        public ActionResult BootstrapMaterialDesign()
        {
            return View("BootstrapMaterialDesign");
        }

        public ActionResult Materialize()
        {
            return View("PaperElements");
        }

    }
}
