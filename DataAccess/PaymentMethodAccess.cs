using System.Collections.Generic;
using Data;
using LocalTypes;

namespace DataAccess
{
    /// <summary>
    /// Contains methods neccesary for CRUD methods of payment methods
    /// -Before sending to the next tier translates into Local types
    /// </summary>
    public class PaymentMethodAccess : BaseCategoryAccess
	{
        #region CRUD Methods

        #region Read Methods

        /// <summary>
        /// Loads a specific payment method from the cache
        /// </summary>
        /// <param name="id">The id of the payment method wanted</param>
        /// <returns>The payment method as it is in the cache</returns>
        public static PaymentMethod LoadById(int id)
        {
            StaticDataSet.t_payment_methodsRow requestedRow =
                Cache.SDB.t_payment_methods.FindByID(id);
            return new PaymentMethod(requestedRow.ID, requestedRow.NAME);
        }

        /// <summary>
        /// Loads all the payment methods from the cache
        /// </summary>
        /// <returns>All the payment methods as they are in the cache in generic-based
        /// list
        /// </returns>
        public override List<BaseCategory> LoadAll()
        {
            List<BaseCategory> allPaymentMethods = new List<BaseCategory>();

            foreach (StaticDataSet.t_payment_methodsRow currPaymentMethod in Cache.SDB.t_payment_methods.Rows)
            {
                allPaymentMethods.Add(
                    new PaymentMethod(currPaymentMethod.ID, currPaymentMethod.NAME));
            }

            return allPaymentMethods;
        }

        #endregion

        #region Create Methods

        public override int AddNewCategory(string categoryName)
        {
            StaticDataSet.t_payment_methodsRow newPaymentMethod = Cache.SDB.t_payment_methods.Newt_payment_methodsRow();
            newPaymentMethod.ID = this.GetNextId();
            newPaymentMethod.NAME = categoryName;

            Cache.SDB.t_payment_methods.Addt_payment_methodsRow(newPaymentMethod);

            return newPaymentMethod.ID;
        }

        #endregion
        
        #endregion

        #region Other Methods

        internal override void UpdateDataBase(BaseCategory categoryTranslating)
        {
            StaticDataSet.t_payment_methodsRow translatedRow = Cache.SDB.t_payment_methods.FindByID(categoryTranslating.Id);

            //Because this form is only for updating, there is no check if it exists in the database

            translatedRow.ID = categoryTranslating.Id;
            translatedRow.NAME = categoryTranslating.Name;
        }

        #endregion
    }
}
