using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalTypes;
using DataAccess;

namespace BusinessLogic
{
    public class PaymentMethodHandler
    {
        #region CRUD Methods

        #region Read Methods

        public static PaymentMethod LoadById(int id)
        {
            return PaymentMethodAccess.LoadById(id);
        }

        public static List<PaymentMethod> LoadAll()
        {
            return PaymentMethodAccess.LoadAll();
        }

        #endregion

        #endregion
    }
}
