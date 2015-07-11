using System;
using System.Collections.Generic;
using System.Linq;
using MyHome.DataClasses;
using MyHome.DataRepository;

namespace MyHome.Services
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of incomes
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class IncomeService
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
    }
}
