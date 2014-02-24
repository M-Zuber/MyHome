using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalTypes
{
    public class PaymentMethod
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

        public PaymentMethod(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        #endregion

        #region Other Methods

        #region Override Methods

        public override string ToString()
        {
            return this.Name;
        }

        public bool Equals(PaymentMethod paymentMethod)
        {
            return ((this.ID == paymentMethod.ID) &&
                    (this.Name == paymentMethod.Name));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #endregion
    }
}
