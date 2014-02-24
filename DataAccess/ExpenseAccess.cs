using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameWork;
using LocalTypes;

namespace DataAccess
{
    public static class ExpenseAccess
    {
        #region CRUD Methods

        #region Read Methods

        public static Expense LoadById(int id)
        {
            StaticDataSet.t_expensesRow requestedRow =
                Cache.SDB.t_expenses.FindByID((uint)id);
            return new Expense(requestedRow.AMOUNT, requestedRow.EXP_DATE,
                ExpenseCategoryAccess.LoadById(requestedRow.CATEGORY), 
                PaymentMethodAccess.LoadById(requestedRow.METHOD), requestedRow.COMMENTS, requestedRow.ID);
        }

        public static List<Expense> LoadAll()
        {
            List<Expense> allExpensesCategories = new List<Expense>();

            foreach (StaticDataSet.t_expensesRow currExpense in Cache.SDB.t_expenses.Rows)
            {
                allExpensesCategories.Add(TranslateFromDataRow(currExpense));
            }

            return allExpensesCategories;
        }

        #endregion

        #region Update Methods

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

        public static void UpdateDataBase(Expense expenseTranslating)
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
            translatedRow.CATEGORY = (uint)expenseTranslating.Category.ID;
            translatedRow.METHOD = expenseTranslating.Method.ID;
        }

        public static Expense TranslateFromDataRow(StaticDataSet.t_expensesRow rowTranslating)
        {
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
