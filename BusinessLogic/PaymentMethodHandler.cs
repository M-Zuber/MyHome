using System.Collections.Generic;
using DataAccess;
using LocalTypes;

namespace BusinessLogic
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of payment methods
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class PaymentMethodHandler
    {
        #region CRUD Methods

        #region Read Methods

        /// <summary>
        /// Loads a specific Payment Method from the cache
        /// </summary>
        /// <param name="id">The id of the Payment Method wanted</param>
        /// <returns>The Payment Method as it is in the cache</returns>
        public static PaymentMethod LoadById(int id)
        {
            return PaymentMethodAccess.LoadById(id);
        }

        /// <summary>
        /// Loads all the Payment Methods from the cache
        /// </summary>
        /// <returns>All the Payment Methods as they are in the cache in generic-based
        /// list
        /// </returns>
        public static List<PaymentMethod> LoadAll()
        {
            return PaymentMethodAccess.LoadAll();
        }

        #endregion

        #endregion
    }
}
