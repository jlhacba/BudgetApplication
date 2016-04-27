using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetApp.Models
{
    public class Expense
    {
        public int ExpenseID { get; set; }
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public DateTime DateRecorded { get; set; }
        public decimal Cost { get; set; }
    }
}