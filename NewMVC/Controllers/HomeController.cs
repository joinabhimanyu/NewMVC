using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.Script.Services;
using DbUtility;
using System.Data;
using System.Text;
using System.IO;
using System.Net;
using NewMVC.Models;


namespace NewMVC.Controllers
{
    public class HomeController : Controller
    {
        private List<StateMaster> states = null;

        public HomeController()
        {
            DbUtility.DataObjectClass.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["con"].ToString().Trim();
            states = new List<StateMaster>();

            StateMaster state = null;
            var qstring = "select * from workflow_state_master";
            DataObjectClass obj_dataobject = new DataObjectClass("Password=life;User ID=life;Data Source=conf_146; max pool size=25");
            DataTable dt = null;
            dt = obj_dataobject.getSQLDataTable(qstring);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    state = new StateMaster();
                    state.StateID = (dt.Rows[i][0] == System.DBNull.Value) ? "" : dt.Rows[i][0].ToString().Trim();
                    state.StateName = (dt.Rows[i][1] == System.DBNull.Value) ? "" : dt.Rows[i][1].ToString().Trim();
                    state.StateDispName = (dt.Rows[i][2] == System.DBNull.Value) ? "" : dt.Rows[i][2].ToString().Trim();
                    states.Add(state);
                    state = null;
                }
            }

        }

        public ActionResult Index()
        {
            var display_states = states.OrderBy(state => state.StateID).Select(state => state);
            return View(display_states);
        }

        public ActionResult Create()
        {
            StateMaster newstate = new StateMaster();
            return PartialView("_CreateState", newstate);
        }

        [HttpPost]
        public ActionResult Create(StateMaster newstate)
        {
            var stateid = newstate.StateID;
            var statename = newstate.StateName;
            var dispname = newstate.StateDispName;
            states.Add(newstate);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string StateID)
        {
            var searched_state = from state in states where state.StateID == StateID.Trim() select state;
            if (searched_state.Count() > 0)
            {
                return View((StateMaster)searched_state.First());    
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No matching records found");
            }
            
        }

        [HttpPost]
        public ActionResult Back()
        {
            return RedirectToAction("Index");
        }

        public ActionResult GetStateByName(string state_name)
        {
            StateMaster state = null;
            DataObjectClass obj_dataobject = new DataObjectClass("Password=life;User ID=life;Data Source=conf_146; max pool size=25");
            DataRow dr = null;
            try
            {
                string qstring = string.Format("select * from workflow_state_master where txt_state_name = '{0}'", state_name.ToString().Trim());
                dr = obj_dataobject.getSQLDataRow(qstring.Trim());
                if (dr != null)
                {
                    state = new StateMaster();
                    state.StateID = (dr[0] == System.DBNull.Value) ? "" : dr[0].ToString().Trim();
                    state.StateName = (dr[1] == System.DBNull.Value) ? "" : dr[1].ToString().Trim();
                    state.StateDispName = (dr[2] == System.DBNull.Value) ? "" : dr[2].ToString().Trim();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Json(state, JsonRequestBehavior.AllowGet);
        }

    }
}
