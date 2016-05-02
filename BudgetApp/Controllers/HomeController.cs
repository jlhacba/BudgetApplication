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

        public ActionResult BudgetChart()
        {
            var pf = new List<Donut>();
            
            foreach (var c in db.Categories)
            {
                var newDonut = new Donut();
                newDonut.value = c.BudgetCost.ToString();
                newDonut.label = c.Type;
                pf.Add(newDonut);
            }

            return Json(pf, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BudgetBarStacked()
        {
            var bs = new List<StackedBar>();

            foreach (var c in db.Categories.Include("Expenses"))
            {


                //var value = c.Expenses.Count();

                var newStackedBar = new StackedBar();
                newStackedBar.xkey = c.Name.ToString();
                newStackedBar.ykey1 = c.BudgetCost.ToString();
                newStackedBar.ykey2 = c.Expenses.Where(e => e.DateRecorded.Year == DateTime.Today.Year).Where(e => e.DateRecorded.Month == DateTime.Today.Month).Select(e => e.Cost).Sum().ToString();



                //if (c.Expenses != null)
                //{
                //    newStackedBar.ykey2 = c.Expenses.Where(e => e.DateRecorded.Year == DateTime.Today.Year).Where(e => e.DateRecorded.Month == DateTime.Today.Month).Select(e => e.Cost).Sum().ToString();

                //}
                //else
                //{
                //    newStackedBar.ykey2 = "0";

                //}
                    
                bs.Add(newStackedBar);
            }

            return Json(bs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            if (db.Categories.Count() == 0)
            {
                ViewBag.TotalBudget = "No Set";
                return View();
            }
            else
            {

                var totalBudget = db.Categories.Select(c => c.BudgetCost).Sum();
                ViewBag.TotalBudget = totalBudget;
                return View();
            }

            
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