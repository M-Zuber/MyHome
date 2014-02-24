using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalTypes;

namespace FrameWork
{
    public static class Extensions
    {
        #region Expense Extensions
        
        public static Expense Copy(this Expense orgionalExpense)
        {
            return new Expense(orgionalExpense.Amount, orgionalExpense.Date,
                orgionalExpense.Category, orgionalExpense.Method, orgionalExpense.Comment, orgionalExpense.ID);
        }

        #endregion
    }
}
