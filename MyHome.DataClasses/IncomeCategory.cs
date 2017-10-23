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

        public int CompareTo(IncomeCategory other) => string.Compare(Name, other.Name, StringComparison.Ordinal);

        public override string ToString() => Name;

        public bool Equals(IncomeCategory incomeCategory) => Id == incomeCategory.Id && Name == incomeCategory.Name;
    }
}