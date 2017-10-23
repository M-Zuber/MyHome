using System;
using System.Collections.Generic;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Infrastructure.Validation;

namespace MyHome.Services
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of income categories
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class IncomeCategoryService : ICategoryService<IncomeCategory>
    {
        private readonly IncomeCategoryRepository _repository;

        public IncomeCategoryService(IncomeCategoryRepository repository)
        {
            _repository = repository;
        }

        public IncomeCategory GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Delete(string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name));
            _repository.RemoveByName(name);
        }

        public IEnumerable<Category> GetAll()
        {
            return _repository.GetAll();
        }

        public bool Exists(string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name));
            return _repository.GetByName(name) != null;
        }

        public IncomeCategory Create(IncomeCategory incomeCategory)
        {
            Contract.Requires<ArgumentException>(incomeCategory != null);
            // ReSharper disable once PossibleNullReferenceException
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(incomeCategory.Name));
            Contract.Requires<ArgumentException>(!Exists(incomeCategory.Name), $"Income category '{incomeCategory.Name}' is already defined");

            _repository.Create(incomeCategory);

            return incomeCategory;
        }

        public IncomeCategory Create(string name, int id = 0)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name));
            Contract.Requires<ArgumentException>(!Exists(name), $"Income category '{name}' is already defined");

            var incomeCategory = new IncomeCategory(id, name);

            _repository.Create(incomeCategory);

            return incomeCategory;
        }

        public void Save(int id, string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name));
            Contract.Requires<ArgumentException>(!Exists(name), $"Income category '{name}' is already defined");

            var category = new IncomeCategory { Id = id, Name = name };
            _repository.Save(category);
        }
    }
}
