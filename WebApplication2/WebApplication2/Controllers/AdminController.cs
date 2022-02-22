using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    
    public class AdminController : Controller
    {
        string connectionString = "Data Source=localhost;Initial Catalog=autopark;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

        private QueryExc qe = new QueryExc();
        // GET: Admin
        public ActionResult Index()
        {
            
            return View();
        }


        // GET: Admin
        [HttpPost]
        public ActionResult Index(string query)
        {
            ViewBag.adm = qe.SelectorQuery(query);
            return View();
        }
        

    }
}