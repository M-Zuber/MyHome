using System;

namespace MyHome.DataClasses
{
    public class PaymentMethod : Category, IComparable<PaymentMethod>
    {
        public PaymentMethod()
        {
        }

        public PaymentMethod(int id, string name) : base(id, name)
        {
        }

        public int CompareTo(PaymentMethod other)
        {
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(PaymentMethod paymentMethod)
        {
            return ((Id == paymentMethod.Id) &&
                    (Name == paymentMethod.Name));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}