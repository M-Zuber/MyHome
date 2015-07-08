using System;

namespace MyHome.DataClasses
{
    public class ExpenseCategory : BaseCategory, IComparable<ExpenseCategory>
    {
        #region C'Tor

        public ExpenseCategory(int id, string name)
        {
            base.Id = id;
            base.Name = name;
        }

        #endregion

        #region Other Methods

        #region Override Methods

        public override string ToString()
        {
            return this.Name;
        }

        public bool Equals(ExpenseCategory expenseCategory)
        {
            return ((this.Id == expenseCategory.Id) &&
                    (this.Name == expenseCategory.Name));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #endregion

        public int CompareTo(ExpenseCategory other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
