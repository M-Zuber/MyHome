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

        public int CompareTo(ExpenseCategory other) => string.Compare(Name, other.Name, StringComparison.Ordinal);

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(ExpenseCategory expenseCategory) => Id == expenseCategory.Id && Name == expenseCategory.Name;
    }
}