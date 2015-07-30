using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyHome.DataClasses;
using MyHome.Persistence;

namespace MyHome.DataRepository
{
    public class ExpenseCategoryRepository
    {
        private readonly AccountingDataContext _context;

        public ExpenseCategoryRepository(AccountingDataContext context)
        {
            _context = context;
        }

        public ExpenseCategory GetById(int id)
        {
            return _context.ExpenseCategories.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public ExpenseCategory GetByName(string name)
        {
            return _context.ExpenseCategories.AsNoTracking().FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<ExpenseCategory> GetAll()
        {
            return _context.ExpenseCategories.AsNoTracking().ToList();
        }
        
        public void Save(ExpenseCategory expenseCategory)
        {
            if (expenseCategory.Id != 0)
            {
                Update(expenseCategory);
            }
            else
            {
                Create(expenseCategory);
            }
        }

        public void Update(ExpenseCategory expenseCategory)
        {
            var existing = _context.ExpenseCategories.Find(expenseCategory.Id);
            existing.Name = expenseCategory.Name;
            _context.SaveChanges();
        }

        public void Create(ExpenseCategory expenseCategory)
        {
            _context.ExpenseCategories.Add(expenseCategory);
            _context.SaveChanges();
        }

        public void RemoveByName(string name)
        {
            var existing = _context.ExpenseCategories.FirstOrDefault(x => x.Name == name);
            if (existing == null) return;
            _context.ExpenseCategories.Remove(existing);
        }
    }
}
