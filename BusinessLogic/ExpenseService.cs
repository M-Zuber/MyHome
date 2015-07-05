using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using LocalTypes;

namespace BusinessLogic
{
    public class ExpenseService
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
            _repository.Save(expenseToSave);
        }

        public void Create(Expense newExpense)
        {
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
            return LoadOfMonth(monthWanted)
                .Where(curExpense => curExpense.Category.Name == categoryWanted)
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
            return LoadOfMonth(monthWanted)
                .Where(curExpense => curExpense.Method.Name == methodWanted)
                .Sum(curExpense => curExpense.Amount);
        }

        public Dictionary<string, decimal> GetAllPaymentMethodTotals(DateTime monthWanted)
        {
            return LoadOfMonth(monthWanted)
                .GroupBy(e => e.Method.Name)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
        }
    }
}
