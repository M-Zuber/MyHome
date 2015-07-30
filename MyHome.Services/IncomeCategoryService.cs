﻿using System;
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
    public class IncomeCategoryService : ICategoryService
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

        public void Remove(string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));
            _repository.RemoveByName(name);
        }

        public IEnumerable<Category> GetAll()
        {
            return _repository.GetAll();
        }
        
        public bool Exists(string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));
            return _repository.GetByName(name) != null;
        }

        public void Add(string name)
        {
            if (Exists(name))
            {
                throw new Exception(string.Format("Income category '{0}' is already defined", name));
            }

            Save(new IncomeCategory(0, name));
        }

        public void Update(int id, string name)
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

        private void Save(IncomeCategory category)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(category.Name));
            _repository.Save(category);
        }
    }
}
