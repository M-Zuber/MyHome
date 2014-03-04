namespace LocalTypes
{
    public class IncomeCategory
    {
        #region Properties

        /// <summary>
        /// The name (type) of the expense category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id number of the expense category in the table
        /// </summary>
        public int ID { get; internal set; }

        #endregion

        #region C'Tor

        public IncomeCategory(int id, string name)
        {
            this.ID = id;
            this.Name = name;
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
            return ((this.ID == incomeCategory.ID) &&
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
