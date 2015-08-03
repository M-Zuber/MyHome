using System;
using System.Collections.Generic;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Infrastructure.Validation;

namespace MyHome.Services
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of payment methods
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class PaymentMethodService : ICategoryService
    {
        private readonly PaymentMethodRepository _repository;

        public PaymentMethodService(PaymentMethodRepository repository)
        {
            _repository = repository;
        }

        public PaymentMethod GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Delete(string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));
            _repository.RemoveByName(name);
        }

        /// <summary>
        /// Loads all the Payment Methods from the cache
        /// </summary>
        /// <returns>All the Payment Methods as they are in the cache in generic-based
        /// list
        /// </returns>
        public IEnumerable<Category> GetAll()
        {
            return _repository.GetAll();
        }

        public void Create(PaymentMethod paymentMethod)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(paymentMethod.Name));
           _repository.Create(paymentMethod);
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
                throw new Exception(string.Format("Payment method '{0}' is already defined", name));
            }
            _repository.Create(new PaymentMethod(id, name));
        }

        public void Save(int id, string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));

            if (Exists(name))
            {
                throw new Exception(string.Format("Payment method '{0}' is already defined", name));
            }

            var category = _repository.GetById(id);
            category.Name = name;
            _repository.Save(category);
        }
    }
}