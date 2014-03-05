namespace LocalTypes
{
    public class IncomeCategory : BaseCategory
    {
        #region C'Tor

        public IncomeCategory(int id, string name)
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
    }
}
