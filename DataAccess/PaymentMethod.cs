using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameWork;

namespace DataAccess
{
    public class PaymentMethodEntity
    {
        #region Properties

        /// <summary>
        /// The name (type) of the payment method category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id number of the payment method category in the table
        /// </summary>
        public int ID { get; internal set; }

        #endregion

        #region C'Tor

        public PaymentMethodEntity(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        #endregion

        #region CRUD Methods

        #region Read Methods

        public static PaymentMethodEntity LoadById(int id)
        {
            StaticDataSet.t_payment_methodsRow requestedRow =
                Cache.SDB.t_payment_methods.FindByID(id);
            return new PaymentMethodEntity(requestedRow.ID, requestedRow.NAME);
        }

        public static List<PaymentMethodEntity> LoadAll()
        {
            List<PaymentMethodEntity> allPaymentMethods = new List<PaymentMethodEntity>();

            foreach (StaticDataSet.t_payment_methodsRow currPaymentMethod in Cache.SDB.t_payment_methods.Rows)
            {
                allPaymentMethods.Add(
                    new PaymentMethodEntity(currPaymentMethod.ID, currPaymentMethod.NAME));
            }

            return allPaymentMethods;
        }

        #endregion

        #endregion

        #region Other Methods

        #region Override Methods

        public override string ToString()
        {
            return this.Name;
        }

        public override bool Equals(object obj)
        {
            PaymentMethodEntity methodComparing = (PaymentMethodEntity)obj;

            return ((this.ID == methodComparing.ID) &&
                    (this.Name == methodComparing.Name));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #endregion
    }
}
