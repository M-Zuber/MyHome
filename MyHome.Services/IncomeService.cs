﻿using System;
using System.Collections.Generic;
using System.Linq;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Infrastructure.Validation;

namespace MyHome.Services
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of incomes
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class IncomeService: ITransactionService
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
            _repository.Save(incomeToSave);
        }

        public void Create(Income newIncome)
        {
            Contract.Requires<ArgumentException>(newIncome.Category != null, "There must be a category selected");
            Contract.Requires<ArgumentException>(newIncome.Method != null, "There must be a payment method selected");

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
            return LoadOfMonth(monthWanted)
                .Where(curIncome => curIncome.Category.Name == categoryWanted)
                .Sum(curIncome => curIncome.Amount);
        }

        public Dictionary<string, decimal> GetTotalIncomeByCategories(DateTime monthWanted)
        {
            return LoadOfMonth(monthWanted)
                .GroupBy(i => i.Category.Name)
                .ToDictionary(g => g.Key, g => g.Sum(i => i.Amount));
        }

        public decimal GetPaymentMethodTotalForMonth(DateTime monthWanted, string methodWanted)
        {
            return LoadOfMonth(monthWanted)
                .Where(curIncome => curIncome.Method.Name == methodWanted)
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
            var income = transaction as Income;
            if (income == null)
            {
                throw new ArgumentException("The transaction is the wrong type");
            }
            income.Category = new IncomeCategory(transaction.Category?.Id ?? 0, transaction.Category?.Name ?? "");
            Create(income);
        }

        IEnumerable<Transaction> ITransactionService.GetAll()
        {
            return LoadAll();
        }

        #endregion
    }
}
