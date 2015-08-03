using System;
using System.Collections.Generic;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Infrastructure.Validation;

namespace MyHome.Services
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of expense categories
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class ExpenseCategoryService : ICategoryService
    {
        private readonly ExpenseCategoryRepository _repository;

        public ExpenseCategoryService(ExpenseCategoryRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Loads a specific Expense Category from the cache
        /// </summary>
        /// <param name="id">The id of the Expense category wanted</param>
        /// <returns>The expense category as it is in the cache</returns>
        public ExpenseCategory GetById(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Loads all the Expense Categories from the cache
        /// </summary>
        /// <returns>All the Expense Categories as they are in the cache in generic-based
        /// list
        /// </returns>
        public IEnumerable<Category> GetAll()
        {
            return _repository.GetAll();
        }

        public void Create(ExpenseCategory expenseCategory)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(expenseCategory.Name));
            _repository.Create(expenseCategory);
        }
        
        public bool Exists(string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));
            return _repository.GetByName(name) != null;
        }

        public void Create(string name, int id = 0)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));
            if (Exists(name))
            {
                throw new Exception(string.Format("Expense category '{0}' is already defined", name));
            }
            _repository.Create(new ExpenseCategory(id, name));
        }

        public void Delete(string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));
            _repository.RemoveByName(name);
        }

        public void Save(int id, string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));

            if (Exists(name))
            {
                throw new Exception(string.Format("Expense category '{0}' is already defined", name));
            }

            var category = _repository.GetById(id);
            category.Name = name;
            _repository.Save(category);
        }
    }
}
