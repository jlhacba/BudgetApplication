﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BudgetApp.Models;
using BudgetApp.ViewModels;

namespace BudgetApp.Controllers
{
    public class BudgetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


       

        public ActionResult Index()
        {
            return View();


        }

        public ActionResult BudgetDashboard()
        {
            if (db.Categories.Count() == 0)
            {
                ViewBag.TotalBudget = "Not Set";
                return View();
            }
            else
            {

                var totalBudget = db.Categories.Select(c => c.BudgetCost).Sum();
                ViewBag.TotalBudget = totalBudget;

                if (db.Expenses.Count() == 0)
                {
                    ViewBag.SpentBudget = 0;
                }
                else
                {
                    var spentBudget = db.Expenses.Select(e => e.Cost).Sum();
                    ViewBag.SpentBudget = spentBudget;
                }

                if (db.Expenses.Count() == 0)
                {
                    ViewBag.RemainingBudget = totalBudget;
                }
                else
                {
                    var remainingBudget = totalBudget - db.Expenses.Select(e => e.Cost).Sum();
                    ViewBag.RemainingBudget = remainingBudget;
                }

                return View();
            }
        }

        public ActionResult BudgetMonthChart()
        {
            var pf = new List<Donut>();

            foreach (var c in db.Categories.Where(c => c.Occurance == "Monthly"))
            {
                var newDonut = new Donut();
                newDonut.value = c.BudgetCost.ToString();
                newDonut.label = c.Type;
                pf.Add(newDonut);
            }

            return Json(pf, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BudgetMonthBarStacked()
        {
            var bs = new List<StackedBar>();

            foreach (var c in db.Categories.Include("Expenses").Where(c => c.Occurance == "Monthly"))
            {


                var expenses = c.Expenses.Where(e => e.DateRecorded.Year == DateTime.Today.Year).Where(e => e.DateRecorded.Month == DateTime.Today.Month).Select(e => e.Cost).Sum();
                var budgetLeft = c.BudgetCost - expenses;


                var newStackedBar = new StackedBar();
                newStackedBar.xkey = c.Type.ToString();
                // newStackedBar.xkey = "a[href^='http://stackoverflow.com']";
                newStackedBar.ykey1 = budgetLeft.ToString();
                newStackedBar.ykey2 = expenses.ToString();

                bs.Add(newStackedBar);
            }

            return Json(bs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BudgetMonthlyPartial()
        {
            if (db.Categories.Count() == 0)
            {
                ViewBag.TotalBudget = "Not Set";
                return View();
            }
            else
            {

                var totalBudget = db.Categories.Where(c => c.Occurance == "Monthly").Select(c => c.BudgetCost).Sum();
                ViewBag.TotalBudget = totalBudget;

                if (db.Expenses.Where(e => e.DateRecorded.Year == DateTime.Today.Year).Where(e => e.DateRecorded.Month == DateTime.Today.Month).Count() == 0)
                {
                    ViewBag.SpentBudget = 0;
                }
                else
                {
                    var spentBudget = db.Expenses.Where(e => e.DateRecorded.Year == DateTime.Today.Year).Where(e => e.DateRecorded.Month == DateTime.Today.Month).Where(c => c.Category.Occurance == "Monthly").Select(e => e.Cost).Sum();
                    ViewBag.SpentBudget = spentBudget;
                }

                if (db.Expenses.Where(e => e.DateRecorded.Year == DateTime.Today.Year).Where(e => e.DateRecorded.Month == DateTime.Today.Month).Count() == 0)
                {
                    ViewBag.RemainingBudget = totalBudget;
                }
                else
                {
                    var remainingBudget = totalBudget - db.Expenses.Where(c => c.Category.Occurance == "Monthly").Select(e => e.Cost).Sum();
                    ViewBag.RemainingBudget = remainingBudget;
                }

                return PartialView();

            }

        }

        public ActionResult BudgetAnnualChart()
        {
            var pf = new List<Donut>();

            foreach (var c in db.Categories)
            {
                var newDonut = new Donut();

                if (c.Occurance == "Monthly")
                {
                    var annualBudget = c.BudgetCost * 12;
                    newDonut.value = annualBudget.ToString();
                    newDonut.label = c.Type;
                    pf.Add(newDonut);
                }
                else
                {
                    var annualBudget = c.BudgetCost;
                    newDonut.value = annualBudget.ToString();
                    newDonut.label = c.Type;
                    pf.Add(newDonut);
                }

            }

            return Json(pf, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BudgetAnnualBarStacked()
        {
            var bs = new List<StackedBar>();

            foreach (var c in db.Categories.Include("Expenses"))
            {

                if (c.Occurance == "Monthly")
                {
                    var expenses = c.Expenses.Where(e => e.DateRecorded.Year == DateTime.Today.Year).Select(e => e.Cost).Sum();
                    var budgetLeft = (c.BudgetCost * 12) - (expenses);

                    var newStackedBar = new StackedBar();
                    newStackedBar.xkey = c.Type.ToString();
                    // newStackedBar.xkey = "a[href^='http://stackoverflow.com']";
                    newStackedBar.ykey1 = budgetLeft.ToString();
                    newStackedBar.ykey2 = expenses.ToString();

                    bs.Add(newStackedBar);

                }
                else
                {
                    var expenses = c.Expenses.Where(e => e.DateRecorded.Year == DateTime.Today.Year).Select(e => e.Cost).Sum();
                    var budgetLeft = (c.BudgetCost ) - (expenses );

                    var newStackedBar = new StackedBar();
                    newStackedBar.xkey = c.Type.ToString();
                    // newStackedBar.xkey = "a[href^='http://stackoverflow.com']";
                    newStackedBar.ykey1 = budgetLeft.ToString();
                    newStackedBar.ykey2 = expenses.ToString();

                    bs.Add(newStackedBar);
                }

            }

            return Json(bs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BudgetAnnualPartial()
        {
            if (db.Categories.Count() == 0)
            {
                ViewBag.TotalBudget = "Not Set";
                return PartialView();
            }
            else
            {
                
                var totalBudget = db.Categories.Select(c => c.BudgetCost).Sum() * 12;
                ViewBag.TotalBudget = totalBudget;

                if (db.Expenses.Count() == 0)
                {
                    ViewBag.SpentBudget = 0;
                }
                else
                {
                    var spentBudget = db.Expenses.Where(e => e.DateRecorded.Year == DateTime.Today.Year).Select(e => e.Cost).Sum();
                    ViewBag.SpentBudget = spentBudget;
                }

                if (db.Expenses.Count() == 0)
                {
                    ViewBag.RemainingBudget = totalBudget;
                }
                else
                {
                    var remainingBudget = (totalBudget * 12) - (db.Expenses.Select(e => e.Cost).Sum());
                    ViewBag.RemainingBudget = remainingBudget;
                }

                return PartialView();
            }


        }

        // GET: Budgets
        //public ActionResult Index()
        //{

        //    return View(db.Budgets.ToList());
        //}

        // GET: Budgets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // GET: Budgets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Budgets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BudgetID,Name,StartDate,EndDate")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                db.Budgets.Add(budget);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(budget);
        }

        // GET: Budgets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // POST: Budgets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BudgetID,Name,StartDate,EndDate")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                db.Entry(budget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(budget);
        }

        // GET: Budgets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // POST: Budgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Budget budget = db.Budgets.Find(id);
            db.Budgets.Remove(budget);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
