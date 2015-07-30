﻿using System;

namespace MyHome.DataClasses
{
    public class Expense: Transaction
    {
        public Expense()
        {
        }

        public Expense(decimal amount, DateTime date, ExpenseCategory expenseCategory,
            PaymentMethod paymentMethod, string comment, int id = 0)
        {
            Amount = amount;
            Category = expenseCategory;
            Comment = comment;
            Date = date;
            Id = id;
            Method = paymentMethod;
        }

        ///// <summary>
        /////     The amount of the expense
        ///// </summary>
        //public decimal Amount { get; set; }

        ///// <summary>
        /////     The date of the expense
        ///// </summary>
        //public DateTime Date { get; set; }

        public int CategoryId { get; set; }

        public int PaymentMethodId { get; set; }

        /// <summary>
        ///     Category of the expense
        /// </summary>
        public new ExpenseCategory Category { get; set; }

        /// <summary>
        ///     How the expense was payed
        /// </summary>
        //public PaymentMethod Method { get; set; }

        /// <summary>
        ///     Additional info about the expense
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        ///     ID number of the expense in the data table
        /// </summary>
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            var expenseComparing = (Expense) obj;

            return ((Amount == expenseComparing.Amount) &&
                    (Category.Equals(expenseComparing.Category)) &&
                    (Comment == expenseComparing.Comment) &&
                    (Date == expenseComparing.Date) &&
                    (Id == expenseComparing.Id) &&
                    (Method.Equals(expenseComparing.Method)));
        }

        public override int GetHashCode()
        {
            return new {Id, Amount, Category, Comment, Date, Method}.GetHashCode();
        }
    }
}