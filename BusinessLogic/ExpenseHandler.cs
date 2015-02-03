using System;
using System.Collections.Generic;
using System.Linq;
using MyHome2013.Core.LocalTypes;

namespace BusinessLogic
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of expenses
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class ExpenseHandler
    {
        ITransactionRepository<Expense> eRepository;
        IRepository<ExpenseCategory> ecHandler;
        IRepository<PaymentMethod> pmHandler;

        public ExpenseHandler(ITransactionRepository<Expense> eRepository, IRepository<ExpenseCategory> ecHandler, IRepository<PaymentMethod> pmHandler)
        {
            this.eRepository = eRepository;
            this.ecHandler = ecHandler;
            this.pmHandler = pmHandler;
        }

        #region CRUD Methods

        #region Read Methods

        /// <summary>
        /// Loads a specific Expense from the cache
        /// </summary>
        /// <param name="id">The id of the Expense wanted</param>
        /// <returns>The expense as it is in the cache</returns>
        public Expense LoadById(int id)
        {
            return eRepository.LoadById(id);
        }

        /// <summary>
        /// Loads all the Expenses from the cache
        /// </summary>
        /// <returns>All the Expenses as they are in the cache in generic-based
        /// list
        /// </returns>
        public List<Expense> LoadAll()
        {
            return eRepository.LoadAll();
        }

        /// <summary>
        /// Loads the expenses for a given month
        /// </summary>
        /// <param name="monthWanted">DateTime object with the wanted month and year</param>
        /// <returns>A list of expenses filtered to a specific month</returns>
        public List<Expense> LoadOfMonth(DateTime monthWanted)
        {
            return eRepository.LoadMonth(monthWanted);
        }

        #endregion

        #region Update Methods
        
        /// <summary>
        /// Updates the cache with the changes of the expense
        /// </summary>
        /// <param name="expenseToSave">The expense to be saved</param>
        public void Save(Expense expenseToSave)
        {
            eRepository.Save(expenseToSave);
        }

        #endregion

        #region Create Methods

        public int AddNewExpense(Expense newExpense)
        {
            var result = eRepository.Save(newExpense);
            return (result != null ? 1 : default(int));
        }

        #endregion

        #region Delete Region
        
        public void Delete(int id)
        {
            eRepository.Remove(id);
        }

        #endregion

        #endregion

        #region Other Methods

        public decimal GetMonthTotal(DateTime monthWanted)
        {
            return this.LoadOfMonth(monthWanted).Sum(curExpense => curExpense.Amount);
        }

        public decimal GetCategoryTotalForMonth(DateTime monthWanted, string categoryWanted)
        {
            return this.LoadOfMonth(monthWanted)
                .Where(curExpense => curExpense.Category.Name == categoryWanted)
                .Sum(curExpense => curExpense.Amount);
        }

        public Dictionary<string, decimal> GetAllCategoryTotals(DateTime monthWanted)
        {
            var categoryTotals = new Dictionary<string, decimal>();

            foreach (ExpenseCategory currCategory in ecHandler.LoadAll())
            {
                categoryTotals.Add(currCategory.Name, GetCategoryTotalForMonth(monthWanted, currCategory.Name));
            }

            return categoryTotals;
        }

        public decimal GetPaymentMethodTotalForMonth(DateTime monthWanted, string methodWanted)
        {
            return this.LoadOfMonth(monthWanted)
                .Where(curExpense => curExpense.Method.Name == methodWanted)
                .Sum(curExpense => curExpense.Amount);
        }

        public Dictionary<string, decimal> GetAllPaymentMethodTotals(DateTime monthWanted)
        {
            var methodTotals = new Dictionary<string, decimal>();

            foreach (PaymentMethod curMethod in pmHandler.LoadAll())
            {
                methodTotals.Add(curMethod.Name, GetPaymentMethodTotalForMonth(monthWanted, curMethod.Name));
            }

            return methodTotals;
        }

        #endregion
    }
}
