namespace BL
{
    /// <summary>
    /// Allows access to the local table type - mainly used for saving the sum totals
    /// </summary>
    public class CategorySumBL
    {
        #region Properties

        /// <summary>
        /// Key for access to the corrosponding data in the table
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The value in the table
        /// </summary>
        public int Value { get; set; }
        
        #endregion

        #region C'tor

        /// <summary>
        /// Ctor that recieves the initial vlaues 
        /// for the key and the value
        /// </summary>
        /// <param name="strKey">The key for data access</param>
        /// <param name="nValue">The value of the data</param>
        public CategorySumBL(string strKey, int nValue)
        {
            this.Key = strKey;
            this.Value = nValue;
        }
        
        #endregion
    }
}
