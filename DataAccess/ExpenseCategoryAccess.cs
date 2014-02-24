using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameWork;
using LocalTypes;

namespace DataAccess
{
    public class ExpenseCategoryAccess
    {
        #region CRUD Methods

        #region Read Methods

        public static ExpenseCategory LoadById(uint id)
        {
            StaticDataSet.t_expenses_categoryRow requestedRow =
                Cache.SDB.t_expenses_category.FindByID((uint)id);
            return new ExpenseCategory((int)requestedRow.ID, requestedRow.NAME);
        }

        public static List<ExpenseCategory> LoadAll()
        {
            List<ExpenseCategory> allExpensesCategories = new List<ExpenseCategory>();

            foreach (StaticDataSet.t_expenses_categoryRow currExpenseCategory in Cache.SDB.t_expenses_category.Rows)
            {
                allExpensesCategories.Add(
                    new ExpenseCategory((int)currExpenseCategory.ID, currExpenseCategory.NAME));
            }

            return allExpensesCategories;
        }

        #endregion

        #endregion
    }
}
