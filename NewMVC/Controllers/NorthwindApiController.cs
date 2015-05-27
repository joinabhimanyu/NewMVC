using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DbUtility;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OracleClient;
using System.Data.Entity;
using NewMVC.Models;

namespace NewMVC.Controllers
{

    public class NorthwindApiController : ApiController
    {
        private NorthwindEntities db = new NorthwindEntities();

        //WCF Proxy Object
        private UlipServiceRef.UlipServiceClient RevService { get; set; }
        private GenericServiceRef.GenericServiceClient GenService { get; set; }

        //Connection String
        private static string ConnectionString_life { get; set; }
        private static string ConnectionString { get; set; }

        //App Server IP
        private string AppServerIP { get; set; }


        //Client Server IP
        private string ClientIP { get; set; }

        //General Service BL
        public GeneralServiceBL.GeneralService objGeneralService = null;

        //Constructor
        public NorthwindApiController()
        {
            db.Configuration.ProxyCreationEnabled = false;
            ConnectionString_life = System.Configuration.ConfigurationManager.AppSettings["con_life"].ToString().Trim();
            ConnectionString = System.Configuration.ConfigurationManager.AppSettings["con"].ToString().Trim();
            objGeneralService = new GeneralServiceBL.GeneralService();

        }

        //api/NorthWind?company_name=something
        public Object GetSupplierByCompany(string company_name)
        {
            var suppliers = db.Suppliers.Where(m => m.Company_Name == company_name.Trim());
            if (suppliers != null && suppliers.Count() > 0)
            {
                return suppliers;
            }
            else
            {
                return "No matching records found";
            }
        }

        //api/NorthwindApi?product_name=something
        public Object GetProductsByProductName(string product_name)
        {
            var products = db.Products.Where(m => m.Product_Name == product_name.Trim());
            if (products != null && products.Count() > 0)
            {
                return products;
            }
            else
            {
                return "No matching records found";
            }
        }

        public Object GetOrdersByCustomerID(string customerID)
        {
            var orders = db.Orders.Where(m => m.Customer_ID == customerID.Trim());
            if (orders != null && orders.Count() > 0)
            {
                return orders;
            }
            else
            {
                return "No matching records found";
            }
        }

        public Object GetOrderDetailByOrderID(int id)
        {
            var order_details = db.Order_Details.Where(m => m.Order_ID == id);
            if (order_details != null && order_details.Count() > 0)
            {
                return order_details;
            }

            else
            {
                return "No matching records found";
            }
        }

        public WCFResult GetPolicyServicingDtlssWcf(string policy_no_wcf)
        {
            
            var result = new UlipServiceRef.PolicyServicingViewDtls();
            RevService = new UlipServiceRef.UlipServiceClient();
            result = RevService.GetViewDtls(policy_no_wcf.ToString().Trim());

            var wcfresult = new WCFResult();
            if (result != null)
            {
                wcfresult.status = "Success";
                wcfresult.response = result;
                wcfresult.errors.Clear();
            }
            else
            {
                wcfresult.status = "Failure";
                wcfresult.errors.Add(result.err_code.ToString().Trim());
                wcfresult.errors.Add(result.err_msg.Trim());
            }
            return wcfresult;
        }

        public WCFResult GetAutheticatedUser(string user_id, string password)
        {
            var connectionstring = ConnectionString_life.ToString().Trim();
            GenService = new GenericServiceRef.GenericServiceClient("EndPointHTTPGeneric", "http://172.31.247.146/WcfGenericService/GenericService.svc");     

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            AppServerIP = ipAddress.ToString().Trim();

            ClientIP = System.Web.HttpContext.Current.Request.UserHostAddress;

            var serverIP = AppServerIP.ToString().Trim();
            var clientIP = ClientIP.ToString().Trim();
            var wcfresult = new WCFResult();
            var authentication_data = new AuthenticationData();

            //var IsAuthenticated = objGeneralService.Authenticate(user_id.Trim(), password.Trim(), "Wcf", connectionstring.Trim(), "Loggedon", "", serverIP.Trim(), clientIP.Trim());
            //var IsAuthenticated = objGeneralService.Authenticate(user_id.Trim(), password.Trim(), "Wcf");

            var authencticationToken = GenService.Authenticate(user_id.Trim(), password.Trim());
            if (authencticationToken != "-1")
            {
                wcfresult.status = "Success";
                authentication_data.AuthenticationToken = authencticationToken.ToString().Trim();
                authentication_data.AuthenticationSessionID = string.Empty;
                wcfresult.response = authentication_data;
                wcfresult.errors.Clear();
            }
            else
            {
                wcfresult.status = "Failure";
                wcfresult.errors.Add("Invalid user or password");
            }

            return wcfresult;
        }

        public WCFResult GetPolicyServicingDtls(string policy_no)
        {
            DataObjectClass obj_dataobject = new DataObjectClass(ConnectionString_life);
            var result = new UlipServiceRef.PolicyServicingViewDtls();
            var wcfresult = new WCFResult();
            DataTable dt = null;

            try
            {
                OracleParameter[] arrparams = {
                                                  new OracleParameter("p_txt_policy_no_char", OracleType.VarChar, 2000),
                                                  new OracleParameter("get_policy_dtls", OracleType.Cursor),
                                                  new OracleParameter("p_err_code", OracleType.Number),
                                                  new OracleParameter("p_err_msg", OracleType.VarChar, 2000)
                                              };

                arrparams[0].Value = policy_no.Trim();
                arrparams[1].Direction = ParameterDirection.Output;
                arrparams[2].Direction = ParameterDirection.Output;
                arrparams[3].Direction = ParameterDirection.Output;

                dt = obj_dataobject.getProcDataTable("gc_life_pkg_ulip.get_policy_details", arrparams);

                if (dt != null && dt.Rows.Count > 0)
                {
                    result.assured_name = (dt.Rows[0][0] == System.DBNull.Value ? "" : dt.Rows[0][0].ToString().Trim());
                    result.policy_start_date = (dt.Rows[0][1] == System.DBNull.Value ? "" : dt.Rows[0][1].ToString().Trim());
                    result.product_name = (dt.Rows[0][2] == System.DBNull.Value ? "" : dt.Rows[0][2].ToString().Trim());
                    result.annual_premium = (dt.Rows[0][6] == System.DBNull.Value ? "" : dt.Rows[0][6].ToString().Trim());
                    result.base_sum_insured = (dt.Rows[0][7] == System.DBNull.Value ? "" : dt.Rows[0][7].ToString().Trim());
                    result.policy_status = (dt.Rows[0][3] == System.DBNull.Value ? "" : dt.Rows[0][3].ToString().Trim());
                    result.ref_no = (dt.Rows[0][4] == System.DBNull.Value ? "" : dt.Rows[0][4].ToString().Trim());
                    result.num_policy_no = (dt.Rows[0][8] == System.DBNull.Value ? "" : dt.Rows[0][8].ToString().Trim());
                    result.prem_payment_mode = (dt.Rows[0][5] == System.DBNull.Value ? "" : dt.Rows[0][5].ToString().Trim());

                    wcfresult.status = "Success";
                    wcfresult.response = result;
                    wcfresult.errors.Clear();
                }
            }
            catch (Exception)
            {
                wcfresult.status = "Failure";
                wcfresult.errors.Add("Some operation failed");
            }

            return wcfresult;
        }

        [HttpPost]
        public WCFResult PostCreateCustomer([FromBody]CustomerDetails customerDetails)
        {
            DataObjectClass obj_dataobject = new DataObjectClass(ConnectionString);
            var customerResult = new CreateCustomerResult();
            var wcfresult = new WCFResult();
            ArrayList arrlist;

            try
            {
                OracleParameter[] arrparams = {
                                                  new OracleParameter("P_TXT_CUSTOMER_TYPE", OracleType.VarChar, 2000),
                                                  new OracleParameter("P_TXT_SALUTATION", OracleType.VarChar, 2000),
                                                  new OracleParameter("P_TXT_FIRSTNAME", OracleType.VarChar, 2000),
                                                  new OracleParameter("P_TXT_MIDDLENAME", OracleType.VarChar, 2000),
                                                  new OracleParameter("P_TXT_LASTNAME", OracleType.VarChar, 2000),
                                                  new OracleParameter("P_TXT_CUSTOMER_NAME", OracleType.VarChar, 2000),
                                                  new OracleParameter("P_TXT_DOB", OracleType.VarChar, 2000),
                                                  new OracleParameter("P_TXT_PASSWORD", OracleType.VarChar, 2000),
                                                  new OracleParameter("P_TXT_GENDER", OracleType.VarChar, 2000),
                                                  new OracleParameter("P_TXT_EMAILID", OracleType.VarChar, 2000),
                                                  new OracleParameter("P_TXT_TINNO", OracleType.VarChar, 2000),
                                                  new OracleParameter("P_TXT_MOBILENO", OracleType.VarChar, 2000),
                                                  new OracleParameter("P_TXT_LANDLINENO", OracleType.VarChar, 2000),
                                                  new OracleParameter("E_CUST_ID", OracleType.Number),
                                                  new OracleParameter("E_ERR_CODE", OracleType.Number),
                                                  new OracleParameter("E_ERR_MSG", OracleType.VarChar, 2000)
                                              };

                arrparams[0].Value = (customerDetails.customer_type == null ? "" : customerDetails.customer_type.Trim().ToUpper());
                arrparams[1].Value = (customerDetails.salutation == null ? "" : customerDetails.salutation.ToString().Trim());
                arrparams[2].Value = (customerDetails.firstName == null ? "" : customerDetails.firstName.Trim().ToUpper());
                arrparams[3].Value = (customerDetails.middleName == null ? "" : customerDetails.middleName.Trim().ToUpper());
                arrparams[4].Value = (customerDetails.lastName == null ? "" : customerDetails.lastName.Trim().ToUpper());
                arrparams[5].Value = (customerDetails.customerName == null ? "" : customerDetails.customerName.Trim().ToUpper());
                arrparams[6].Value = (customerDetails.dob == null ? "" : customerDetails.dob.Trim().ToUpper());
                arrparams[7].Value = (customerDetails.password == null ? "" : customerDetails.password.Trim());
                arrparams[8].Value = (customerDetails.gender == null ? "" : customerDetails.gender.Trim().ToUpper());
                arrparams[9].Value = (customerDetails.emailID == null ? "" : customerDetails.emailID.Trim().ToUpper());
                arrparams[10].Value = (customerDetails.tinNo == null ? "" : customerDetails.tinNo.Trim().ToUpper());
                arrparams[11].Value = (customerDetails.mobileNo == null ? "" : customerDetails.mobileNo.Trim().ToUpper());
                arrparams[12].Value = (customerDetails.landlineNo == null ? "" : customerDetails.landlineNo.Trim().ToUpper());
                arrparams[13].Direction = ParameterDirection.Output;
                arrparams[14].Direction = ParameterDirection.Output;
                arrparams[15].Direction = ParameterDirection.Output;
            
                arrlist = obj_dataobject.getProcArrayList("GENPROC_CUSTOMER_LIGHT", arrparams);

                if (arrlist != null)
                {
                    var errCode = arrlist[1].ToString().Trim();
                    if (errCode == "NA")
                    {
                        wcfresult.status = "Success";
                        customerResult.customerID = (arrlist[0] == System.DBNull.Value ? "" : arrlist[0].ToString().Trim());
                        customerResult.errCode = (arrlist[1] == System.DBNull.Value ? "" : arrlist[1].ToString().Trim());
                        customerResult.errMsg = (arrlist[2] == System.DBNull.Value ? "" : arrlist[2].ToString().Trim());
                        wcfresult.response = customerResult;
                        wcfresult.errors.Clear();
                    }
                    else
                    {
                        wcfresult.status = "Failure";
                        customerResult.errCode = (arrlist[1] == System.DBNull.Value ? "" : arrlist[1].ToString().Trim());
                        customerResult.errMsg = (arrlist[2] == System.DBNull.Value ? "" : arrlist[2].ToString().Trim());
                        wcfresult.response = customerResult;
                        wcfresult.errors.Add(customerResult.errMsg.ToString().Trim());
                    }

                }
            }
            catch (Exception)
            {
                wcfresult.status = "Failure";
                wcfresult.errors.Add("Some operation failed");
            }

            return wcfresult;
        }

    }
}