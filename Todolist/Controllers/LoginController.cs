using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Todolist.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        SqlConnection conString = new SqlConnection(ConfigurationManager.ConnectionStrings["Constring"].ConnectionString);
        public ActionResult LoginView()
        {
            return View();
        }
        public JsonResult checkUser(string email, string PasswordTxt)
        {
            string Result = "";
            try
            {
                DataTable dtOrderStatus = new DataTable();
                // SqlCommand cmd = new SqlCommand("select reg_email, reg_password from tbl_registration where reg_email ="+email+" and reg_password="+PasswordTxt+")", conString);
                SqlDataAdapter da = new SqlDataAdapter("select reg_username, reg_email, reg_password from tbl_registration where reg_email ='" + email + "' and reg_password='" + PasswordTxt + "'", conString);
                da.Fill(dtOrderStatus);
                //var data = dtOrderStatus.AsEnumerable().Select(row => row.ItemArray).ToArray();

                if (dtOrderStatus.Rows.Count>0)
                {
                    var data = dtOrderStatus.AsEnumerable().Select(row => row.ItemArray).ToArray();
                    Session["username"] = data[0][0];
                    Result = "valid User";
                }
                else
                {
                    Result = "Incorrect email or password";
                }
                
            }
            catch (Exception e)
            {
                Result = "Error : " + e.Message;
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
    }
}