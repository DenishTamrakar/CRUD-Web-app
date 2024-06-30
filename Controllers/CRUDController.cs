using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Data.SqlClient;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class CRUDController : Controller
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
        public ActionResult Index()
        {
            List<Student> students = [];
            ConnectionToString();
            cmd.CommandText = "select * from student_record";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Student student = new()
                {
                    ID = (int)dr["stud_id"],
                    Name = dr["stud_name"].ToString(),
                    Age = (int)dr["stud_age"],
                    Gender = dr["stud_gender"].ToString(),
                    Address = dr["stud_address"].ToString(),
                };
                students.Add(student);
            }
            return View(students);
        }

        // GET: CRUDController/Details/5
        public ActionResult Details(int id)
        {
            List<Student> students = [];
            ConnectionToString();
            cmd.CommandText = "select * from student_record where stud_id=" + id;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Student student = new()
                {
                    ID = (int)dr["stud_id"],
                    Name = dr["stud_name"].ToString(),
                    Age = (int)dr["stud_age"],
                    Gender = dr["stud_gender"].ToString(),
                    Address = dr["stud_address"].ToString(),
                };
                students.Add(student);
            }
            return View(students);
        }

        // GET: CRUDController/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Insert(Student a)
        {
            ConnectionToString();
            cmd.CommandText = "insert into student_record(stud_name,stud_age,stud_gender,stud_address) values('" + a.Name + "'," + a.Age + ",'" + a.Gender + "','" + a.Address + "')";
            if (cmd.ExecuteNonQuery() != 0)
            {
                TempData["error"] = "Record added.";
            }
            else 
            {
                TempData["error"] = "Unable to add record.";
            }
            return RedirectToAction("Index", "CRUD");
        }

        // GET: CRUDController/Edit/5
        public ActionResult Edit(int id)
        {
            List<Student> students = [];
            ConnectionToString();
            cmd.CommandText = "select * from student_record where stud_id=" + id;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Student student = new()
                {
                    ID = (int)dr["stud_id"],
                    Name = dr["stud_name"].ToString(),
                    Age = (int)dr["stud_age"],
                    Gender = dr["stud_gender"].ToString(),
                    Address = dr["stud_address"].ToString(),
                };
                students.Add(student);
            }
            TempData["select"] = "selected";
            return View(students);
        }

        [HttpPost]
        public ActionResult Update(int id, Student b)
        {
            ConnectionToString();
            cmd.CommandText = "update student_record set stud_name = '" + b.Name + "',  stud_age = " + b.Age + ", stud_gender = '" + b.Gender + "', stud_address = '" + b.Address + "' where stud_id = " + id;
            if (cmd.ExecuteNonQuery() != 0)
            {
                TempData["error"] = "Record Updated.";
            }
            else 
            {
                TempData["error"] = "Unable to update record.";
            }
            return RedirectToAction("Index","CRUD");
        }

        // GET: CRUDController/Delete/5
        public ActionResult Delete(int id)
        {
            ConnectionToString();
            cmd.CommandText = "delete from student_record where stud_id=" + id;
            if (cmd.ExecuteNonQuery() != 0)
            {
                TempData["error"] = "Record deleted!";
            }
            else
            {
                TempData["error"] = "Unable to delete.";
            }
            return RedirectToAction("Index", "CRUD");
        }
    }
}
