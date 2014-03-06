using System.Collections.Generic;
using Old_FrameWork;
using LocalTypes;

namespace DataAccess
{
    /// <summary>
    /// Contains methods neccesary for CRUD methods of incomes
    /// -Before sending to the next tier translates into Local types
    /// </summary>
    public static class IncomeAccess
    {
        #region CRUD Methods

        #region Read Methods

        /// <summary>
        /// Loads a specific income from the cache
        /// </summary>
        /// <param name="id">The id of the income wanted</param>
        /// <returns>The income as it is in the cache</returns>
        public static Income LoadById(int id)
        {
            StaticDataSet.t_incomesRow requestedRow =
                Cache.SDB.t_incomes.FindByID((uint)id);
            return new Income(requestedRow.AMOUNT, requestedRow.INC_DATE,
                IncomeCategoryAccess.LoadById(requestedRow.CATEGORY), 
                PaymentMethodAccess.LoadById(requestedRow.METHOD), requestedRow.COMMENTS, requestedRow.ID);
        }

        /// <summary>
        /// Loads all the incomes from the cache
        /// </summary>
        /// <returns>All the incomes as they are in the cache in generic-based
        /// list
        /// </returns>
        public static List<Income> LoadAll()
        {
            List<Income> allIncomes = new List<Income>();

            foreach (StaticDataSet.t_incomesRow currIncome in Cache.SDB.t_incomes.Rows)
            {
                allIncomes.Add(TranslateFromDataRow(currIncome));
            }

            return allIncomes;
        }

        #endregion

        #region Update Methods

        /// <summary>
        /// Updates the cache with the changes of the income
        /// </summary>
        /// <param name="expenseToSave">The income to be saved</param>
        /// <returns>True on success, false on any exception</returns>
        public static bool Save(Income incomeToSave)
        {
            try
            {
                UpdateDataBase(incomeToSave);
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
        /// Updates the row in the database that corrosponds to the income entity
        /// passed in
        /// </summary>
        /// <param name="incomeTranslating">The income entity to update the database with</param>
        private static void UpdateDataBase(Income incomeTranslating)
        {
            StaticDataSet.t_incomesRow translatedRow = Cache.SDB.t_incomes.FindByID(incomeTranslating.ID);

            //Because this form is only for updating, there is no check if it exists in the database

            translatedRow.ID = incomeTranslating.ID;
            translatedRow.AMOUNT = incomeTranslating.Amount;
            translatedRow.COMMENTS = incomeTranslating.Comment;
            translatedRow.INC_DATE = incomeTranslating.Date;

            // There is no check to see if they exist in the database or not
            // because as of 20.02.2014 the form only shows categories/methods
            // that already exist - and do not allow the user to create new ones
            translatedRow.CATEGORY = incomeTranslating.Category.Id;
            translatedRow.METHOD = incomeTranslating.Method.Id;
        }

        /// <summary>
        /// Translates an income from the tabular data in the cache to entity form
        /// </summary>
        /// <param name="rowTranslating">The income row to be translated</param>
        /// <returns>The entity representation of the income row passed in</returns>
        internal static Income TranslateFromDataRow(StaticDataSet.t_incomesRow rowTranslating)
        {
            // Check for DBNull
            if (rowTranslating.IsCOMMENTSNull())
            {
                rowTranslating.COMMENTS = string.Empty;
            }

            return new Income(rowTranslating.AMOUNT, rowTranslating.INC_DATE,
                IncomeCategoryAccess.LoadById(rowTranslating.CATEGORY),
                PaymentMethodAccess.LoadById(rowTranslating.METHOD), rowTranslating.COMMENTS, rowTranslating.ID);
        }

        #endregion
    }
}
