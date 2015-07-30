using System;

namespace MyHome.DataClasses
{
    public class ExpenseCategory : Category, IComparable<ExpenseCategory>
    {
        public ExpenseCategory()
        {
        }

        public ExpenseCategory(int id, string name) : base(id, name)
        {
        }

        public int CompareTo(ExpenseCategory other)
        {
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(ExpenseCategory expenseCategory)
        {
            return ((Id == expenseCategory.Id) &&
                    (Name == expenseCategory.Name));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}