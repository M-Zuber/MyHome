﻿using System;
using System.Collections.Generic;
using System.Linq;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Infrastructure;
using MyHome.Persistence;
using MyHome.Infrastructure.Validation;

namespace MyHome.Services
{
    public class MonthService
    {
        private readonly ExpenseService _expenseService;
        private readonly IncomeService _incomeService;

        public MonthService(AccountingDataContext accountingDataContext)
        {
            _expenseService = new ExpenseService(new ExpenseRepository(accountingDataContext));
            _incomeService = new IncomeService(new IncomeRepository(accountingDataContext));
        }

        public List<Expense> GetExpensesForMonth(DateTime month)
        {
            return _expenseService.LoadOfMonth(month).ToList();
        }

        private decimal GetTotalExpenseForMonth(DateTime month)
        {
            return _expenseService.GetMonthTotal(month);
        }

        private decimal GetExpenseTotalForCategoryAndMonth(string categoryName, DateTime month)
        {
            return _expenseService.GetCategoryTotalForMonth(month, categoryName);
        }

        public List<Income> GetIncomesForMonth(DateTime month)
        {
            return _incomeService.LoadOfMonth(month).ToList();
        }

        private decimal GetTotalIncomeForMonth(DateTime month)
        {
            return _incomeService.GetMonthTotal(month);
        }

        private decimal GetTotalIncomeForCategoryAndMonth(string categoryName, DateTime month)
        {
            return _incomeService.GetCategoryTotalForMonth(month, categoryName);
        }

        public decimal GetTotalForCategoryAndMonth(string categoryType, string categoryName, DateTime month)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(categoryType), "The category type must be specified");
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(categoryName), "The category name must be specified");

            // ReSharper disable once PossibleNullReferenceException
            switch (categoryType.ToLower())
            {
                case "expense":
                    {
                        return GetExpenseTotalForCategoryAndMonth(categoryName, month);
                    }
                case "income":
                    {
                        return GetTotalIncomeForCategoryAndMonth(categoryName, month);
                    }
                default:
                    {
                        return 0;
                    }
            }
        }

        public Dictionary<string, decimal> GetTotalFlowPerCategoriesForMonth(DateTime month)
        {
            var categoryTotals = new Dictionary<string, decimal>
            {
                {"Total Expenses", GetTotalExpenseForMonth(month)}
            };

            categoryTotals.AddRange(_expenseService.GetAllCategoryTotals(month));

            categoryTotals.Add("Total Income", GetTotalIncomeForMonth(month));
            foreach (var totalIncomeByCategory in _incomeService.GetAllCategoryTotals(month))
            {
                if (categoryTotals.ContainsKey(totalIncomeByCategory.Key))
                {
                    var placeholder = categoryTotals[totalIncomeByCategory.Key];
                    categoryTotals.Remove(totalIncomeByCategory.Key);
                    categoryTotals.Add($"{totalIncomeByCategory.Key} - Expense", placeholder);
                    categoryTotals.Add($"{totalIncomeByCategory.Key} - Income", totalIncomeByCategory.Value);
                }
                else
                {
                    categoryTotals.Add(totalIncomeByCategory.Key, totalIncomeByCategory.Value);
                }
            }

            return categoryTotals;
        }
    }
}
