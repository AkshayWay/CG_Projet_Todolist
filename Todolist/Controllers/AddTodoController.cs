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
    public class AddTodoController : Controller
    {
        // GET: AddTodo
        SqlConnection conString = new SqlConnection(ConfigurationManager.ConnectionStrings["Constring"].ConnectionString);
        public ActionResult addTodoView(int? id)
        {
            if (id > 0)
            {
                Session["fetchTodod"] = id;
            }
            else
            {
                Session["fetchTodod"] = "";
            }
            return View();
        }
        public JsonResult newTodo(string TitleTxt, string descTxt, DateTime dateSel, string PrioritySel, int? EditId)
        {
            string Result = "";
            try
            {
                conString.Open();
                SqlCommand cmd = new SqlCommand("sp_AddTodo", conString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@todo_name", @TitleTxt);
                cmd.Parameters.AddWithValue("@todo_desc", @descTxt);
                cmd.Parameters.AddWithValue("@todo_date", @dateSel);
                cmd.Parameters.AddWithValue("@todo_priority", @PrioritySel);
                cmd.Parameters.AddWithValue("@editId", @EditId);
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
        public JsonResult fetchTodo()
        {
            try
            {
                var editTodoId = Session["fetchTodod"];
                if (editTodoId != "")
                {
                    DataTable dtOrderStatus = new DataTable();
                    conString.Open();
                    SqlDataAdapter da = new SqlDataAdapter("select * from tbl_todolist where todo_id = "+editTodoId, conString);

                    da.Fill(dtOrderStatus);
                    var data = dtOrderStatus.AsEnumerable().Select(row => row.ItemArray).ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = "";
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                var data = "Error : " + e.Message;
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            
        }
    }
}