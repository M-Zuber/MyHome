using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyHome.DataClasses;
using MyHome.Persistence;

namespace MyHome.DataRepository
{
    public class IncomeRepository
    {
        private readonly AccountingDataContext _context;

        public IncomeRepository(AccountingDataContext context)
        {
            _context = context;
        }

        public Income GetById(int id)
        {
            return _context.Incomes
                            .Include(i => i.Category)
                            .Include(i => i.Method)
                            .FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Income> GetAll()
        {
            return _context.Incomes
                           .Include(i => i.Category)
                           .Include(i => i.Method).ToList();
        }

        public IEnumerable<Income> GetForMonthAndYear(int month, int year)
        {
            return _context.Incomes
                            .Include(i => i.Category)
                           .Include(i => i.Method)
                           .Where(i => i.Date.Month == month && i.Date.Year == year)
                           .ToList();
        }

        public void Remove(int id)
        {
            var income = _context.Incomes.FirstOrDefault(i => i.Id == id);
            _context.Incomes.Remove(income);
            _context.SaveChanges();
        }

        public void Save(Income income)
        {
            if (income.Id != 0)
            {
                Update(income);
            }
            else
            {
                Create(income);
            }
        }

        public void Update(Income income)
        {
            //TODO -there should be tests that cover this, but what happens if the item is a new item?
            CleanUpForEF(income);
            _context.Incomes.Attach(income);
            _context.SaveChanges();
        }

        public void Create(Income income)
        {
            if (income == null)
            {
                return;
            }
            CleanUpForEF(income);
            _context.Incomes.Add(income);
            _context.SaveChanges();
        }

        private void CleanUpForEF(Income income)
        {
            if (income.CategoryId > 0)
            {
                income.Category = null;
            }
            if (income.PaymentMethodId > 0)
            {
                income.Method = null;
            }
        }
    }
}
