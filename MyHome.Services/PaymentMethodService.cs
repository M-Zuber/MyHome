using System.Collections.Generic;
using MyHome.DataClasses;
using MyHome.DataRepository;

namespace MyHome.Services
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of payment methods
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class PaymentMethodService
    {
        private readonly PaymentMethodRepository _repository;

        public PaymentMethodService(PaymentMethodRepository repository)
        {
            _repository = repository;
        }

        public PaymentMethod LoadById(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Loads all the Payment Methods from the cache
        /// </summary>
        /// <returns>All the Payment Methods as they are in the cache in generic-based
        /// list
        /// </returns>
        public IEnumerable<BaseCategory> LoadAll()
        {
            return _repository.GetAll();
        }

        public void Create(PaymentMethod paymentMethod)
        {
           _repository.Create(paymentMethod);
        }


        public void Save(PaymentMethod paymentMethod)
        {
            _repository.Save(paymentMethod);
        }
    }
}