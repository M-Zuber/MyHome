using System;
namespace LocalTypes
{
    public class IncomeCategory : BaseCategory, IComparable<IncomeCategory>
    {
        #region C'Tor

        public IncomeCategory()
        {
        }

        #endregion

        #region Other Methods

        #region Override Methods

        public override string ToString()
        {
            return this.Name;
        }

        public bool Equals(IncomeCategory incomeCategory)
        {
            return ((this.Id == incomeCategory.Id) &&
                    (this.Name == incomeCategory.Name));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #endregion

        public int CompareTo(IncomeCategory other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
