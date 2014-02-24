using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameWork;
using LocalTypes;

namespace DataAccess
{
    public class ExpenseEntity
    {
        #region Properties

        /// <summary>
        /// The amount of the expense
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// The date of the expense
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Category of the expense
        /// </summary>
        public ExpenseCategory Category { get; set; }

        /// <summary>
        /// How the expense was payed
        /// </summary>
        public PaymentMethod Method { get; set; }

        /// <summary>
        /// Additional info about the expense
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// ID number of the expense in the data table
        /// </summary>
        public uint ID { get; private set; }

        #endregion

        #region C'Tor

        public ExpenseEntity(double amount, DateTime date, ExpenseCategory expenseCategory,
            PaymentMethod paymentMethod, string comment, uint id)
        {
            this.Amount = amount;
            this.Category = expenseCategory;
            this.Comment = comment;
            this.Date = date;
            this.ID = id;
            this.Method = paymentMethod;
        }

        #endregion

        #region CRUD Methods

        #region Read Methods

        public static ExpenseEntity LoadById(int id)
        {
            StaticDataSet.t_expensesRow requestedRow =
                Cache.SDB.t_expenses.FindByID((uint)id);
            return new ExpenseEntity(requestedRow.AMOUNT, requestedRow.EXP_DATE,
                ExpenseCategoryAccess.LoadById(requestedRow.CATEGORY), 
                PaymentMethodAccess.LoadById(requestedRow.METHOD), requestedRow.COMMENTS, requestedRow.ID);
        }

        public static List<ExpenseEntity> LoadAll()
        {
            List<ExpenseEntity> allExpensesCategories = new List<ExpenseEntity>();

            foreach (StaticDataSet.t_expensesRow currExpense in Cache.SDB.t_expenses.Rows)
            {
                allExpensesCategories.Add(TranslateFromDataRow(currExpense));
            }

            return allExpensesCategories;
        }

        public static List<ExpenseEntity> LoadOfMonth(DateTime monthWanted)
        {
            List<ExpenseEntity> allExpensesCategories = new List<ExpenseEntity>();

            foreach (StaticDataSet.t_expensesRow currExpense in Cache.SDB.t_expenses.Rows)
            {
                if ((currExpense.EXP_DATE.Month == monthWanted.Month) &&
                    (currExpense.EXP_DATE.Year == monthWanted.Year))
                {
                    allExpensesCategories.Add(TranslateFromDataRow(currExpense)); 
                }
            }

            return allExpensesCategories;
        }

        #endregion

        #region Update Methods

        public bool Save()
        {
            try
            {
                UpdateDataBase(this);
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

        #region Override Methods

        public override bool Equals(object obj)
        {
            ExpenseEntity expenseComparing = (ExpenseEntity)obj;

            return ((this.Amount == expenseComparing.Amount) &&
                    (this.Category.Equals(expenseComparing.Category)) &&
                    (this.Comment == expenseComparing.Comment) &&
                    (this.Date == expenseComparing.Date) &&
                    (this.ID == expenseComparing.ID) &&
                    (this.Method.Equals(expenseComparing.Method)));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        public StaticDataSet.t_expensesRow UpdateDataBase(ExpenseEntity expenseTranslating)
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

            return translatedRow;
        }

        public static ExpenseEntity TranslateFromDataRow(StaticDataSet.t_expensesRow rowTranslating)
        {
            if (rowTranslating.IsCOMMENTSNull())
            {
                rowTranslating.COMMENTS = string.Empty;
            }

            return new ExpenseEntity(rowTranslating.AMOUNT, rowTranslating.EXP_DATE,
                ExpenseCategoryAccess.LoadById(rowTranslating.CATEGORY),
                PaymentMethodAccess.LoadById(rowTranslating.METHOD), rowTranslating.COMMENTS, rowTranslating.ID);
        }

        public ExpenseEntity Copy()
        {
            return new ExpenseEntity(this.Amount, this.Date,
                this.Category, this.Method, this.Comment, this.ID);
        }

        #endregion
    }
}
