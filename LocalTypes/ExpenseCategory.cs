using System;
namespace LocalTypes
{
    public class ExpenseCategory : BaseCategory, IComparable<ExpenseCategory>
    {
        #region C'Tor
        
        public ExpenseCategory()
        {
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
