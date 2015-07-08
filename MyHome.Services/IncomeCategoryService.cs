using System.Collections.Generic;
using MyHome.DataClasses;
using MyHome.DataRepository;

namespace MyHome.Services
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of income categories
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class IncomeCategoryService
    {
        private readonly IncomeCategoryRepository _repository;

        public IncomeCategoryService(IncomeCategoryRepository repository)
        {
            _repository = repository;
        }

        public IncomeCategory LoadById(int id)
        {
            return _repository.GetById(id);
        }

        
        public IEnumerable<BaseCategory> LoadAll()
        {
            return _repository.GetAll();
        }
        
        public void AddNewCategory(IncomeCategory category)
        {
            _repository.Create(category);
        }
        
        public void Save(IncomeCategory category)
        {
            _repository.Save(category);
        }
    }
}
