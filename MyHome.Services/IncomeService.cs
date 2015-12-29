using System;
using System.Collections.Generic;
using System.Linq;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Infrastructure.Validation;
using static MyHome.Services.TransactionExtensions;

namespace MyHome.Services
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of incomes
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class IncomeService : ITransactionService
    {
        private readonly IncomeRepository _repository;

        public IncomeService(IncomeRepository repository)
        {
            _repository = repository;
        }

        public Income LoadById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Income> LoadAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Income> LoadOfMonth(DateTime monthWanted)
        {
            return _repository.GetForMonthAndYear(monthWanted.Month, monthWanted.Year);
        }

        public void Save(Income incomeToSave)
        {
            Contract.Requires<ArgumentNullException>(incomeToSave != null, "The income must not be null");
            Contract.Requires<ArgumentNullException>(incomeToSave.CategoryId > 0, "There must be a category selected");
            Contract.Requires<ArgumentNullException>(incomeToSave.PaymentMethodId > 0, "There must be a payment method selected");
            _repository.Save(incomeToSave);
        }

        public void Create(Income newIncome)
        {
            Contract.Requires<ArgumentNullException>(newIncome != null, "The income must not be null");
            Contract.Requires<ArgumentNullException>(newIncome.CategoryId > 0, "There must be a category selected");
            Contract.Requires<ArgumentNullException>(newIncome.PaymentMethodId > 0, "There must be a payment method selected");

            _repository.Create(newIncome);
        }

        public void Delete(int id)
        {
            _repository.Remove(id);
        }

        public decimal GetMonthTotal(DateTime monthWanted)
        {
            return LoadOfMonth(monthWanted).Sum(i => i.Amount);
        }

        public decimal GetCategoryTotalForMonth(DateTime monthWanted, string categoryWanted)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(categoryWanted), "The category must have a value");
            return LoadOfMonth(monthWanted)
                .Where(curIncome => curIncome.Category.Name.Equals(categoryWanted, StringComparison.OrdinalIgnoreCase))
                .Sum(curIncome => curIncome.Amount);
        }

        public Dictionary<string, decimal> GetAllCategoryTotals(DateTime monthWanted)
        {
            return LoadOfMonth(monthWanted)
                .GroupBy(i => i.Category.Name)
                .ToDictionary(g => g.Key, g => g.Sum(i => i.Amount));
        }

        public decimal GetPaymentMethodTotalForMonth(DateTime monthWanted, string methodWanted)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(methodWanted), "The method must have a value");
            return LoadOfMonth(monthWanted)
                .Where(curIncome => curIncome.Method.Name.Equals(methodWanted, StringComparison.OrdinalIgnoreCase))
                .Sum(curIncome => curIncome.Amount);
        }

        public Dictionary<string, decimal> GetAllPaymentMethodTotals(DateTime monthWanted)
        {
            return LoadOfMonth(monthWanted)
                .GroupBy(i => i.Method.Name)
                .ToDictionary(g => g.Key, g => g.Sum(i => i.Amount));
        }

        #region ITransactionService Members

        void ITransactionService.Create(Transaction transaction)
        {
            var income = TryParseToIncome(transaction);
            Create(income);
        }

        IEnumerable<Transaction> ITransactionService.GetAll()
        {
            return LoadAll();
        }

        void ITransactionService.Save(Transaction transaction)
        {
            var income = TryParseToIncome(transaction);
            Save(income);
        }

        #endregion
    }
}
