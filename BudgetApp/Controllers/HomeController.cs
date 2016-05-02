﻿using BudgetApp.Models;
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

            foreach (var c in db.Categories)
            {
                var newStackedBar = new StackedBar();
                newStackedBar.xkey = c.Name.ToString();
                newStackedBar.ykey1 = "0";
                newStackedBar.ykey2 = "25";
 
                    
             
                    //if (c.Expenses == null)
                    //{"0"}
                    //else
                    //    c.Expenses.Where(e => e.DateRecorded.Year == DateTime.Today.Year).Where(e => e.DateRecorded.Month == DateTime.Today.Month).Select(e => e.Cost).Sum().ToString();
                bs.Add(newStackedBar);
            }

            return Json(bs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var totalBudget = db.Categories.Select(c => c.BudgetCost).Sum();
            ViewBag.TotalBudget = totalBudget;

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