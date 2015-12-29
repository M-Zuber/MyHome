using System;

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
            Comments = comment;
            Date = date;
            Id = id;
            Method = paymentMethod;
        }

        public Expense(decimal amount, DateTime date, int categoryID, int methodID, string comments)
        {
            Amount = amount;
            CategoryId = categoryID;
            Comments = comments;
            Date = date;
            PaymentMethodId = methodID;
        }

        ///// <summary>
        /////     The amount of the expense
        ///// </summary>
        public new decimal Amount { get; set; }

        ///// <summary>
        /////     The date of the expense
        ///// </summary>
        public new DateTime Date { get; set; }

        public new int CategoryId { get; set; }

        public new int PaymentMethodId { get; set; }

        /// <summary>
        ///     Category of the expense
        /// </summary>
        public new ExpenseCategory Category { get; set; }

        /// <summary>
        ///     How the expense was payed
        /// </summary>
        public new PaymentMethod Method { get; set; }

        /// <summary>
        ///     Additional info about the expense
        /// </summary>
        public new string Comments { get; set; }

        /// <summary>
        ///     ID number of the expense in the data table
        /// </summary>
        public new int Id { get; set; }

        public override bool Equals(object obj)
        {
            var expenseComparing = (Expense) obj;

            return ((Amount == expenseComparing.Amount) &&
                    ((Category == null && expenseComparing.Category == null) || Category.Equals(expenseComparing.Category)) &&
                    (Comments == expenseComparing.Comments) &&
                    (Date == expenseComparing.Date) &&
                    (Id == expenseComparing.Id) &&
                    ((Method == null && expenseComparing.Method == null) || Method.Equals(expenseComparing.Method)));
        }

        public override int GetHashCode()
        {
            return new {Id, Amount, Category, Comments, Date, Method}.GetHashCode();
        }
    }
}