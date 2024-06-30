using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;
using System.Data.SqlClient;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqlConnection con = new();
        private readonly SqlCommand cmd = new();
        private SqlDataReader? dr;

        public void ConnectionToString()
        {
            con.ConnectionString = "data source =DESKTOP-QF4UMI6\\SQLEXPRESS; database =db_1; Integrated Security =True;";
            con.Open();
            cmd.Connection = con;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Validation(User u)
        {
            ConnectionToString();
            cmd.CommandText = "select * from user_log_info where user_name = '" + u.Username + "' and user_password = '" + u.Password + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                HttpContext.Session.SetString("Username", u.Username);
                return RedirectToAction("Index", "CRUD");
            }
            else
            {
                ViewData["error"] = "Invalid username or password.";
                return View("Login");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["logout"] = "Logout Sucessful.";
            return RedirectToAction("Login", "Home");
        }
    }
}
