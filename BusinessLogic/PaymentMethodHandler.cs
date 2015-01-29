using System.Collections.Generic;
using LocalTypes;
using System.Linq;

namespace BusinessLogic
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of payment methods
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class PaymentMethodHandler : BaseCategoryHandler
    {
        IRepository<PaymentMethod> repository;

        public PaymentMethodHandler(IRepository<PaymentMethod> repository)
        {
            this.repository = repository;
        }
        
        //TODO should the handler methods simply have a property of the appropiate access class?
        #region CRUD Methods

        #region Read Methods

        /// <summary>
        /// Loads a specific Payment Method from the cache
        /// </summary>
        /// <param name="id">The id of the Payment Method wanted</param>
        /// <returns>The Payment Method as it is in the cache</returns>
        public PaymentMethod LoadById(int id)
        {
            return this.repository.LoadById(id);
        }

        /// <summary>
        /// Loads all the Payment Methods from the cache
        /// </summary>
        /// <returns>All the Payment Methods as they are in the cache in generic-based
        /// list
        /// </returns>
        public override List<BaseCategory> LoadAll()
        {
            return this.repository.LoadAll().ToList<BaseCategory>();
        }

        #endregion

        #region Create Methods

        public override int AddNewCategory(string categoryName)
        {
            var result = this.repository.Save(new PaymentMethod { Name = categoryName });
            return result != null ? result.Id : 0;
        }

        #endregion

        #region Update Methods

        public override bool Save(BaseCategory categoryToSave)
        {
            var result = this.repository.Save(categoryToSave as PaymentMethod);
            return result != null;
        }

        #endregion

        #endregion

    }
}
