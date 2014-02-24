using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using LocalTypes;

namespace BusinessLogic
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of expenses
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class ExpenseHandler
    {
        #region CRUD Methods

        #region Read Methods

        /// <summary>
        /// Loads a specific Expense from the cache
        /// </summary>
        /// <param name="id">The id of the Expense wanted</param>
        /// <returns>The expense as it is in the cache</returns>
        public static Expense LoadById(int id)
        {
            return ExpenseAccess.LoadById(id);
        }

        /// <summary>
        /// Loads all the Expenses from the cache
        /// </summary>
        /// <returns>All the Expenses as they are in the cache in generic-based
        /// list
        /// </returns>
        public static List<Expense> LoadAll()
        {
            return ExpenseAccess.LoadAll();
        }

        /// <summary>
        /// Loads the expenses for a given month
        /// </summary>
        /// <param name="monthWanted">DateTime object with the wanted month and year</param>
        /// <returns>A list of expenses filtered to a specific month</returns>
        public static List<Expense> LoadOfMonth(DateTime monthWanted)
        {
            List<Expense> allExpensesCategories = LoadAll();

            return (from expense in allExpensesCategories
                    where expense.Date.Month == monthWanted.Month
                       && expense.Date.Year == monthWanted.Year
                    select expense).ToList<Expense>();
        }

        #endregion

        #region Update Methods
        
        /// <summary>
        /// Updates the cache with the changes of the expense
        /// </summary>
        /// <param name="expenseToSave">The expense to be saved</param>
        public static void Save(Expense expenseToSave)
        {
            ExpenseAccess.Save(expenseToSave);
        }

        #endregion

        #endregion
    }
}
