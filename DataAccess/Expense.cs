using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameWork;

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
        public ExpenseCategoryEntity Category { get; set; }

        /// <summary>
        /// How the expense was payed
        /// </summary>
        public PaymentMethodEntity Method { get; set; }

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

        public ExpenseEntity(double amount, DateTime date, ExpenseCategoryEntity expenseCategory,
            PaymentMethodEntity paymentMethod, string comment, uint id)
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
                ExpenseCategoryEntity.LoadById(requestedRow.CATEGORY), 
                PaymentMethodEntity.LoadById(requestedRow.METHOD), requestedRow.COMMENTS, requestedRow.ID);
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

        #endregion

        #region Other Methods

        #region Override Methods

        public override bool Equals(object obj)
        {
            ExpenseEntity expenseComparing = (ExpenseEntity)obj;

            return ((this.Amount == expenseComparing.Amount) &&
                    (this.Category == expenseComparing.Category) &&
                    (this.Comment == expenseComparing.Comment) &&
                    (this.Date == expenseComparing.Date) &&
                    (this.ID == expenseComparing.ID) &&
                    (this.Method == expenseComparing.Method));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        public static ExpenseEntity TranslateFromDataRow(StaticDataSet.t_expensesRow rowTranslating)
        {
            if (rowTranslating.IsCOMMENTSNull())
            {
                rowTranslating.COMMENTS = string.Empty;
            }

            return new ExpenseEntity(rowTranslating.AMOUNT, rowTranslating.EXP_DATE,
                ExpenseCategoryEntity.LoadById(rowTranslating.CATEGORY),
                PaymentMethodEntity.LoadById(rowTranslating.METHOD), rowTranslating.COMMENTS, rowTranslating.ID);
        }

        public ExpenseEntity Copy()
        {
            return new ExpenseEntity(this.Amount, this.Date,
                this.Category, this.Method, this.Comment, this.ID);
        }

        #endregion
    }
}
