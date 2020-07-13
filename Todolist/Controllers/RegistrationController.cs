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
    public class RegistrationController : Controller
    {
        // GET: Registration
        SqlConnection conString = new SqlConnection(ConfigurationManager.ConnectionStrings["Constring"].ConnectionString);
        public ActionResult Registration()
        {
            return View();
        }
        public JsonResult newUser(string reg_username,string Emailtxt, string PasswordTxt)
        {
            string Result = "";
            try
            {
                conString.Open();
                SqlCommand cmd = new SqlCommand("sp_newUSer", conString);
                cmd.CommandType = CommandType.StoredProcedure; 
                cmd.Parameters.AddWithValue("@reg_username", @reg_username);
                cmd.Parameters.AddWithValue("@reg_email", @Emailtxt);
                cmd.Parameters.AddWithValue("@reg_password", @PasswordTxt);
                cmd.Parameters.Add("@Message", SqlDbType.VarChar, 100);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                Result = cmd.Parameters["@Message"].Value.ToString();
                
            }
            catch (Exception e)
            {
                Result = "Error : " + e.Message;
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
    }
}