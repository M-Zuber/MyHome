using System;

namespace MyHome.DataClasses
{
    public class Transaction
    {
        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public PaymentMethod Method { get; set; }

        public int PaymentMethodId { get; set; }

        public int Id { get; set; }

        public string Comments { get; set; }
    }
}
