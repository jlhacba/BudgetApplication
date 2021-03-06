﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DataType(DataType.Date, ErrorMessage = "Must be a valid Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName ("Date Recorded")]
        public DateTime DateRecorded { get; set; }
        public decimal Cost { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}