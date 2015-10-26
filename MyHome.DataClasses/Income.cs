using System;

namespace MyHome.DataClasses
{
    public class Income : Transaction
    {
        #region Properties

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

        public int CategoryId { get; set; }
        public int PaymentMethodId { get; set; }

        #endregion

        #region C'Tor

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

        #endregion

        #region Override Methods

        public override bool Equals(object obj)
        {
            Income incomeComparing = (Income)obj;

            return ((Amount == incomeComparing.Amount) &&
                    (Category.Equals(incomeComparing.Category)) &&
                    (Comments == incomeComparing.Comments) &&
                    (Date == incomeComparing.Date) &&
                    (Id == incomeComparing.Id) &&
                    (Method.Equals(incomeComparing.Method)));
        }

        public override int GetHashCode()
        {
            return new { Id, Amount, Category, Comments, Date, Method }.GetHashCode();
        }

        #endregion
    }
}
