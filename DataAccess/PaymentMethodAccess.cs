using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameWork;
using LocalTypes;

namespace DataAccess
{
    public class PaymentMethodAccess
	{
        #region CRUD Methods

        #region Read Methods

        public static PaymentMethod LoadById(int id)
        {
            StaticDataSet.t_payment_methodsRow requestedRow =
                Cache.SDB.t_payment_methods.FindByID(id);
            return new PaymentMethod(requestedRow.ID, requestedRow.NAME);
        }

        public static List<PaymentMethod> LoadAll()
        {
            List<PaymentMethod> allPaymentMethods = new List<PaymentMethod>();

            foreach (StaticDataSet.t_payment_methodsRow currPaymentMethod in Cache.SDB.t_payment_methods.Rows)
            {
                allPaymentMethods.Add(
                    new PaymentMethod(currPaymentMethod.ID, currPaymentMethod.NAME));
            }

            return allPaymentMethods;
        }

        #endregion

        #endregion
	}
}
