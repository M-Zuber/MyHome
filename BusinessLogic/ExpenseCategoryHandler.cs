using System.Collections.Generic;
using System.Linq;
using DataAccess;
using LocalTypes;

namespace BusinessLogic
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of expense categories
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class ExpenseCategoryHandler : BaseCategoryHandler
    {
        #region CRUD Methods

        #region Read Methods

        /// <summary>
        /// Loads a specific Expense Category from the cache
        /// </summary>
        /// <param name="id">The id of the Expense category wanted</param>
        /// <returns>The expense category as it is in the cache</returns>
        public static ExpenseCategory LoadById(int id)
        {
            return ExpenseCategoryAccess.LoadById(id);
        }

        /// <summary>
        /// Loads all the Expense Categories from the cache
        /// </summary>
        /// <returns>All the Expense Categories as they are in the cache in generic-based
        /// list
        /// </returns>
        public override List<BaseCategory> LoadAll()
        {
            return (new ExpenseCategoryAccess()).LoadAll();
        }

        #endregion

        #region Create Methods

        public override int AddNewCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName) || base.DoesNameExist(categoryName))
            {
                return -1;
            }

            return (new ExpenseCategoryAccess()).AddNewCategory(categoryName);
        }

        #endregion

        #region Update Methods

        public override bool Save(BaseCategory categoryToSave)
        {
            return (new ExpenseCategoryAccess()).Save(categoryToSave);
        }

        #endregion

        #endregion
    }
}
