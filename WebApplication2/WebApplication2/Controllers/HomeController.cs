using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        autoparkEntities dbHome = new autoparkEntities();
        
        string connectionString = "Data Source=localhost;Initial Catalog=autopark;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

        // GET: Home/Index
        public ActionResult Index()
        {
            var cityRoute = dbHome.city_route;
            ViewBag.city_route = cityRoute;

            return View();
        }
        // GET: Home/Authorization
        public ActionResult Authorization()
        {

            return View();
        }

        // GET: Home/Authorization
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authorization([Bind(Include = "login_,password_")] employer empLog)
        {
            if (ModelState.IsValid)
            {
                using (dbHome)
                {
                    empLog = dbHome.employer.FirstOrDefault(x =>
                        x.login_ == empLog.login_ && x.password_ == empLog.password_);
                }
                if (empLog != null)
                {
                    //Session["id_passport"] = empLog.id_passport.ToString();
                    //Session["status"] = empLog.status_.ToString();
                    //return Redirect("~/Work/Profile_");

                   

                    switch (Singleton.getInstance(empLog).employer.status_)
                    {
                        case "admin":
                            return Redirect("~/Admin/Index");
                        case "manager":
                            return Redirect("~/Manager/Profile_");
                        default:
                            return Redirect("~/Work/Profile_"); ;
                    }
                }
            }
            return View();
        }

        // GET: Home/Inform
        public ActionResult Inform()
        {

            return View();
        }

        // GET: Home/Passenger
        public ActionResult Passenger()
        {
            

           
            return View();
        }

        // GET: Home/Passenger
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Passenger([Bind(Include = "number")] emp_tec_route etr )
        {
            var users = dbHome.emp_tec_route.Where(x => x.number == etr.number).GroupBy(x => x.id_tec)
                .Select(grp => grp.FirstOrDefault());

            ViewBag.TechRoute = users;

            return View();
        }

        // GET: Home/Application
        public ActionResult Application()
        {

            return View();
        }
        // GET: Home/Application
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Application([Bind(Include = "id_passport,name_j_apply,name_prof,telephone,info_j_apply")] job_apply ja)
        {
            dbHome.job_apply.Add(ja);
            dbHome.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}