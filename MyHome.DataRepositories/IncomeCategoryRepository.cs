using System.Collections.Generic;
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
            return _context.IncomeCategories.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<IncomeCategory> GetAll()
        {
            return _context.IncomeCategories.ToList();
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
            _context.IncomeCategories.Add(incomeCategory);
            _context.SaveChanges();
        }
    }
}
