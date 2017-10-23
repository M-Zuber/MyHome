using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyHome.DataClasses;
using MyHome.Persistence;

namespace MyHome.DataRepository
{
    public class ExpenseRepository
    {
        private readonly AccountingDataContext _context;

        public ExpenseRepository(AccountingDataContext context)
        {
            _context = context;
        }

        public Expense GetById(int id)
        {
            return _context.Expenses
                            .Include(i => i.Category)
                            .Include(i => i.Method)
                            .FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Expense> GetAll()
        {
            return _context.Expenses
                           .Include(i => i.Category)
                           .Include(i => i.Method).ToList();
        }

        public IEnumerable<Expense> GetForMonthAndYear(int month, int year)
        {
            return _context.Expenses.Include(i => i.Category)
                                    .Include(i => i.Method)
                                    .Where(i => i.Date.Month == month && i.Date.Year == year)
                                    .ToList();
        }

        public void Remove(int id)
        {
            var expense = _context.Expenses.FirstOrDefault(e => e.Id == id);
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
        }

        public void Save(Expense expense)
        {
            if (expense.Id != 0)
            {
                Update(expense);
            }
            else
            {
                Create(expense);
            }
        }

        public void Update(Expense expense)
        {
            CleanUpForEF(expense);
            _context.Expenses.Attach(expense);
            _context.SaveChanges();
        }

        public void Create(Expense expense)
        {
            if (expense == null)
            {
                return;
            }
            CleanUpForEF(expense);
            _context.Expenses.Add(expense);
            _context.SaveChanges();
        }

        private static void CleanUpForEF(Expense expense)
        {
            if (expense.CategoryId > 0)
            {
                expense.Category = null;
            }
            if (expense.PaymentMethodId > 0)
            {
                expense.Method = null;
            }
        }
    }
}
