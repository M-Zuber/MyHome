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

        public int CompareTo(PaymentMethod other) => string.Compare(Name, other.Name, StringComparison.Ordinal);

        public override string ToString() => Name;

        public bool Equals(PaymentMethod paymentMethod) => Id == paymentMethod.Id && Name == paymentMethod.Name;
    }
}