using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHome.TestUtils;
using System.Collections.Generic;
using MyHome.DataClasses;
using System.Linq;

namespace MyHome.DataRepositories.Tests
{
    [TestClass]
    public class GeneralCategoryHandlerTests
    {
        private readonly List<IncomeCategory> _incomeCategoryData = new List<IncomeCategory> { new IncomeCategory(1, "first"), new IncomeCategory(2, "second") };
        private readonly List<ExpenseCategory> _expenseCategoryData = new List<ExpenseCategory> { new ExpenseCategory(3, "third"), new ExpenseCategory(4, "fourth") };

        [TestMethod]
        public void GeneralCategoryHandler_GetAllNames_With_No_Duplicates()
        {
            var mock = RepositoryMocks.GetMockGeneralCategoryHandler(_expenseCategoryData, _incomeCategoryData);

            var expected = new List<string> { "Total Expenses", "third", "fourth", "Total Income", "first", "second" };
            var actual = mock.GetAllCategoryNames();

            CollectionAssert.AreEqual(expected, actual.ToList());
        }

        [TestMethod]
        public void GeneralCategoryHandler_GetAllNames_With_Duplicates_In_Expense()
        {
            var moreExpenseCategoryData = _incomeCategoryData.Select(i => new ExpenseCategory(i.Id * 2, i.Name));
            var mock = RepositoryMocks.GetMockGeneralCategoryHandler(_expenseCategoryData.Concat(moreExpenseCategoryData).ToList(), _incomeCategoryData);

            var expected = new List<string> { "Total Expenses", "third", "fourth", "first - Expense", "second - Expense", "Total Income", "first - Income", "second - Income" };
            var actual = mock.GetAllCategoryNames();

            CollectionAssert.AreEqual(expected, actual.ToList());
        }

        [TestMethod]
        public void GeneralCategoryHandler_GetAllNames_With_Duplicates_In_Income()
        {
            var moreIncomeCategoryData = _expenseCategoryData.Select(i => new IncomeCategory(i.Id * 2, i.Name));
            var mock = RepositoryMocks.GetMockGeneralCategoryHandler(_expenseCategoryData, _incomeCategoryData.Concat(moreIncomeCategoryData).ToList());

            var expected = new List<string> { "Total Expenses", "third - Expense", "fourth - Expense", "Total Income", "first", "second", "third - Income", "fourth - Income" };
            var actual = mock.GetAllCategoryNames();

            CollectionAssert.AreEqual(expected, actual.ToList());
        }

        [TestMethod]
        public void GeneralCategoryHandler_GetAllNames_With_Duplicates_In_Both()
        {
            var moreIncomeCategoryData = _expenseCategoryData.Select(i => new IncomeCategory(i.Id * 2, i.Name));
            var moreExpenseCategoryData = _incomeCategoryData.Select(i => new ExpenseCategory(i.Id * 3, i.Name));

            var mock = RepositoryMocks.GetMockGeneralCategoryHandler(_expenseCategoryData.Concat(moreExpenseCategoryData).ToList(), _incomeCategoryData.Concat(moreIncomeCategoryData).ToList());

            var expected = new List<string> { "Total Expenses", "third - Expense", "fourth - Expense", "first - Expense", "second - Expense", "Total Income", "first - Income", "second - Income", "third - Income", "fourth - Income" };
            var actual = mock.GetAllCategoryNames();

            CollectionAssert.AreEqual(expected, actual.ToList());
        }

        [TestMethod]
        public void GeneralCategoryHandler_GetAllNames_With_No_Categories()
        {
            var mock = RepositoryMocks.GetMockGeneralCategoryHandler();

            var expected = new List<string> { "Total Expenses", "Total Income" };
            var actual = mock.GetAllCategoryNames();

            CollectionAssert.AreEqual(expected, actual.ToList());
        }

        [TestMethod]
        public void GeneralCategoryHandler_GetAllNames_Expense_Categories()
        {
            var mock = RepositoryMocks.GetMockGeneralCategoryHandler(_expenseCategoryData);

            var expected = new List<string> { "third", "fourth" };
            var actual = mock.GetAllCategoryNames("expense");

            CollectionAssert.AreEqual(expected, actual.ToList());
        }

        [TestMethod]
        public void GeneralCategoryHandler_GetAllNames_Expense_Categories_No_Catgories()
        {
            var mock = RepositoryMocks.GetMockGeneralCategoryHandler();

            var expected = new List<string>();
            var actual = mock.GetAllCategoryNames("expense");

            CollectionAssert.AreEqual(expected, actual.ToList());
        }

        [TestMethod]
        public void GeneralCategoryHandler_GetAllNames_Income_Categories()
        {
            var mock = RepositoryMocks.GetMockGeneralCategoryHandler(incomeCategories: _incomeCategoryData);

            var expected = new List<string> { "first", "second" };
            var actual = mock.GetAllCategoryNames("income");

            CollectionAssert.AreEqual(expected, actual.ToList());
        }

        [TestMethod]
        public void GeneralCategoryHandler_GetAllNames_Income_Categories_No_Categories()
        {
            var mock = RepositoryMocks.GetMockGeneralCategoryHandler();

            var expected = new List<string>();
            var actual = mock.GetAllCategoryNames("income");

            CollectionAssert.AreEqual(expected, actual.ToList());
        }

        [TestMethod]
        public void GeneralCategoryHandler_GetAllNames_Bad_Category_Type()
        {
            var mock = RepositoryMocks.GetMockGeneralCategoryHandler();

            var expected = new List<string>();
            var actual = mock.GetAllCategoryNames("bad");

            CollectionAssert.AreEqual(expected, actual.ToList());
        }
    }
}
