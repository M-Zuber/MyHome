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
            var r = new CachedIncomeRepository(new IncomeAccess(ConnectionManager.ProviderFactory));
            return r.LoadById(id);
        }

        /// <summary>
        /// Loads all the Income from the cache
        /// </summary>
        /// <returns>All the Income as they are in the cache in generic-based
        /// list
        /// </returns>
        public static List<Income> LoadAll()
        {
            var r = new CachedIncomeRepository(new IncomeAccess(ConnectionManager.ProviderFactory));
            return r.LoadAll();
        }

        /// <summary>
        /// Loads the Incomes for a given month
        /// </summary>
        /// <param name="monthWanted">DateTime object with the wanted month and year</param>
        /// <returns>A list of Incomes filtered to a specific month</returns>
        public static List<Income> LoadOfMonth(DateTime monthWanted)
        {
            var r = new CachedIncomeRepository(new IncomeAccess(ConnectionManager.ProviderFactory));
            return r.LoadMonth(monthWanted);
        }

        #endregion

        #region Update Methods
        
        /// <summary>
        /// Updates the cache with the changes of the Income
        /// </summary>
        /// <param name="incomeToSave">The Income to be saved</param>
        public static void Save(Income incomeToSave)
        {
            var r = new CachedIncomeRepository(new IncomeAccess(ConnectionManager.ProviderFactory));
            r.Save(incomeToSave);
        }

        #endregion
        
        #region Create Methods

        public static int AddNewIncome(Income newIncome)
        {
            var r = new CachedIncomeRepository(new IncomeAccess(ConnectionManager.ProviderFactory));
            var result = r.Save(newIncome);
            return (result != null ? 1 : default(int));
        }

        #endregion

        #region Delete Region

        public static void Delete(int id)
        {
            var r = new CachedIncomeRepository(new IncomeAccess(ConnectionManager.ProviderFactory));
            r.Remove(id);
        }

        #endregion

        #endregion

        #region Other Methods

        public static double GetMonthTotal(DateTime monthWanted)
        {
            return IncomeHandler.LoadOfMonth(monthWanted).Sum(curIncome => curIncome.Amount);
        }

        public static double GetCategoryTotalForMonth(DateTime monthWanted, string categoryWanted)
        {
            return IncomeHandler.LoadOfMonth(monthWanted)
                .Where(curIncome => curIncome.Category.Name == categoryWanted)
                .Sum(curIncome => curIncome.Amount);
        }

        public static Dictionary<string, double> GetAllCategoryTotals(DateTime monthWanted)
        {
            Dictionary<string, double> categoryTotals = new Dictionary<string, double>();

            foreach (IncomeCategory currCategory in (new IncomeCategoryHandler()).LoadAll())
            {
                categoryTotals.Add(currCategory.Name, GetCategoryTotalForMonth(monthWanted, currCategory.Name));
            }

            return categoryTotals;
        }

        public static double GetPaymentMethodTotalForMonth(DateTime monthWanted, string methodWanted)
        {
            return IncomeHandler.LoadOfMonth(monthWanted)
                .Where(curIncome => curIncome.Method.Name == methodWanted)
                .Sum(curIncome => curIncome.Amount);
        }

        public static Dictionary<string, double> GetAllPaymentMethodTotals(DateTime monthWanted)
        {
            Dictionary<string, double> methodTotals = new Dictionary<string, double>();

            foreach (PaymentMethod curMethod in (new PaymentMethodHandler()).LoadAll())
            {
                methodTotals.Add(curMethod.Name, GetPaymentMethodTotalForMonth(monthWanted, curMethod.Name));
            }

            return methodTotals;
        }
        #endregion
    }
}
