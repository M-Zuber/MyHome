using System.Collections.Generic;
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
            return _context.ExpenseCategories.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<ExpenseCategory> GetAll()
        {
            return _context.ExpenseCategories.ToList();
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
            _context.ExpenseCategories.Attach(expenseCategory);
            _context.SaveChanges();
        }

        public void Create(ExpenseCategory expenseCategory)
        {
            _context.ExpenseCategories.Add(expenseCategory);
            _context.SaveChanges();
        }
    }
}
