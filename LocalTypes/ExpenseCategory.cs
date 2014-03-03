using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalTypes
{
    public class ExpenseCategory
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

        public ExpenseCategory(int id, string name)
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

        public bool Equals(ExpenseCategory expenseCategory)
        {
            return ((this.ID == expenseCategory.ID) &&
                    (this.Name == expenseCategory.Name));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #endregion
    }
}
