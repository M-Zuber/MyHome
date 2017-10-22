using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHome.DataClasses;
using MyHome.TestUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyHome.Services.Tests
{
    [TestClass]
    public class IncomeServiceTests
    {
        private readonly Income _baseTestData = new Income(10, new DateTime(2015, 2, 2), new IncomeCategory(1, "test"), new PaymentMethod(1, "test"), "") { PaymentMethodId = 1, CategoryId = 1 };

        [TestMethod]
        public void IncomeService_LoadById_Item_Exists_Returns_It()
        {
            var mock = ServiceMocks.GetMockIncomeService(new List<Income> { _baseTestData });

            var actual = mock.LoadById(_baseTestData.Id);
            Assert.IsNotNull(actual);
            Assert.AreEqual(_baseTestData, actual);
        }

        [TestMethod]
        public void IncomeService_LoadById_Item_Does_Not_Exist_Returns_Null()
        {
            var mock = ServiceMocks.GetMockIncomeService();

            var actual = mock.LoadById(1);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void IncomeService_GetAll_Returns_Data()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i => { var c = _baseTestData.Copy(); c.Id = i; c.Amount = (decimal)Math.Pow(i, i); return c; })
                                 .ToList();
            var mock = ServiceMocks.GetMockIncomeService(data);

            var actual = mock.LoadAll();

            CollectionAssert.AreEquivalent(data, actual.ToList());
        }

        [TestMethod]
        public void IncomeService_GetAll_No_Data_Returns_Empty_List()
        {
            var mock = ServiceMocks.GetMockIncomeService(new List<Income>());

            var actual = mock.LoadAll();

            CollectionAssert.AreEquivalent(new List<Income>(), actual.ToList());
        }

        [TestMethod]
        public void IncomeService_LoadOfMonth_No_Matching_Data_Returns_Empty_List()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = _baseTestData.Copy();
                                     c.Id = i;
                                     c.Date = new DateTime(2015, 3, 3);
                                     c.Amount = (decimal)Math.Pow(i, i);
                                     return c;
                                 })
                                 .ToList();
            var mock = ServiceMocks.GetMockIncomeService(data);

            var actual = mock.LoadOfMonth(new DateTime(2010, 1, 1));

            CollectionAssert.AreEquivalent(new List<Income>(), actual.ToList());
        }

        [TestMethod]
        public void IncomeService_LoadOfMonth_Only_Month_Matches_Returns_Empty_List()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = _baseTestData.Copy();
                                     c.Id = i;
                                     c.Date = new DateTime(2015, 1, 3);
                                     c.Amount = (decimal)Math.Pow(i, i);
                                     return c;
                                 })
                                 .ToList();
            var mock = ServiceMocks.GetMockIncomeService(data);

            var actual = mock.LoadOfMonth(new DateTime(2010, 1, 1));

            CollectionAssert.AreEquivalent(new List<Income>(), actual.ToList());
        }

        [TestMethod]
        public void IncomeService_LoadOfMonth_Only_Year_Matches_Returns_Empty_List()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = _baseTestData.Copy();
                                     c.Id = i;
                                     c.Date = new DateTime(2010, 3, 3);
                                     c.Amount = (decimal)Math.Pow(i, i);
                                     return c;
                                 })
                                 .ToList();
            var mock = ServiceMocks.GetMockIncomeService(data);

            var actual = mock.LoadOfMonth(new DateTime(2010, 1, 1));

            CollectionAssert.AreEquivalent(new List<Income>(), actual.ToList());
        }

        [TestMethod]
        public void IncomeService_LoadOfMonth_Returns_All_Matching_Data()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = _baseTestData.Copy();
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
                                     var c = _baseTestData.Copy();
                                     c.Id = i;
                                     c.Date = new DateTime(2012, 6, 3);
                                     c.Amount = (decimal)Math.Pow(i, i);
                                     return c;
                                 }))
                                 .ToList();

            var mock = ServiceMocks.GetMockIncomeService(data);

            var actual = mock.LoadOfMonth(new DateTime(2015, 3, 1));

            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "There must be a category selected")]
        public void IncomeService_Save_Catgeory_Null_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockIncomeService();
            mock.Save(new Income() { Method = new PaymentMethod(), CategoryId = 0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IncomeService_Save_PaymentMethod_Null_Throw_Exception()
        {
            var mock = ServiceMocks.GetMockIncomeService();
            mock.Save(new Income() { Category = new IncomeCategory(), PaymentMethodId = 0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IncomeService_Save_Item_Is_Null_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockIncomeService();
            mock.Save(null);
        }

        [TestMethod]
        public void IncomeService_Save_New_Item_Is_Added()
        {
            var mock = ServiceMocks.GetMockIncomeService(new List<Income>());

            var before = mock.LoadAll();
            Assert.IsFalse(before.Contains(_baseTestData));

            mock.Save(_baseTestData);

            var after = mock.LoadAll();
            Assert.IsTrue(after.Contains(_baseTestData));

            var actual = mock.LoadById(_baseTestData.Id);
            Assert.IsNotNull(actual);
            Assert.AreEqual(_baseTestData, actual);
        }

        [TestMethod]
        public void IncomeService_Save_Existing_Item_Is_Updated()
        {
            var testDataWithId = _baseTestData.Copy();
            testDataWithId.Id = 1;

            var mock = ServiceMocks.GetMockIncomeService(new List<Income> { testDataWithId });

            var before = mock.LoadAll();
            Assert.IsTrue(before.Contains(testDataWithId));

            testDataWithId.Comments = "some random things";

            mock.Save(testDataWithId);

            var after = mock.LoadAll();
            Assert.IsTrue(after.Contains(testDataWithId));

            var actual = mock.LoadById(testDataWithId.Id);
            Assert.IsNotNull(actual);
            Assert.AreEqual(testDataWithId, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IncomeService_Create_Item_Is_Null_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockIncomeService();
            mock.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IncomeService_Create_Category_Is_Null_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockIncomeService();
            mock.Create(new Income { Method = new PaymentMethod(), CategoryId = 0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IncomeService_Create_PaymentMethod_Is_Null_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockIncomeService();
            mock.Create(new Income { Category = new IncomeCategory(), PaymentMethodId = 0 });
        }

        [TestMethod]
        public void IncomeService_Create_Item_Is_Added()
        {
            var mock = ServiceMocks.GetMockIncomeService(new List<Income>());

            var before = mock.LoadAll();
            Assert.IsFalse(before.Contains(_baseTestData));

            mock.Create(_baseTestData);

            var after = mock.LoadAll();
            Assert.IsTrue(after.Contains(_baseTestData));

            var actual = mock.LoadById(_baseTestData.Id);
            Assert.IsNotNull(actual);
            Assert.AreEqual(_baseTestData, actual);
        }

        [TestMethod]
        public void IncomeService_Delete_Item_Does_Not_Exist_Nothing_Happens()
        {
            var mock = ServiceMocks.GetMockIncomeService();
            mock.Delete(12);
        }

        [TestMethod]
        public void IncomeService_Delete_Item_exists_Is_Removed()
        {
            var testDataWithId = _baseTestData.Copy();
            testDataWithId.Id = 1;

            var mock = ServiceMocks.GetMockIncomeService(new List<Income> { testDataWithId });

            var before = mock.LoadAll();
            Assert.IsTrue(before.Contains(testDataWithId));

            var actual = mock.LoadById(testDataWithId.Id);
            Assert.IsNotNull(actual);

            mock.Delete(testDataWithId.Id);

            var after = mock.LoadAll();
            Assert.IsFalse(after.Contains(testDataWithId));

            var itemAfter = mock.LoadById(testDataWithId.Id);
            Assert.IsNull(itemAfter);
        }

        [TestMethod]
        public void IncomeService_GetMonthTotal_No_Matching_Data_Returns_Zero()
        {
            var mock = ServiceMocks.GetMockIncomeService();

            var total = mock.GetMonthTotal(DateTime.Today);

            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void IncomeService_GetMonthTotal_Returns_Full_Sum()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = _baseTestData.Copy();
                                     c.Id = i;
                                     c.Date = DateTime.Today;
                                     c.Amount = 1;
                                     return c;
                                 })
                                 .ToList();
            var mock = ServiceMocks.GetMockIncomeService(data);

            var total = mock.GetMonthTotal(DateTime.Today);

            Assert.AreEqual(5, total);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IncomeService_GetCategoryTotalForMonth_Null_Category_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockIncomeService();

            var _ = mock.GetCategoryTotalForMonth(DateTime.Today, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IncomeService_GetCategoryTotalForMonth_Whitespace_Category_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockIncomeService();

            var _ = mock.GetCategoryTotalForMonth(DateTime.Today, "\t\t\t");
        }

        [TestMethod]
        public void IncomeService_GetCategoryTotalForMonth_No_Matching_Data_Returns_Zero()
        {
            var mock = ServiceMocks.GetMockIncomeService();

            var total = mock.GetCategoryTotalForMonth(DateTime.Today, "not here");

            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void IncomeService_GetCategoryTotalForMonth_Returns_Full_Sum()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = _baseTestData.Copy();
                                     c.Id = i;
                                     c.Date = DateTime.Today;
                                     c.Category = new IncomeCategory(0, "this");
                                     c.Amount = 1;
                                     return c;
                                 })
                                 .ToList();
            var mock = ServiceMocks.GetMockIncomeService(data);

            var total = mock.GetCategoryTotalForMonth(DateTime.Today, "this");

            Assert.AreEqual(5, total);
        }

        [TestMethod]
        public void IncomeService_GetCategoryTotalForMonth_Case_Insensitive_Returns_Full_Sum()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = _baseTestData.Copy();
                                     c.Id = i;
                                     c.Date = DateTime.Today;
                                     c.Category = new IncomeCategory(0, "this");
                                     c.Amount = 1;
                                     return c;
                                 })
                                 .ToList();
            var mock = ServiceMocks.GetMockIncomeService(data);

            var total = mock.GetCategoryTotalForMonth(DateTime.Today, "THIS");

            Assert.AreEqual(5, total);
        }

        [TestMethod]
        public void IncomeService_GetAllCategoryTotals_No_Categories_Returns_Empty_Dictionary()
        {
            var mock = ServiceMocks.GetMockIncomeService();

            var actual = mock.GetAllCategoryTotals(DateTime.Today);

            CollectionAssert.AreEquivalent(new Dictionary<string, decimal>(), actual);
        }

        [TestMethod]
        public void IncomeService_GetAllCategoryTotals_All_Categories_With_Correct_Sum()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = _baseTestData.Copy();
                                     c.Id = i;
                                     c.Date = DateTime.Today;
                                     c.Category = new IncomeCategory(0, i.ToString());
                                     c.Amount = 1;
                                     return c;
                                 })
                                 .ToList();
            var expected = Enumerable.Range(1, 5)
                                     .ToDictionary(k => k.ToString(), v => (decimal)1);
            var mock = ServiceMocks.GetMockIncomeService(data);

            var actual = mock.GetAllCategoryTotals(DateTime.Today);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IncomeService_GetPaymentMethodTotalForMonth_Null_Category_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockIncomeService();

            var _ = mock.GetPaymentMethodTotalForMonth(DateTime.Today, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IncomeService_GetPaymentMethodTotalForMonth_Whitespace_Category_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockIncomeService();

            var _ = mock.GetPaymentMethodTotalForMonth(DateTime.Today, "\t\t\t");
        }

        [TestMethod]
        public void IncomeService_GetPaymentMethodTotalForMonth_No_Matching_Data_Returns_Zero()
        {
            var mock = ServiceMocks.GetMockIncomeService();

            var total = mock.GetPaymentMethodTotalForMonth(DateTime.Today, "not here");

            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void IncomeService_GetPaymentMethodTotalForMonth_Returns_Full_Sum()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = _baseTestData.Copy();
                                     c.Id = i;
                                     c.Date = DateTime.Today;
                                     c.Method = new PaymentMethod(0, "this");
                                     c.Amount = 1;
                                     return c;
                                 })
                                 .ToList();
            var mock = ServiceMocks.GetMockIncomeService(data);

            var total = mock.GetPaymentMethodTotalForMonth(DateTime.Today, "this");

            Assert.AreEqual(5, total);
        }

        [TestMethod]
        public void IncomeService_GetPaymentMethodTotalForMonth_Case_Insensitive_Returns_Full_Sum()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = _baseTestData.Copy();
                                     c.Id = i;
                                     c.Date = DateTime.Today;
                                     c.Method = new PaymentMethod(0, "this");
                                     c.Amount = 1;
                                     return c;
                                 })
                                 .ToList();
            var mock = ServiceMocks.GetMockIncomeService(data);

            var total = mock.GetPaymentMethodTotalForMonth(DateTime.Today, "THIS");

            Assert.AreEqual(5, total);
        }

        [TestMethod]
        public void IncomeService_GetAllPaymentMethodTotals_No_Methods_Returns_Empty_Dictionary()
        {
            var mock = ServiceMocks.GetMockIncomeService();

            var actual = mock.GetAllPaymentMethodTotals(DateTime.Today);

            CollectionAssert.AreEquivalent(new Dictionary<string, decimal>(), actual);
        }

        [TestMethod]
        public void IncomeService_GetAllPaymentMethodTotals_All_Methods_With_Correct_Sum()
        {
            var data = Enumerable.Range(1, 5)
                                 .Select(i =>
                                 {
                                     var c = _baseTestData.Copy();
                                     c.Id = i;
                                     c.Date = DateTime.Today;
                                     c.Method = new PaymentMethod(0, i.ToString());
                                     c.Amount = 1;
                                     return c;
                                 })
                                 .ToList();
            var expected = Enumerable.Range(1, 5)
                                     .ToDictionary(k => k.ToString(), v => (decimal)1);
            var mock = ServiceMocks.GetMockIncomeService(data);

            var actual = mock.GetAllPaymentMethodTotals(DateTime.Today);

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}