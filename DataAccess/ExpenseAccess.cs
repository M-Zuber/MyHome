using System.Collections.Generic;
using FrameWork;
using LocalTypes;

namespace DataAccess
{
    /// <summary>
    /// Contains methods neccesary for CRUD methods of expenses
    /// -Before sending to the next tier translates into Local types
    /// </summary>
    public static class ExpenseAccess
    {
        #region CRUD Methods

        #region Read Methods

        /// <summary>
        /// Loads a specific Expense from the cache
        /// </summary>
        /// <param name="id">The id of the Expense wanted</param>
        /// <returns>The expense as it is in the cache</returns>
        public static Expense LoadById(int id)
        {
            StaticDataSet.t_expensesRow requestedRow =
                Cache.SDB.t_expenses.FindByID((uint)id);
            return new Expense(requestedRow.AMOUNT, requestedRow.EXP_DATE,
                ExpenseCategoryAccess.LoadById(requestedRow.CATEGORY), 
                PaymentMethodAccess.LoadById(requestedRow.METHOD), requestedRow.COMMENTS, requestedRow.ID);
        }

        /// <summary>
        /// Loads all the Expenses from the cache
        /// </summary>
        /// <returns>All the Expenses as they are in the cache in generic-based
        /// list
        /// </returns>
        public static List<Expense> LoadAll()
        {
            List<Expense> allExpenses = new List<Expense>();

            foreach (StaticDataSet.t_expensesRow currExpense in Cache.SDB.t_expenses.Rows)
            {
                allExpenses.Add(TranslateFromDataRow(currExpense));
            }

            return allExpenses;
        }

        #endregion

        #region Update Methods

        /// <summary>
        /// Updates the cache with the changes of the expense
        /// </summary>
        /// <param name="expenseToSave">The expense to be saved</param>
        /// <returns>True on success, false on any exception</returns>
        public static bool Save(Expense expenseToSave)
        {
            try
            {
                UpdateDataBase(expenseToSave);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #endregion

        #region Other Methods

        /// <summary>
        /// Updates the row in the database that corrosponds to the expense entity
        /// passed in
        /// </summary>
        /// <param name="expenseTranslating">The expense entity to update the database with</param>
        private static void UpdateDataBase(Expense expenseTranslating)
        {
            StaticDataSet.t_expensesRow translatedRow = Cache.SDB.t_expenses.FindByID(expenseTranslating.ID);

            //Because this form is only for updating, there is no check if it exists in the database

            translatedRow.ID = expenseTranslating.ID;
            translatedRow.AMOUNT = expenseTranslating.Amount;
            translatedRow.COMMENTS = expenseTranslating.Comment;
            translatedRow.EXP_DATE = expenseTranslating.Date;

            // There is no check to see if they exist in the database or not
            // because as of 20.02.2014 the form only shows categories/methods
            // that already exist - and do not allow the user to create new ones
            translatedRow.CATEGORY = (uint)expenseTranslating.Category.Id;
            translatedRow.METHOD = expenseTranslating.Method.Id;
        }

        /// <summary>
        /// Translates an expense from the tabular data in the cache to entity form
        /// </summary>
        /// <param name="rowTranslating">The expense row to be translated</param>
        /// <returns>The entity representation of the expense row passed in</returns>
        internal static Expense TranslateFromDataRow(StaticDataSet.t_expensesRow rowTranslating)
        {
            // Check for DBNull
            if (rowTranslating.IsCOMMENTSNull())
            {
                rowTranslating.COMMENTS = string.Empty;
            }

            return new Expense(rowTranslating.AMOUNT, rowTranslating.EXP_DATE,
                ExpenseCategoryAccess.LoadById(rowTranslating.CATEGORY),
                PaymentMethodAccess.LoadById(rowTranslating.METHOD), rowTranslating.COMMENTS, rowTranslating.ID);
        }

        #endregion
    }
}
