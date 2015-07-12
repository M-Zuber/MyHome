﻿using System.Collections.Generic;
using MyHome.DataClasses;
using MyHome.DataRepository;

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
        public ExpenseCategory LoadById(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Loads all the Expense Categories from the cache
        /// </summary>
        /// <returns>All the Expense Categories as they are in the cache in generic-based
        /// list
        /// </returns>
        public IEnumerable<Category> LoadAll()
        {
            return _repository.GetAll();
        }

        public void Create(ExpenseCategory expenseCategory)
        {
            _repository.Create(expenseCategory);
        }

        
        public void Save(ExpenseCategory expenseCategory)
        {
            _repository.Save(expenseCategory);
        }

        public bool Exists(string name)
        {
            return _repository.GetByName(name) != null;
        }

        public void Add(string name)
        {
            Save(new ExpenseCategory(0, name));
        }
    }
}
