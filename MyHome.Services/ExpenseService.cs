using System;
using System.Collections.Generic;
using System.Linq;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Infrastructure.Validation;
using static MyHome.Services.TransactionExtensions;

namespace MyHome.Services
{
    public class ExpenseService : ITransactionService
    {
        private readonly ExpenseRepository _repository;

        public ExpenseService(ExpenseRepository repository)
        {
            _repository = repository;
        }

        public Expense LoadById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Expense> LoadAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Expense> LoadOfMonth(DateTime month)
        {
            return _repository.GetForMonthAndYear(month.Month, month.Year);
        }

        public void Save(Expense expenseToSave)
        {
            Contract.Requires<ArgumentNullException>(expenseToSave != null, "The expense must not be null");
            Contract.Requires<ArgumentNullException>(expenseToSave.Category != null, "There must be a category selected");
            Contract.Requires<ArgumentNullException>(expenseToSave.Method != null, "There must be a payment method selected");
            _repository.Save(expenseToSave);
        }

        public void Create(Expense newExpense)
        {
            Contract.Requires<ArgumentNullException>(newExpense != null, "The expense must not be null");
            Contract.Requires<ArgumentNullException>(newExpense.Category != null, "There must be a category selected");
            Contract.Requires<ArgumentNullException>(newExpense.Method != null, "There must be a payment method selected");

            _repository.Create(newExpense);
        }

        public void Delete(int id)
        {
            _repository.Remove(id);
        }

        public decimal GetMonthTotal(DateTime monthWanted)
        {
            return LoadOfMonth(monthWanted).Sum(e => e.Amount);
        }

        public decimal GetCategoryTotalForMonth(DateTime monthWanted, string categoryWanted)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(categoryWanted), "The category must have a value");
            return LoadOfMonth(monthWanted)
                .Where(curExpense => curExpense.Category.Name.Equals(categoryWanted, StringComparison.OrdinalIgnoreCase))
                .Sum(curExpense => curExpense.Amount);
        }

        public Dictionary<string, decimal> GetAllCategoryTotals(DateTime monthWanted)
        {
            return LoadOfMonth(monthWanted)
                .GroupBy(e => e.Category.Name)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
        }

        public decimal GetPaymentMethodTotalForMonth(DateTime monthWanted, string methodWanted)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(methodWanted), "The method must have a value");
            return LoadOfMonth(monthWanted)
                .Where(curExpense => curExpense.Method.Name.Equals(methodWanted, StringComparison.OrdinalIgnoreCase))
                .Sum(curExpense => curExpense.Amount);
        }

        public Dictionary<string, decimal> GetAllPaymentMethodTotals(DateTime monthWanted)
        {
            return LoadOfMonth(monthWanted)
                .GroupBy(e => e.Method.Name)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
        }

        #region ITransactionService Members

        void ITransactionService.Create(Transaction transaction)
        {
            var expense = TryParseToExpense(transaction);
            Create(expense);
        }

        IEnumerable<Transaction> ITransactionService.GetAll()
        {
            return LoadAll();
        }

        void ITransactionService.Save(Transaction transaction)
        {
            var expense = TryParseToExpense(transaction);
            Save(expense);
        }

        #endregion
    }
}
