using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetApp.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public decimal BudgetCost { get; set; }
        public string CatColor { get; set; }
        public int BudgetID { get; set; }
        public virtual Budget Budget { get; set; }
        public virtual List<Expense> Expenses { get; set; }
    }
}