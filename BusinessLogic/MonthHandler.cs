using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyHome2013.Core.LocalTypes;
using MyHome2013.Core.FrameWork;

namespace BusinessLogic
{
    public class MonthHandler
    {
        #region Properties

        public DateTime MonthRepresented { get; private set; } 
        
        #endregion

        IncomeHandler incomehandler;
        ExpenseHandler expensehandler;

        #region C'Tor

        public MonthHandler(IncomeHandler incomehandler, ExpenseHandler expensehandler, DateTime monthToRepresent)
        {
            this.incomehandler = incomehandler;
            this.expensehandler = expensehandler;
            MonthRepresented = monthToRepresent;
        }

        #endregion

        #region Expense Methods

        public List<Expense> GetAllExpenses()
        {
            return expensehandler.LoadOfMonth(MonthRepresented);
        }

        public decimal GetMonthesExpenseTotal()
        {
            return expensehandler.GetMonthTotal(MonthRepresented);
        }

        private decimal GetExpenseCategoryTotal(string categoryName)
        {
            return expensehandler.GetCategoryTotalForMonth(MonthRepresented, categoryName);
        }

        #endregion

        #region Income Methods
        
        public List<Income> GetAllIncomes()
        {
            return incomehandler.LoadOfMonth(MonthRepresented);
        }

        public decimal GetMonthesIncomeTotal()
        {
            return incomehandler.GetMonthTotal(MonthRepresented);
        }

        private decimal GetIncomeCategoryTotal(string categoryName)
        {
            return incomehandler.GetCategoryTotalForMonth(MonthRepresented, categoryName);
        }

        #endregion

        #region General Methods

        public decimal GetCategoryTotal(string categoryType, string categoryName) 
        {
            switch (categoryType.ToLower())
            {
                case("expense"):
                {
                    return GetExpenseCategoryTotal(categoryName);
                }
                case ("income"):
                {
                    return GetIncomeCategoryTotal(categoryName);
                }
                default:
                {
                    return 0;
                }
            }
        }

        public Dictionary<string, decimal> GetTotalsOfMonthByCategory()
        {
            Dictionary<string, decimal> categoryTotals = new Dictionary<string, decimal>();

            categoryTotals.Add("Total Expenses", GetMonthesExpenseTotal());
            categoryTotals.AddRange(expensehandler.GetAllCategoryTotals(MonthRepresented));

            categoryTotals.Add("Total Income", GetMonthesIncomeTotal());
            foreach (KeyValuePair<string, decimal> curIncomeCatTotal in incomehandler.GetAllCategoryTotals(MonthRepresented))
            {
                if (categoryTotals.ContainsKey(curIncomeCatTotal.Key))
                {
                    decimal placeholder = categoryTotals[curIncomeCatTotal.Key];
                    categoryTotals.Remove(curIncomeCatTotal.Key);
                    categoryTotals.Add(string.Format("{0} - {1}", curIncomeCatTotal.Key, "Expense"), placeholder);
                    categoryTotals.Add(string.Format("{0} - {1}", curIncomeCatTotal.Key, "Income"), curIncomeCatTotal.Value);
                }
                else
                {
                    categoryTotals.Add(curIncomeCatTotal.Key, curIncomeCatTotal.Value);
                }
            }

            return categoryTotals;
        }

        #endregion
    }
}
