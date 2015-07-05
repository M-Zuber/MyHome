using System;

namespace LocalTypes
{
    // TODO - QC - Income and Expense are exactly the same, maybe we could do something about it.
    public class Income
    {
         #region Properties

        /// <summary>
        /// The amount of the income
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The date of the income
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Category of the income
        /// </summary>
        public IncomeCategory Category { get; set; }

        /// <summary>
        /// How the income was payed
        /// </summary>
        public PaymentMethod Method { get; set; }

        /// <summary>
        /// Additional info about the income
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// ID number of the income in the data table
        /// </summary>
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public int PaymentMethodId { get; set; }

        #endregion

        #region C'Tor

        public Income(decimal amount, DateTime date, IncomeCategory incomeCategory,
            PaymentMethod paymentMethod, string comment, int id = 0)
        {
            Amount = amount;
            Category = incomeCategory;
            Comment = comment;
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
                    (Comment == incomeComparing.Comment) &&
                    (Date == incomeComparing.Date) &&
                    (Id == incomeComparing.Id) &&
                    (Method.Equals(incomeComparing.Method)));
        }

        // TODO - QC - override get hash code as well.
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
