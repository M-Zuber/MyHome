using System;
namespace MyHome2013.Core.LocalTypes
{
    public class PaymentMethod : BaseCategory, IComparable<PaymentMethod>
    {
        #region C'Tor

        public PaymentMethod() { }

        #endregion

        #region Other Methods

        #region Override Methods

        public override string ToString()
        {
            return this.Name;
        }

        public bool Equals(PaymentMethod paymentMethod)
        {
            return ((this.Id == paymentMethod.Id) &&
                    (this.Name == paymentMethod.Name));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #endregion

        public int CompareTo(PaymentMethod other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
