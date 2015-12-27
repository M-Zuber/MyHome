using System;

namespace MyHome.DataClasses
{
    public class IncomeCategory : Category, IComparable<IncomeCategory>
    {
        public IncomeCategory()
        {
        }

        public IncomeCategory(int id, string name) : base(id, name)
        {
        }

        public int CompareTo(IncomeCategory other)
        {
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(IncomeCategory incomeCategory)
        {
            return ((Id == incomeCategory.Id) &&
                    (Name == incomeCategory.Name));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}