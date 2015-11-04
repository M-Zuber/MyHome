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
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name));
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
            Contract.Requires<ArgumentException>(paymentMethod != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(paymentMethod.Name));
            Contract.Requires<ArgumentException>(!Exists(paymentMethod.Name), $"Payment method '{paymentMethod.Name}' is already defined");

            _repository.Create(paymentMethod);
        }

        public bool Exists(string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name));
            return _repository.GetByName(name) != null;
        }

        public void Create(string name, int id = 0)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name));
            Contract.Requires<ArgumentException>(!Exists(name), $"Payment method '{name}' is already defined");

            _repository.Create(new PaymentMethod(id, name));
        }

        public void Save(int id, string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name));
            Contract.Requires<ArgumentException>(!Exists(name), $"Payment method '{name}' is already defined");

            var paymentMethod = _repository.GetById(id);
            if (paymentMethod == null)
            {
                paymentMethod = new PaymentMethod();
            }
            paymentMethod.Name = name;
            _repository.Save(paymentMethod);
        }
    }
}