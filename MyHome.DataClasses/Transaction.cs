using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyHome.DataClasses
{
    public class Transaction
    {
        public Category Category { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public PaymentMethod Method { get; set; }

        public int Id { get; set; }
        public string Comment { get; set; }
    }
}
