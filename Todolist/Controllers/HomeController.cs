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
    public class HomeController : Controller
    {
        SqlConnection conString = new SqlConnection(ConfigurationManager.ConnectionStrings["Constring"].ConnectionString);
        public ActionResult Index()
        {
           // string conString = null;
          //  SqlConnection cnn;
         

          //  cnn = new SqlConnection(conString);
            try
            {
                //conString.Open();
                //ViewBag.Connetion="Connection Open ! ";
                //DataTable dtOrderStatus = new DataTable();
                //SqlDataAdapter da = new SqlDataAdapter("select * from tbl_todolist", conString);
                //da.Fill(dtOrderStatus);
                //var data = dtOrderStatus.AsEnumerable().Select(row => row.ItemArray).ToArray();
                //conString.Close();
            }
            catch (Exception ex)
            {
                ViewBag.Connetion = "Connection Closed ";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public  JsonResult getData()
        {
            try
            {
                DataTable dtOrderStatus = new DataTable();
                conString.Open();
                //ViewBag.Connetion = "Connection Open ! ";
                SqlDataAdapter da = new SqlDataAdapter("select * from tbl_todolist", conString);
                //da.Fill(dtOrderStatus);
                //var data = dtOrderStatus.AsEnumerable().Select(row => row.ItemArray).ToArray();
                
                da.Fill(dtOrderStatus);
                var data1 = dtOrderStatus.AsEnumerable().Select(row => row.ItemArray).ToArray();
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var data = "Error : " + e.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult deleteData(int deleteID)
        {
            var message = "";
            try
            {
                DataTable dtOrderStatus = new DataTable();
                conString.Open();
                SqlCommand cmd = new SqlCommand("sp_deleteTodo", conString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@deleteId", deleteID);
                cmd.Parameters.Add("@Message", SqlDbType.VarChar, 100);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                // cmd.CommandText = query;
                //cmd.Connection = conString;
                cmd.ExecuteNonQuery();
                conString.Close();
                message = cmd.Parameters["@Message"].Value.ToString();
            }
            catch (Exception e)
            {
                message = "Error : " + e.Message;
                
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}