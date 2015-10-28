using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyHome.DataClasses;
using MyHome.Persistence;

namespace MyHome.DataRepository
{
    public class IncomeCategoryRepository
    {
        private readonly AccountingDataContext _context;

        public IncomeCategoryRepository(AccountingDataContext context)
        {
            _context = context;
        }

        public IncomeCategory GetById(int id)
        {
            return _context.IncomeCategories.AsNoTracking().FirstOrDefault(i => i.Id == id);
        }

        public IncomeCategory GetByName(string name)
        {
            return _context.IncomeCategories.AsNoTracking().FirstOrDefault(i => i.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<IncomeCategory> GetAll()
        {
            return _context.IncomeCategories.AsNoTracking().ToList();
        }

        public void Save(IncomeCategory incomeCategory)
        {
            if (incomeCategory.Id != 0)
            {
                Update(incomeCategory);
            }
            else
            {
                Create(incomeCategory);
            }
        }

        public void Update(IncomeCategory incomeCategory)
        {
            _context.IncomeCategories.Attach(incomeCategory);
            _context.SaveChanges();
        }

        public void Create(IncomeCategory incomeCategory)
        {
            if (incomeCategory == null)
            {
                return;
            }
            _context.IncomeCategories.Add(incomeCategory);
            _context.SaveChanges();
        }

        public void RemoveByName(string name)
        {
            var existing = _context.IncomeCategories.FirstOrDefault(i => i.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
            if (existing == null) return;
            _context.IncomeCategories.Remove(existing);
        }
    }
}
