using System;

namespace MyHome.DataClasses
{
    public class Income : Transaction
    {
        ///// <summary>
        ///// The amount of the income
        ///// </summary>
        public new decimal Amount { get; set; }

        ///// <summary>
        ///// The date of the income
        ///// </summary>
        public new DateTime Date { get; set; }

        /// <summary>
        /// Category of the income
        /// </summary>
        public new IncomeCategory Category { get; set; }

        /// <summary>
        /// How the income was payed
        /// </summary>
        public new PaymentMethod Method { get; set; }

        /// <summary>
        /// Additional info about the income
        /// </summary>
        public new string Comments { get; set; }

        /// <summary>
        /// ID number of the income in the data table
        /// </summary>
        public new int Id { get; set; }

        public new int CategoryId { get; set; }
        public new int PaymentMethodId { get; set; }

        public Income()
        {
        }

        public Income(decimal amount, DateTime date, IncomeCategory incomeCategory,
            PaymentMethod paymentMethod, string comment, int id = 0)
        {
            Amount = amount;
            Category = incomeCategory;
            Comments = comment;
            Date = date;
            Id = id;
            Method = paymentMethod;
        }

        public Income(decimal amount, DateTime date, int categoryId, int methodId, string comments)
        {
            Amount = amount;
            CategoryId = categoryId;
            Comments = comments;
            Date = date;
            PaymentMethodId = methodId;
        }

        public override bool Equals(object obj)
        {
            var incomeComparing = (Income)obj;
            
            return incomeComparing != null &&
                   Amount == incomeComparing.Amount &&
                   (Category == null && incomeComparing.Category == null || Category?.Equals(incomeComparing.Category) == true) &&
                   Comments == incomeComparing.Comments &&
                   Date == incomeComparing.Date &&
                   Id == incomeComparing.Id &&
                   (Method == null && incomeComparing.Method == null || Method?.Equals(incomeComparing.Method) == true);
        }

        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            return new { Id, Amount, Category, Comments, Date, Method }.GetHashCode();
            // ReSharper restore NonReadonlyMemberInGetHashCode
        }
    }
}
