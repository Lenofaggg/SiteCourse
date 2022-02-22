using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    
    public class WorkController : Controller
    {
        private employer curEmployer;
        // GET: Work/Profile
        public ActionResult Profile_()
        {
            autoparkEntities dbWork = new autoparkEntities();

            employer empLog = Singleton.getInstance(curEmployer).employer;

            //using (dbWork)
            //{
            //    empLog = dbWork.employer.FirstOrDefault(x =>
            //        x.id_passport == Singleton.getInstance(curEmployer).employer.id_passport);
            //}

            ;

            ViewData["id_passport"] = empLog.id_passport;
            ViewData["name_emp"] = empLog.name_emp;
            ViewData["adress"] = empLog.adress;
            ViewData["telephone"] = empLog.telephone;

            return View();
        }
        // GET: Work/Members
        public ActionResult Members()
        {
            autoparkEntities dbWork = new autoparkEntities();
            var users = from e in dbWork.employer
                        join ed in dbWork.emp_dep on e.id_passport equals ed.id_passport
                        select new ViewModel
                        {
                            emp = e,
                            ed = ed
                        };
            ViewBag.members = users;
            return View();
        }
        // GET: Work/Profession
        public ActionResult Profession()
        {
            autoparkEntities dbWork = new autoparkEntities();
            int d = Singleton.getInstance(curEmployer).employer.id_passport;

            emp_dep empdLog;

            using (dbWork)
            {
                empdLog = dbWork.emp_dep.FirstOrDefault(x =>
                    x.id_passport == d);
            }

            ViewData["id_passport"] = empdLog.id_passport;
            ViewData["id_dep"] = empdLog.id_dep;
            ViewData["salary"] = empdLog.salary;
            ViewData["profession"] = empdLog.profession;

            return View();
        }
    }
}