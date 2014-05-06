using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using LocalTypes;

namespace BusinessLogic
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of incomes
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class IncomeHandler
    {
        #region CRUD Methods

        #region Read Methods

        /// <summary>
        /// Loads a specific Income from the cache
        /// </summary>
        /// <param name="id">The id of the Income wanted</param>
        /// <returns>The Income as it is in the cache</returns>
        public static Income LoadById(int id)
        {
            return IncomeAccess.LoadById(id);
        }

        /// <summary>
        /// Loads all the Income from the cache
        /// </summary>
        /// <returns>All the Income as they are in the cache in generic-based
        /// list
        /// </returns>
        public static List<Income> LoadAll()
        {
            return IncomeAccess.LoadAll();
        }

        /// <summary>
        /// Loads the Incomes for a given month
        /// </summary>
        /// <param name="monthWanted">DateTime object with the wanted month and year</param>
        /// <returns>A list of Incomes filtered to a specific month</returns>
        public static List<Income> LoadOfMonth(DateTime monthWanted)
        {
            List<Income> allIncomes = LoadAll();

            return (from income in allIncomes
                    where income.Date.Month == monthWanted.Month
                       && income.Date.Year == monthWanted.Year
                    select income).ToList<Income>();
        }

        #endregion

        #region Update Methods
        
        /// <summary>
        /// Updates the cache with the changes of the Income
        /// </summary>
        /// <param name="incomeToSave">The Income to be saved</param>
        public static void Save(Income incomeToSave)
        {
            IncomeAccess.Save(incomeToSave);
        }

        #endregion
        
        #region Create Methods

        public static int AddNewIncome(Income newIncome)
        {
            return IncomeAccess.AddNewIncome(newIncome);
        }

        #endregion

        #endregion

        #region Other Methods

        public static Dictionary<string, double> GetCategoryTotals(DateTime monthWanted)
        {
            Dictionary<string, double> categoryTotals = new Dictionary<string, double>();

            categoryTotals.Add("Total Income", 0);
            foreach (IncomeCategory currCategory in (new IncomeCategoryHandler()).LoadAll())
            {
                categoryTotals.Add(currCategory.Name, 0);
            }

            foreach (Income currExpense in IncomeHandler.LoadOfMonth(monthWanted))
            {
                categoryTotals["Total Income"] += currExpense.Amount;
                categoryTotals[currExpense.Category.Name] += currExpense.Amount;
            }

            return categoryTotals;
        }

        #endregion
    }
}
