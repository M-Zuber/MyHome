using System.Collections.Generic;
using FrameWork;
using LocalTypes;

namespace DataAccess
{
    /// <summary>
    /// Contains methods neccesary for CRUD methods of expense categories
    /// -Before sending to the next tier translates into Local types
    /// </summary>
    public class ExpenseCategoryAccess
    {
        #region CRUD Methods

        #region Read Methods

        /// <summary>
        /// Loads a specific Expense category from the cache
        /// </summary>
        /// <param name="id">The id of the Expense category wanted</param>
        /// <returns>The expense category as it is in the cache</returns>
        public static ExpenseCategory LoadById(uint id)
        {
            StaticDataSet.t_expenses_categoryRow requestedRow =
                Cache.SDB.t_expenses_category.FindByID(id);
            return new ExpenseCategory((int)requestedRow.ID, requestedRow.NAME);
        }

        /// <summary>
        /// Loads all the Expense categories from the cache
        /// </summary>
        /// <returns>All the Expense categories as they are in the cache in generic-based
        /// list
        /// </returns>
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
