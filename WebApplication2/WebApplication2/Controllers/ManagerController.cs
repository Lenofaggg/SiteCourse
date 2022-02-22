using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ManagerController : Controller
    {
        autoparkEntities dbWork = new autoparkEntities();
        string connectionString = "Data Source=localhost;Initial Catalog=autopark;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        private QueryExc qe = new QueryExc();
        private employer curEmployer;

        // GET: Manager/Profile_
        public ActionResult Profile_()
        {

            employer empLog = Singleton.getInstance(curEmployer).employer;

            
            ViewData["id_passport"] = empLog.id_passport;
            ViewData["name_emp"] = empLog.name_emp;
            ViewData["adress"] = empLog.adress;
            ViewData["telephone"] = empLog.telephone;

            return View();
        }
        // GET: Manager/WorkPlace
        public ActionResult WorkPlace()
        {
            
                ViewBag.ja = dbWork.job_apply;
                ViewBag.emp = dbWork.employer;
                ViewBag.ed = dbWork.emp_dep;
                ViewBag.td = dbWork.tec_dep;
                ViewBag.etr = dbWork.emp_tec_route;
                ViewBag.num = dbWork.city_route;

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkPlaceRmJA([Bind(Include = "id_passport")] job_apply ja)
        {
            job_apply usr = dbWork.job_apply.Where(x => x.id_passport == ja.id_passport).Select(o => o)
                .First(); 
            
            dbWork.job_apply.Remove(usr);
            dbWork.SaveChanges();

            return RedirectToAction("WorkPlace");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkPlaceAdEmp([Bind(Include = "id_passport,name_emp,adress,telephone,status_,login_,password_")] employer e)
        {
            dbWork.employer.Add(e);
            dbWork.SaveChanges();

            return RedirectToAction("WorkPlace");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkPlaceRmEmp([Bind(Include = "id_passport")] employer emp)
        {
            employer e = dbWork.employer.Where(x => x.id_passport == emp.id_passport).Select(o => o)
                .First();

            dbWork.employer.Remove(e);
            dbWork.SaveChanges();

            return RedirectToAction("WorkPlace");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkPlaceRmED([Bind(Include = "id_passport")] emp_dep ed)
        {
            string str = $"IF EXISTS (SELECT * from emp_dep where id_passport={ed.id_passport}) DELETE FROM emp_dep WHERE id_passport={ed.id_passport}";
            qe.SelectorQuery(str);

            return RedirectToAction("WorkPlace");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkPlaceAdED([Bind(Include = "id_dep,id_passport,salary,profession")] emp_dep ed)
        {
            string str = string.Format("IF NOT EXISTS(SELECT * from emp_dep where id_passport={1}) " +
                                       "INSERT INTO emp_dep(id_dep, id_passport, salary, profession) VALUES ('{0}',{1}, {2},'{3}')"
                , ed.id_dep, ed.id_passport, ed.salary, ed.profession);
            qe.SelectorQuery(str);

            return RedirectToAction("WorkPlace");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkPlaceUpdEmp([Bind(Include = "id_dep,id_passport,salary,profession")] emp_dep ed)
        {
            string str = string.Format("UPDATE emp_dep SET [id_dep]='{0}', [salary]={2},[profession]='{3}' WHERE [id_passport]={1}"
                , ed.id_dep, ed.id_passport, ed.salary, ed.profession);
            qe.SelectorQuery(str);

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    var cmd = new SqlCommand(
            //        "UPDATE emp_dep SET [id_dep]=@dep, [salary]=@s,[profession]=@p WHERE [id_passport]=@pass");
            //    cmd.Parameters.AddWithValue("@dep", ed.id_dep);
            //    cmd.Parameters.AddWithValue("@pass", ed.id_passport);
            //    cmd.Parameters.AddWithValue("@s", ed.salary);
            //    cmd.Parameters.AddWithValue("@p", ed.profession);

            //    cmd.Connection = connection;
            //    cmd.ExecuteNonQuery();
            //    connection.Close();
            //}


            return RedirectToAction("WorkPlace");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkPlaceRmTD([Bind(Include = "id_tec")] tec_dep td)
        {
            string str = $"IF EXISTS(SELECT* from tec_dep where id_tec={td.id_tec}) DELETE FROM tec_dep WHERE id_tec={td.id_tec}";
            qe.SelectorQuery(str);

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    var cmd = new SqlCommand(
            //        "IF EXISTS(SELECT * from tec_dep where id_tec=@t)" +
            //        "DELETE FROM tec_dep WHERE id_tec=@t");
            //    cmd.Parameters.AddWithValue("@t", td.id_tec);

            //    cmd.Connection = connection;
            //    cmd.ExecuteNonQuery();
            //    connection.Close();
            //}
            return RedirectToAction("WorkPlace");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkPlaceAdTD([Bind(Include = "id_tec,id_dep")] tec_dep td)
        {
            string query = string.Format("IF NOT EXISTS(SELECT * from tec_dep where id_tec={1}) INSERT INTO tec_dep(id_dep, id_tec) VALUES ('{0}', {1})"
                , td.id_dep, td.id_tec);
            qe.SelectorQuery(query);

            return RedirectToAction("WorkPlace");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkPlaceRmETR([Bind(Include = "id_tec")] emp_tec_route etr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(
                    "IF EXISTS(SELECT * from emp_tec_route where id_tec=@t)" +
                    "DELETE FROM emp_tec_route WHERE id_tec=@t");
                cmd.Parameters.AddWithValue("@t", etr.id_tec);

                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                connection.Close();
            }

            return RedirectToAction("WorkPlace");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkPlaceAdETR([Bind(Include = "id_tec,id_passport,number,type_e_t")] emp_tec_route etr)
        {
            string str = $"INSERT INTO emp_tec_route (id_tec, id_passport, number, type_e_t)" +
                         $" VALUES ({etr.id_tec},{etr.id_passport},'{etr.number}','{etr.type_e_t}')";
            qe.SelectorQuery(str);

            return RedirectToAction("WorkPlace");
        }
        

    }
}