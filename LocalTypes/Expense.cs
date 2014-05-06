using System;

namespace LocalTypes
{
    public class Expense
    {
         #region Properties

        /// <summary>
        /// The amount of the expense
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// The date of the expense
        /// </summary>
        public DateTime Date { get; set; }

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
        public int ID { get; private set; }

        #endregion

        #region C'Tor

        public Expense(double amount, DateTime date, ExpenseCategory expenseCategory,
            PaymentMethod paymentMethod, string comment, int id = 0)
        {
            this.Amount = amount;
            this.Category = expenseCategory;
            this.Comment = comment;
            this.Date = date;
            this.ID = id;
            this.Method = paymentMethod;
        }

        #endregion

        #region Override Methods

        public override bool Equals(object obj)
        {
            Expense expenseComparing = (Expense)obj;

            return ((this.Amount == expenseComparing.Amount) &&
                    (this.Category.Equals(expenseComparing.Category)) &&
                    (this.Comment == expenseComparing.Comment) &&
                    (this.Date == expenseComparing.Date) &&
                    (this.ID == expenseComparing.ID) &&
                    (this.Method.Equals(expenseComparing.Method)));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
