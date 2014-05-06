using System.Collections.Generic;
using Old_FrameWork;
using LocalTypes;

namespace DataAccess
{
    /// <summary>
    /// Contains methods neccesary for CRUD methods of expense categories
    /// -Before sending to the next tier translates into Local types
    /// </summary>
    public class ExpenseCategoryAccess : BaseCategoryAccess
    {
        #region CRUD Methods

        #region Read Methods

        /// <summary>
        /// Loads a specific Expense category from the cache
        /// </summary>
        /// <param name="id">The id of the Expense category wanted</param>
        /// <returns>The expense category as it is in the cache</returns>
        public static ExpenseCategory LoadById(int id)
        {
            StaticDataSet.t_expenses_categoryRow requestedRow =
                Cache.SDB.t_expenses_category.FindByID(id);
            return new ExpenseCategory(requestedRow.ID, requestedRow.NAME);
        }

        /// <summary>
        /// Loads all the Expense categories from the cache
        /// </summary>
        /// <returns>All the Expense categories as they are in the cache in generic-based
        /// list
        /// </returns>
        public override List<BaseCategory> LoadAll()
        {
            List<BaseCategory> allExpensesCategories = new List<BaseCategory>();

            foreach (StaticDataSet.t_expenses_categoryRow currExpenseCategory in Cache.SDB.t_expenses_category.Rows)
            {
                allExpensesCategories.Add(
                    new ExpenseCategory((int)currExpenseCategory.ID, currExpenseCategory.NAME));
            }

            return allExpensesCategories;
        }

        #endregion

        #region Create Methods

        public override int AddNewCategory(string categoryName)
        {
            StaticDataSet.t_expenses_categoryRow newPaymentMethod = Cache.SDB.t_expenses_category.Newt_expenses_categoryRow();
            newPaymentMethod.ID = this.GetNextId();
            newPaymentMethod.NAME = categoryName;

            Cache.SDB.t_expenses_category.Addt_expenses_categoryRow(newPaymentMethod);

            return newPaymentMethod.ID;
        }

        #endregion

        #endregion

        #region Other Methods

        internal override void UpdateDataBase(BaseCategory categoryTranslating)
        {
            StaticDataSet.t_expenses_categoryRow translatedRow = Cache.SDB.t_expenses_category.FindByID(categoryTranslating.Id);

            //Because this form is only for updating, there is no check if it exists in the database

            translatedRow.ID = categoryTranslating.Id;
            translatedRow.NAME = categoryTranslating.Name;
        }

        #endregion
    }
}
