using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHome.DataClasses;
using MyHome.TestUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHome.Services.Tests
{
    [TestClass]
    public class MonthServiceTests
    {
        private Expense expenseBaseTestData = new Expense(10, new DateTime(2015, 2, 2), new ExpenseCategory(1, "test"), new PaymentMethod(1, "test"), "");
        private Income incomeBaseTestData = new Income(10, new DateTime(2015, 2, 2), new IncomeCategory(1, "test"), new PaymentMethod(1, "test"), "");

        [TestMethod]
        public void MonthService_GetExpensesForMonth_No_Matching_Data_Returns_Empty_List()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = expenseBaseTestData.Copy();
                                     c.Id = i;
                                     c.Date = new DateTime(2015, 3, 3);
                                     c.Amount = (decimal)Math.Pow(i, i);
                                     return c;
                                 })
                                 .ToList();
            var mock = ServiceMocks.GetMockMonthService(data);

            var actual = mock.GetExpensesForMonth(new DateTime(2010, 1, 1));

            CollectionAssert.AreEquivalent(new List<Expense>(), actual.ToList());
        }

        [TestMethod]
        public void MonthService_GetExpensesForMonth_Only_Month_Matches_Returns_Empty_List()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = expenseBaseTestData.Copy();
                                     c.Id = i;
                                     c.Date = new DateTime(2015, 1, 3);
                                     c.Amount = (decimal)Math.Pow(i, i);
                                     return c;
                                 })
                                 .ToList();
            var mock = ServiceMocks.GetMockMonthService(data);

            var actual = mock.GetExpensesForMonth(new DateTime(2010, 1, 1));

            CollectionAssert.AreEquivalent(new List<Expense>(), actual.ToList());
        }

        [TestMethod]
        public void MonthService_GetExpensesForMonth_Only_Year_Matches_Returns_Empty_List()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = expenseBaseTestData.Copy();
                                     c.Id = i;
                                     c.Date = new DateTime(2010, 3, 3);
                                     c.Amount = (decimal)Math.Pow(i, i);
                                     return c;
                                 })
                                 .ToList();
            var mock = ServiceMocks.GetMockMonthService(data);

            var actual = mock.GetExpensesForMonth(new DateTime(2010, 1, 1));

            CollectionAssert.AreEquivalent(new List<Expense>(), actual.ToList());
        }

        [TestMethod]
        public void MonthService_GetExpensesForMonth_Returns_All_Matching_Data()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = expenseBaseTestData.Copy();
                                     c.Id = i;
                                     c.Date = new DateTime(2015, 3, 3);
                                     c.Amount = (decimal)Math.Pow(i, i);
                                     return c;
                                 })
                                 .ToList();
            var expected = new List<Expense>(data);

            data = data.Concat(Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = expenseBaseTestData.Copy();
                                     c.Id = i;
                                     c.Date = new DateTime(2012, 6, 3);
                                     c.Amount = (decimal)Math.Pow(i, i);
                                     return c;
                                 }))
                                 .ToList();

            var mock = ServiceMocks.GetMockMonthService(data);

            var actual = mock.GetExpensesForMonth(new DateTime(2015, 3, 1));

            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        public void MonthService_GetIncomesForMonth_No_Matching_Data_Returns_Empty_List()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = incomeBaseTestData.Copy();
                                     c.Id = i;
                                     c.Date = new DateTime(2015, 3, 3);
                                     c.Amount = (decimal)Math.Pow(i, i);
                                     return c;
                                 })
                                 .ToList();
            var mock = ServiceMocks.GetMockMonthService(incomeData: data);

            var actual = mock.GetIncomesForMonth(new DateTime(2010, 1, 1));

            CollectionAssert.AreEquivalent(new List<Income>(), actual.ToList());
        }

        [TestMethod]
        public void MonthService_GetIncomesForMonth_Only_Month_Matches_Returns_Empty_List()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = incomeBaseTestData.Copy();
                                     c.Id = i;
                                     c.Date = new DateTime(2015, 1, 3);
                                     c.Amount = (decimal)Math.Pow(i, i);
                                     return c;
                                 })
                                 .ToList();
            var mock = ServiceMocks.GetMockMonthService(incomeData: data);

            var actual = mock.GetIncomesForMonth(new DateTime(2010, 1, 1));

            CollectionAssert.AreEquivalent(new List<Income>(), actual.ToList());
        }

        [TestMethod]
        public void MonthService_GetIncomesForMonth_Only_Year_Matches_Returns_Empty_List()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = incomeBaseTestData.Copy();
                                     c.Id = i;
                                     c.Date = new DateTime(2010, 3, 3);
                                     c.Amount = (decimal)Math.Pow(i, i);
                                     return c;
                                 })
                                 .ToList();
            var mock = ServiceMocks.GetMockMonthService(incomeData: data);

            var actual = mock.GetIncomesForMonth(new DateTime(2010, 1, 1));

            CollectionAssert.AreEquivalent(new List<Income>(), actual.ToList());
        }

        [TestMethod]
        public void MonthService_GetIncomesForMonth_Returns_All_Matching_Data()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = incomeBaseTestData.Copy();
                                     c.Id = i;
                                     c.Date = new DateTime(2015, 3, 3);
                                     c.Amount = (decimal)Math.Pow(i, i);
                                     return c;
                                 })
                                 .ToList();
            var expected = new List<Income>(data);

            data = data.Concat(Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = incomeBaseTestData.Copy();
                                     c.Id = i;
                                     c.Date = new DateTime(2012, 6, 3);
                                     c.Amount = (decimal)Math.Pow(i, i);
                                     return c;
                                 }))
                                 .ToList();

            var mock = ServiceMocks.GetMockMonthService(incomeData: data);

            var actual = mock.GetIncomesForMonth(new DateTime(2015, 3, 1));

            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MonthService_GetTotalForCategoryAndMonth_CategoryType_Is_Null_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockMonthService();
            mock.GetTotalForCategoryAndMonth(null, "something", DateTime.Today);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MonthService_GetTotalForCategoryAndMonth_CategoryType_Is_Whitespace_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockMonthService();
            mock.GetTotalForCategoryAndMonth("\r\t\n\t", "something", DateTime.Today);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MonthService_GetTotalForCategoryAndMonth_CategoryName_Is_Null_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockMonthService();
            mock.GetTotalForCategoryAndMonth("something", null, DateTime.Today);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MonthService_GetTotalForCategoryAndMonth_CategoryName_Is_Whitespace_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockMonthService();
            mock.GetTotalForCategoryAndMonth("something", string.Empty, DateTime.Today);
        }

        [TestMethod]
        public void MonthService_GetTotalForCategoryAndMonth_CategoryType_Expense_No_Matching_Category_Returns_Zero()
        {
            var expenseData = Enumerable.Range(1, 5)
                                        .Select(i => new Expense { Amount = 1, Date = DateTime.Today, Category = new ExpenseCategory(1, "test") })
                                        .ToList();
            var incomeData = Enumerable.Range(1, 5)
                                        .Select(i => new Income { Amount = 1, Date = DateTime.Today, Category = new IncomeCategory(1, "test") })
                                        .ToList();

            var mock = ServiceMocks.GetMockMonthService(expenseData, incomeData);
            var actual = mock.GetTotalForCategoryAndMonth("expense", "other-test", DateTime.Today);
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void MonthService_GetTotalForCategoryAndMonth_CategoryType_Expense_Returns_Full_Sum()
        {
            var expenseData = Enumerable.Range(1, 5)
                                        .Select(i => new Expense { Amount = 1, Date = DateTime.Today, Category = new ExpenseCategory(1, "test") })
                                        .ToList();
            var incomeData = Enumerable.Range(1, 5)
                                        .Select(i => new Income { Amount = 1, Date = DateTime.Today, Category = new IncomeCategory(1, "test") })
                                        .ToList();

            var mock = ServiceMocks.GetMockMonthService(expenseData, incomeData);
            var actual = mock.GetTotalForCategoryAndMonth("expense", "test", DateTime.Today);
            Assert.AreEqual(5, actual);
        }

        [TestMethod]
        public void MonthService_GetTotalForCategoryAndMonth_CategoryType_Expense_Case_Insensitive_Returns_Full_Sum()
        {
            var expenseData = Enumerable.Range(1, 5)
                                        .Select(i => new Expense { Amount = 1, Date = DateTime.Today, Category = new ExpenseCategory(1, "test") })
                                        .ToList();
            var incomeData = Enumerable.Range(1, 5)
                                        .Select(i => new Income { Amount = 1, Date = DateTime.Today, Category = new IncomeCategory(1, "test") })
                                        .ToList();

            var mock = ServiceMocks.GetMockMonthService(expenseData, incomeData);
            var actual = mock.GetTotalForCategoryAndMonth("expense", "TEST", DateTime.Today);
            Assert.AreEqual(5, actual);
        }

        [TestMethod]
        public void MonthService_GetTotalForCategoryAndMonth_CategoryType_Income_No_Matching_Category_Returns_Zero()
        {
            var expenseData = Enumerable.Range(1, 5)
                                        .Select(i => new Expense { Amount = 1, Date = DateTime.Today, Category = new ExpenseCategory(1, "test") })
                                        .ToList();
            var incomeData = Enumerable.Range(1, 5)
                                        .Select(i => new Income { Amount = 1, Date = DateTime.Today, Category = new IncomeCategory(1, "test") })
                                        .ToList();

            var mock = ServiceMocks.GetMockMonthService(expenseData, incomeData);
            var actual = mock.GetTotalForCategoryAndMonth("income", "other-test", DateTime.Today);
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void MonthService_GetTotalForCategoryAndMonth_CategoryType_Income_Returns_Full_Sum()
        {
            var expenseData = Enumerable.Range(1, 5)
                                                    .Select(i => new Expense { Amount = 1, Date = DateTime.Today, Category = new ExpenseCategory(1, "test") })
                                                    .ToList();
            var incomeData = Enumerable.Range(1, 5)
                                        .Select(i => new Income { Amount = 1, Date = DateTime.Today, Category = new IncomeCategory(1, "test") })
                                        .ToList();

            var mock = ServiceMocks.GetMockMonthService(expenseData, incomeData);
            var actual = mock.GetTotalForCategoryAndMonth("income", "test", DateTime.Today);
            Assert.AreEqual(5, actual);
        }

        [TestMethod]
        public void MonthService_GetTotalForCategoryAndMonth_CategoryType_Income_Case_Insensitive_Returns_Full_Sum()
        {
            var expenseData = Enumerable.Range(1, 5)
                                        .Select(i => new Expense { Amount = 1, Date = DateTime.Today, Category = new ExpenseCategory(1, "test") })
                                        .ToList();
            var incomeData = Enumerable.Range(1, 5)
                                        .Select(i => new Income { Amount = 1, Date = DateTime.Today, Category = new IncomeCategory(1, "test") })
                                        .ToList();

            var mock = ServiceMocks.GetMockMonthService(expenseData, incomeData);
            var actual = mock.GetTotalForCategoryAndMonth("income", "TEST", DateTime.Today);
            Assert.AreEqual(5, actual);
        }

        [TestMethod]
        public void MonthService_GetTotalForCategoryAndMonth_CategoryType_Does_Not_Exist_Returns_Zero()
        {
            var expenseData = Enumerable.Range(1, 5)
                                        .Select(i => new Expense { Amount = 1, Date = DateTime.Today, Category = new ExpenseCategory(1, "test") })
                                        .ToList();
            var incomeData = Enumerable.Range(1, 5)
                                        .Select(i => new Income { Amount = 1, Date = DateTime.Today, Category = new IncomeCategory(1, "test") })
                                        .ToList();

            var mock = ServiceMocks.GetMockMonthService(expenseData, incomeData);
            var actual = mock.GetTotalForCategoryAndMonth("are you sure", "test", DateTime.Today);
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void MonthService_GetTotalFlowPerCatgegoriesForMonth_No_Data_Returns_Zero_Sums_For_Totals()
        {
            var mock = ServiceMocks.GetMockMonthService();
            var actual = mock.GetTotalFlowPerCategoriesForMonth(DateTime.Today);

            var expected = new Dictionary<string, decimal>
            {
                ["Total Expenses"] = 0,
                ["Total Income"] = 0
            };

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void MonthService_GetTotalFlowPerCatgegoriesForMonth_No_Duplicates()
        {
            var expenseData = Enumerable.Range(1, 5)
                                        .Select(i => new Expense { Amount = 1, Date = DateTime.Today, Category = new ExpenseCategory(1, "e-test") })
                                        .ToList();
            expenseData = expenseData.Concat(Enumerable.Range(1, 5)
                                        .Select(i => new Expense { Amount = 1, Date = DateTime.Today, Category = new ExpenseCategory(1, "e-test2") }))
                                        .ToList();
            var incomeData = Enumerable.Range(1, 5)
                                        .Select(i => new Income { Amount = 1, Date = DateTime.Today, Category = new IncomeCategory(1, "i-test") })
                                        .ToList();

            var mock = ServiceMocks.GetMockMonthService(expenseData, incomeData);

            var actual = mock.GetTotalFlowPerCategoriesForMonth(DateTime.Today);
            var expected = new Dictionary<string, decimal>
            {
                ["Total Expenses"] = 10,
                ["Total Income"] = 5,
                ["e-test"] = 5,
                ["e-test2"] = 5,
                ["i-test"] = 5
            };

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void MonthService_GetTotalFlowPerCatgegoriesForMonth_Duplicates_In_Income()
        {
            var expenseData = Enumerable.Range(1, 5)
                                        .Select(i => new Expense { Amount = 1, Date = DateTime.Today, Category = new ExpenseCategory(1, "test") })
                                        .ToList();
            expenseData = expenseData.Concat(Enumerable.Range(1, 5)
                                        .Select(i => new Expense { Amount = 1, Date = DateTime.Today, Category = new ExpenseCategory(1, "e-test2") }))
                                        .ToList();
            var incomeData = Enumerable.Range(1, 5)
                                        .Select(i => new Income { Amount = 1, Date = DateTime.Today, Category = new IncomeCategory(1, "test") })
                                        .ToList();

            var mock = ServiceMocks.GetMockMonthService(expenseData, incomeData);

            var actual = mock.GetTotalFlowPerCategoriesForMonth(DateTime.Today);
            var expected = new Dictionary<string, decimal>
            {
                ["Total Expenses"] = 10,
                ["Total Income"] = 5,
                ["test - Expense"] = 5,
                ["e-test2"] = 5,
                ["test - Income"] = 5
            };

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void MonthService_GetTotalFlowPerCatgegoriesForMonth_Duplicates_In_Expense()
        {
            var expenseData = Enumerable.Range(1, 5)
                                        .Select(i => new Expense { Amount = 1, Date = DateTime.Today, Category = new ExpenseCategory(1, "test") })
                                        .ToList();
            var incomeData = Enumerable.Range(1, 5)
                                        .Select(i => new Income { Amount = 1, Date = DateTime.Today, Category = new IncomeCategory(1, "test") })
                                        .ToList();
            incomeData = incomeData.Concat(Enumerable.Range(1, 5)
                                        .Select(i => new Income { Amount = 1, Date = DateTime.Today, Category = new IncomeCategory(1, "i-test2") }))
                                        .ToList();

            var mock = ServiceMocks.GetMockMonthService(expenseData, incomeData);

            var actual = mock.GetTotalFlowPerCategoriesForMonth(DateTime.Today);
            var expected = new Dictionary<string, decimal>
            {
                ["Total Expenses"] = 5,
                ["Total Income"] = 10,
                ["test - Expense"] = 5,
                ["i-test2"] = 5,
                ["test - Income"] = 5
            };

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void MonthService_GetTotalFlowPerCatgegoriesForMonth_Duplicates_In_Both()
        {
            var expenseData = Enumerable.Range(1, 5)
                                        .Select(i => new Expense { Amount = 1, Date = DateTime.Today, Category = new ExpenseCategory(1, "test") })
                                        .ToList();
            expenseData = expenseData.Concat(Enumerable.Range(1, 5)
                                        .Select(i => new Expense { Amount = 1, Date = DateTime.Today, Category = new ExpenseCategory(1, "e-test2") }))
                                        .ToList();
            var incomeData = Enumerable.Range(1, 5)
                                        .Select(i => new Income { Amount = 1, Date = DateTime.Today, Category = new IncomeCategory(1, "test") })
                                        .ToList();
            incomeData = incomeData.Concat(Enumerable.Range(1, 5)
                                        .Select(i => new Income { Amount = 1, Date = DateTime.Today, Category = new IncomeCategory(1, "e-test2") }))
                                        .ToList();

            var mock = ServiceMocks.GetMockMonthService(expenseData, incomeData);

            var actual = mock.GetTotalFlowPerCategoriesForMonth(DateTime.Today);
            var expected = new Dictionary<string, decimal>
            {
                ["Total Expenses"] = 10,
                ["Total Income"] = 10,
                ["test - Expense"] = 5,
                ["e-test2 - Expense"] = 5,
                ["e-test2 - Income"] = 5,
                ["test - Income"] = 5
            };

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
