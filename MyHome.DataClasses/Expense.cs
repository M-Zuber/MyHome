using System;

namespace MyHome.DataClasses
{
    public class Expense
    {
         #region Properties

        /// <summary>
        /// The amount of the expense
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The date of the expense
        /// </summary>
        public DateTime Date { get; set; }


        public int CategoryId { get; set; }

        public int PaymentMethodId { get; set; }

        /// <summary>
        /// Category of the expense
        /// </summary>
        public ExpenseCategory Category { get; set; }

        /// <summary>
        /// How the expense was payed
        /// </summary>
        public PaymentMethod Method { get; set; }

        /// <summary>
        /// Additional info about the expense
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// ID number of the expense in the data table
        /// </summary>
        public int Id { get; set; }

        #endregion

        #region C'Tor

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

        #endregion

        #region Override Methods

        public override bool Equals(object obj)
        {
            Expense expenseComparing = (Expense)obj;

            return ((Amount == expenseComparing.Amount) &&
                    (Category.Equals(expenseComparing.Category)) &&
                    (Comment == expenseComparing.Comment) &&
                    (Date == expenseComparing.Date) &&
                    (Id == expenseComparing.Id) &&
                    (Method.Equals(expenseComparing.Method)));
        }

        // TODO - QC - override get hash code as well.
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
