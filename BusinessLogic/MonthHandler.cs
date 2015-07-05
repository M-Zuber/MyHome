using System;
using System.Collections.Generic;
using LocalTypes;
using FrameWork;

namespace BusinessLogic
{
    public class MonthHandler
    {

        public DateTime MonthRepresented { get; private set; } 
        

        public MonthHandler(DateTime monthToRepresent)
        {
            MonthRepresented = monthToRepresent;
        }


        #region Expense Methods

        public List<Expense> GetAllExpenses()
        {
            return new List<Expense>();
            //return ExpenseService.LoadOfMonth(MonthRepresented);
        }

        public double GetMonthesExpenseTotal()
        {
            return 0;
            //return ExpenseService.GetMonthTotal(MonthRepresented);
        }

        private double GetExpenseCategoryTotal(string categoryName)
        {
            return 0;
            //return ExpenseService.GetCategoryTotalForMonth(MonthRepresented, categoryName);
        }

        #endregion

        #region Income Methods
        
        public List<Income> GetAllIncomes()
        {
            return new List<Income>();
            //return IncomeService.LoadOfMonth(MonthRepresented);
        }

        public double GetMonthesIncomeTotal()
        {
            return 0;
            //return IncomeService.GetMonthTotal(MonthRepresented);
        }

        private double GetIncomeCategoryTotal(string categoryName)
        {
            return 0;
            //return IncomeService.GetCategoryTotalForMonth(MonthRepresented, categoryName);
        }

        #endregion

        #region General Methods

        public double GetCategoryTotal(string categoryType, string categoryName) 
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

        public Dictionary<string, double> GetTotalsOfMonthByCategory()
        {
            return new Dictionary<string, double>();

            //Dictionary<string, double> categoryTotals = new Dictionary<string, double>();

            //categoryTotals.Add("Total Expenses", GetMonthesExpenseTotal());
            //categoryTotals.AddRange(ExpenseService.GetAllCategoryTotals(MonthRepresented));

            //categoryTotals.Add("Total Income", GetMonthesIncomeTotal());            
            //foreach (KeyValuePair<string, double> curIncomeCatTotal in IncomeService.GetAllCategoryTotals(MonthRepresented))
            //{
            //    if (categoryTotals.ContainsKey(curIncomeCatTotal.Key))
            //    {
            //        double placeholder = categoryTotals[curIncomeCatTotal.Key];
            //        categoryTotals.Remove(curIncomeCatTotal.Key);
            //        categoryTotals.Add(string.Format("{0} - {1}", curIncomeCatTotal.Key, "Expense"), placeholder);
            //        categoryTotals.Add(string.Format("{0} - {1}", curIncomeCatTotal.Key, "Income"), curIncomeCatTotal.Value);
            //    }
            //    else
            //    {
            //        categoryTotals.Add(curIncomeCatTotal.Key, curIncomeCatTotal.Value);
            //    }
            //}

            //return categoryTotals;
        }

        #endregion
    }
}
