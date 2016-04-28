using BudgetApp.Models;
using BudgetApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetApp.Controllers
{

    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult GetCompleted()
        {
            var pf = new List<Donut>();

            var pass = new Donut();
            pass.label = "Completed";
            pass.value = "28";

            var fail = new Donut();
            fail.label = "Yet to Complete";

            var good = new Donut();
            good.label = "Good!";
            good.value = "300";
           
            fail.value = (db.Categories.Count().ToString());

            pf.Add(pass);
            pf.Add(fail);
            pf.Add(good);

            return Json(pf, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}