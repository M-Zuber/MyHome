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
            return _context.Incomes.Include(i => i.Category)
                           .Include(i => i.Method).Where(i => i.Date.Month == month
                && i.Date.Year == year).ToList();
        }

        public void Remove(int id)
        {
            var income = _context.Incomes.Find(id);
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
            _context.Incomes.Attach(income);
            _context.SaveChanges();
        }

        public void Create(Income income)
        {
            _context.Incomes.Add(income);
            _context.SaveChanges();
        }
    }
}
