using System;
using System.Collections.Generic;
using System.Linq;
using MyHome2013.Core.LocalTypes;

namespace BusinessLogic
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of incomes
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class IncomeHandler
    {
        ITransactionRepository<Income> incomeRepository;
        IRepository<PaymentMethod> methodRepository;
        IRepository<IncomeCategory> icRepository;

        public IncomeHandler(ITransactionRepository<Income> incomeRepository, IRepository<PaymentMethod> methodRepository, IRepository<IncomeCategory> icRepository)
        {
            this.incomeRepository = incomeRepository;
            this.methodRepository = methodRepository;
            this.icRepository = icRepository;
        }

        #region CRUD Methods

        #region Read Methods

        /// <summary>
        /// Loads a specific Income from the cache
        /// </summary>
        /// <param name="id">The id of the Income wanted</param>
        /// <returns>The Income as it is in the cache</returns>
        public Income LoadById(int id)
        {
            return incomeRepository.LoadById(id);
        }

        /// <summary>
        /// Loads all the Income from the cache
        /// </summary>
        /// <returns>All the Income as they are in the cache in generic-based
        /// list
        /// </returns>
        public List<Income> LoadAll()
        {
            return incomeRepository.LoadAll();
        }

        /// <summary>
        /// Loads the Incomes for a given month
        /// </summary>
        /// <param name="monthWanted">DateTime object with the wanted month and year</param>
        /// <returns>A list of Incomes filtered to a specific month</returns>
        public List<Income> LoadOfMonth(DateTime monthWanted)
        {
            return incomeRepository.LoadMonth(monthWanted);
        }

        #endregion

        #region Update Methods
        
        /// <summary>
        /// Updates the cache with the changes of the Income
        /// </summary>
        /// <param name="incomeToSave">The Income to be saved</param>
        public void Save(Income incomeToSave)
        {
            incomeRepository.Save(incomeToSave);
        }

        #endregion
        
        #region Create Methods

        public int AddNewIncome(Income newIncome)
        {
            var result = incomeRepository.Save(newIncome);
            return (result != null ? 1 : default(int));
        }

        #endregion

        #region Delete Region

        public void Delete(int id)
        {
            incomeRepository.Remove(id);
        }

        #endregion

        #endregion

        #region Other Methods

        public decimal GetMonthTotal(DateTime monthWanted)
        {
            return this.LoadOfMonth(monthWanted).Sum(curIncome => curIncome.Amount);
        }

        public decimal GetCategoryTotalForMonth(DateTime monthWanted, string categoryWanted)
        {
            return this.LoadOfMonth(monthWanted)
                .Where(curIncome => curIncome.Category.Name == categoryWanted)
                .Sum(curIncome => curIncome.Amount);
        }

        public Dictionary<string, decimal> GetAllCategoryTotals(DateTime monthWanted)
        {
            var categoryTotals = new Dictionary<string, decimal>();

            foreach (IncomeCategory currCategory in icRepository.LoadAll())
            {
                categoryTotals.Add(currCategory.Name, GetCategoryTotalForMonth(monthWanted, currCategory.Name));
            }

            return categoryTotals;
        }

        public decimal GetPaymentMethodTotalForMonth(DateTime monthWanted, string methodWanted)
        {
            return this.LoadOfMonth(monthWanted)
                .Where(curIncome => curIncome.Method.Name == methodWanted)
                .Sum(curIncome => curIncome.Amount);
        }

        public Dictionary<string, decimal> GetAllPaymentMethodTotals(DateTime monthWanted)
        {
            var methodTotals = new Dictionary<string, decimal>();

            foreach (PaymentMethod curMethod in methodRepository.LoadAll())
            {
                methodTotals.Add(curMethod.Name, GetPaymentMethodTotalForMonth(monthWanted, curMethod.Name));
            }

            return methodTotals;
        }
        #endregion
    }
}
