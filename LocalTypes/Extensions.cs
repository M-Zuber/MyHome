using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalTypes
{
    /// <summary>
    /// Conatains extension methods for the types defined in the LocalType namespace
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
                orginalExpense.Category, orginalExpense.Method, orginalExpense.Comment, orginalExpense.ID);
        }

        #endregion
    }
}
