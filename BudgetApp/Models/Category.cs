using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BudgetApp.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        [DisplayName ("Budget")]
        public decimal BudgetCost { get; set; }
        public string Type { get; set; }
        public int BudgetID { get; set; }
        public virtual Budget Budget { get; set; }
        public virtual List<Expense> Expenses { get; set; }
    }
}