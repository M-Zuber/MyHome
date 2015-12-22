namespace MyHome.DataClasses
{
    /// <summary>
    /// Conatains extension methods for the types defined in the MyHome.DataClasses namespace
    /// </summary>
    public static class Extensions
    {
        #region Expense Extensions
        
        /// <summary>
        /// Makes a shallow copy of the expense
        /// </summary>
        /// <param name="orginalExpense">The expense being copied</param>
        /// <returns>A shallow copy of the expense</returns>
        public static Expense Copy(this Expense orginalExpense)
        {
            return new Expense(orginalExpense.Amount, orginalExpense.Date,
                orginalExpense.Category, orginalExpense.Method, orginalExpense.Comments, orginalExpense.Id);
        }

        #endregion

        #region Income Extensions

        /// <summary>
        /// Makes a shallow copy of the income
        /// </summary>
        /// <param name="orginalIncome">The income being copied</param>
        /// <returns>A shallow copy of the income</returns>
        public static Income Copy(this Income orginalIncome)
        {
            return new Income(orginalIncome.Amount, orginalIncome.Date,
                orginalIncome.Category, orginalIncome.Method, orginalIncome.Comments, orginalIncome.Id);
        }

        #endregion
    }
}
