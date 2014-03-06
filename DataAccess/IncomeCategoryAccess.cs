using System.Collections.Generic;
using Old_FrameWork;
using LocalTypes;

namespace DataAccess
{
    /// <summary>
    /// Contains methods neccesary for CRUD methods of income categories
    /// -Before sending to the next tier translates into Local types
    /// </summary>
    public class IncomeCategoryAccess : BaseCategoryAccess
    {
        #region CRUD Methods

        #region Read Methods

        /// <summary>
        /// Loads a specific Income category from the cache
        /// </summary>
        /// <param name="id">The id of the Income category wanted</param>
        /// <returns>The Income category as it is in the cache</returns>
        public static IncomeCategory LoadById(int id)
        {
            StaticDataSet.t_incomes_categoryRow requestedRow =
                Cache.SDB.t_incomes_category.FindByID(id);
            return new IncomeCategory(requestedRow.ID, requestedRow.NAME);
        }

        /// <summary>
        /// Loads all the Income categories from the cache
        /// </summary>
        /// <returns>All the Income categories as they are in the cache in generic-based
        /// list
        /// </returns>
        public override List<BaseCategory> LoadAll()
        {
            List<BaseCategory> allIncomeCategories = new List<BaseCategory>();

            foreach (StaticDataSet.t_incomes_categoryRow currIncomeCategory in Cache.SDB.t_incomes_category.Rows)
            {
                allIncomeCategories.Add(
                    new IncomeCategory(currIncomeCategory.ID, currIncomeCategory.NAME));
            }

            return allIncomeCategories;
        }

        #endregion

        #endregion

        #region Other Methods

        internal override void UpdateDataBase(BaseCategory categoryTranslating)
        {
            StaticDataSet.t_incomes_categoryRow translatedRow = Cache.SDB.t_incomes_category.FindByID(categoryTranslating.Id);

            //Because this form is only for updating, there is no check if it exists in the database

            translatedRow.ID = categoryTranslating.Id;
            translatedRow.NAME = categoryTranslating.Name;
        }

        #endregion
    }
}
