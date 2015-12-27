using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHome.TestUtils;
using System.Collections.Generic;
using MyHome.DataClasses;
using System.Linq;
using System;

namespace MyHome.DataRepositories.Tests
{
    [TestClass]
    public class ExpenseRepositoryTests
    {
        private Expense baseTestData = new Expense(10, new DateTime(2015, 2, 2), new ExpenseCategory(1, "test"), new PaymentMethod(1, "test"), "");

        [TestMethod]
        public void ExpenseRepository_GetById_Returns_Null_If_Not_found()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository();

            var actual = mock.GetById(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void ExpenseRepository_GetById_Returns_Object_If_Found()
        {
            var testDataWithID = baseTestData.Copy();
            testDataWithID.Id = 1;
            var mock = RepositoryMocks.GetMockExpenseRepository(new List<Expense> { testDataWithID });

            var actual = mock.GetById(1);

            Assert.AreEqual(testDataWithID, actual);
        }

        [TestMethod]
        public void ExpenseRepository_GetAll_Returns_Empty_List_If_No_Items_Exist()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository();

            // filters out the object added to make the mock work
            var actual = mock.GetAll().Where(e => e.Id != 0);

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count() == 0);
        }

        [TestMethod]
        public void ExpenseRepository_GetAll_Returns_All_Data()
        {
            var data = new List<Expense>();
            Enumerable.Range(1, 5).ToList().ForEach(i => data.Add(new Expense((decimal)Math.Pow(i, i), DateTime.Today, new ExpenseCategory(1, "test"), new PaymentMethod(1, "test"), "")));

            var mock = RepositoryMocks.GetMockExpenseRepository(data);

            var actual = mock.GetAll();

            CollectionAssert.AreEquivalent(data, actual.ToList());
        }

        [TestMethod]
        public void ExpenseRepository_GetForMonthAndYear_Returns_Empty_List_If_None_Found()
        {
            var data = new List<Expense>();
            Enumerable.Range(1, 5).ToList().ForEach(i => data.Add(new Expense((decimal)Math.Pow(i, i), DateTime.Today, new ExpenseCategory(1, "test"), new PaymentMethod(1, "test"), "")));

            var mock = RepositoryMocks.GetMockExpenseRepository(data);

            var actual = mock.GetForMonthAndYear(1,2010);

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count() == 0);
        }

        [TestMethod]
        public void ExpenseRepository_GetForMonthAndYear_Returns_Empty_List_If_No_Data_Exists()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository();

            var actual = mock.GetForMonthAndYear(1, 2010);

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count() == 0);
        }

        [TestMethod]
        public void ExpenseRepository_GetForMonthAndYear_Returns_Filtered_List()
        {
            var data = new List<Expense>();
            Enumerable.Range(1, 5).ToList().ForEach(i => data.Add(new Expense((decimal)Math.Pow(i, i), DateTime.Today, new ExpenseCategory(1, "test"), new PaymentMethod(1, "test"), "")));
            var expected = new List<Expense>(data);

            Enumerable.Range(6, 5).ToList().ForEach(i => data.Add(new Expense((decimal)Math.Pow(i, i), DateTime.MinValue, new ExpenseCategory(1, "test"), new PaymentMethod(1, "test"), "")));

            var mock = RepositoryMocks.GetMockExpenseRepository(data);

            var actual = mock.GetForMonthAndYear(DateTime.Today.Month, DateTime.Today.Year);

            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        public void ExpenseRepository_GetForMOnthAndYear_Only_Month_Matches_Not_Returned()
        {
            DateTime good = new DateTime(2015, 4, 10);
            DateTime bad = new DateTime(2010, 4, 10);

            var data = new List<Expense>();
            Enumerable.Range(1, 5).ToList().ForEach(i => data.Add(new Expense((decimal)Math.Pow(i, i), good, new ExpenseCategory(1, "test"), new PaymentMethod(1, "test"), "")));
            var expected = new List<Expense>(data);

            Enumerable.Range(6, 5).ToList().ForEach(i => data.Add(new Expense((decimal)Math.Pow(i, i), bad, new ExpenseCategory(1, "test"), new PaymentMethod(1, "test"), "")));

            var mock = RepositoryMocks.GetMockExpenseRepository(data);

            var actual = mock.GetForMonthAndYear(good.Month, good.Year);

            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        public void ExpenseRepository_GetForMOnthAndYear_Only_Year_Matches_Not_Returned()
        {
            DateTime good = new DateTime(2015, 4, 10);
            DateTime bad = new DateTime(2015, 6, 10);

            var data = new List<Expense>();
            Enumerable.Range(1, 5).ToList().ForEach(i => data.Add(new Expense((decimal)Math.Pow(i, i), good, new ExpenseCategory(1, "test"), new PaymentMethod(1, "test"), "")));
            var expected = new List<Expense>(data);

            Enumerable.Range(6, 5).ToList().ForEach(i => data.Add(new Expense((decimal)Math.Pow(i, i), bad, new ExpenseCategory(1, "test"), new PaymentMethod(1, "test"), "")));

            var mock = RepositoryMocks.GetMockExpenseRepository(data);

            var actual = mock.GetForMonthAndYear(good.Month, good.Year);

            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        public void ExpenseRepository_Remove_Database_Is_Empty_Nothing_Happens()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository();

            var before = mock.GetAll();
            mock.Remove(1);

            var after = mock.GetAll();

            CollectionAssert.AreEquivalent(before.ToList(), after.ToList());
        }

        [TestMethod]
        public void ExpenseRepository_Remove_Database_Has_No_Match_Nothing_Happens()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository(new List<Expense> { baseTestData });

            var before = mock.GetAll();
            mock.Remove(1);

            var after = mock.GetAll();

            CollectionAssert.AreEquivalent(before.ToList(), after.ToList());
        }

        [TestMethod]
        public void ExpenseRepository_Remove_Removes_Matching_Item_From_Database()
        {
            var testDataWithID = baseTestData.Copy();
            testDataWithID.Id = 1;
            var mock = RepositoryMocks.GetMockExpenseRepository(new List<Expense> { testDataWithID });

            var before = mock.GetAll();
            Assert.IsTrue(before.Contains(testDataWithID));

            mock.Remove(1);

            var after = mock.GetAll();
            Assert.IsFalse(after.Contains(testDataWithID));
        }

        [TestMethod]
        public void ExpenseRepository_Create_Adds_New_Item()
        {
            var copyTestData = baseTestData.Copy();
            copyTestData.Id = 1;
            var mock = RepositoryMocks.GetMockExpenseRepository();
            mock.Create(copyTestData);

            var result = mock.GetAll();
            Assert.IsTrue(result.Contains(copyTestData));

            var singleItem = mock.GetById(1);
            Assert.IsNotNull(singleItem);
            Assert.AreEqual(copyTestData, singleItem);
        }

        [TestMethod]
        public void ExpenseRepository_Create_Does_Nothing_If_Item_Is_Null()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository();
            var before = mock.GetAll();
            mock.Create(null);

            var result = mock.GetAll();

            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(before.ToList(), result.ToList());
        }

        [TestMethod]
        public void ExpenseRepository_Update_Changes_Amount()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository(new List<Expense> { baseTestData });

            var expected = mock.GetById(baseTestData.Id);
            expected.Amount *= 3;

            mock.Update(expected);

            var actual = mock.GetById(baseTestData.Id);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExpenseRepository_Update_Changes_The_Id()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository(new List<Expense> { baseTestData });

            var expected = mock.GetById(baseTestData.Id);
            expected.Id++;

            mock.Update(expected);

            var actual = mock.GetById(expected.Id);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExpenseRepository_Update_Changes_The_Category()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository(new List<Expense> { baseTestData });

            var expected = mock.GetById(baseTestData.Id);
            expected.Category = new ExpenseCategory(4, "other-cat");

            mock.Update(expected);

            var actual = mock.GetById(expected.Id);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExpenseRepository_Update_Changes_Comments()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository(new List<Expense> { baseTestData });

            var expected = mock.GetById(baseTestData.Id);
            expected.Comments = "moar comments";

            mock.Update(expected);

            var actual = mock.GetById(expected.Id);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExpenseRepository_Update_Changes_Date()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository(new List<Expense> { baseTestData });

            var expected = mock.GetById(baseTestData.Id);
            expected.Date = DateTime.MinValue;

            mock.Update(expected);

            var actual = mock.GetById(expected.Id);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExpenseRepository_Update_Changes_Method()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository(new List<Expense> { baseTestData });

            var expected = mock.GetById(baseTestData.Id);
            expected.Method = new PaymentMethod(4, "other-method");

            mock.Update(expected);

            var actual = mock.GetById(expected.Id);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExpenseRepository_Update_Object_That_Was_Not_In_Database_Does_Nothing()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository();

            mock.Update(baseTestData);

            var actual = mock.GetAll();

            Assert.IsFalse(actual.Contains(baseTestData));
        }

        [TestMethod]
        public void ExpenseCategpryRepository_Save_Id_Zero_Adds_Item()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository();
            
            // filters out the object added to make the mock work
            var actual = mock.GetAll().Where(e => e.Id != 0);
            Assert.IsTrue(actual.Count() == 0);

            mock.Save(baseTestData);

            var after = mock.GetAll();
            Assert.IsTrue(after.Contains(baseTestData));
        }

        [TestMethod]
        public void ExpenseCategpryRepository_Save_Id_Non_Zero_Updates_Item()
        {
            var copyTestData = baseTestData.Copy();
            copyTestData.Id = 1;
            var mock = RepositoryMocks.GetMockExpenseRepository(new List<Expense> { copyTestData });

            var before = mock.GetAll();
            Assert.IsTrue(before.Contains(copyTestData));

            var expected = mock.GetById(1);
            Assert.IsNotNull(expected);
            expected.Comments = "save-test";
            mock.Save(expected);

            var after = mock.GetAll();
            Assert.IsTrue(after.Contains(expected));

            var actual = mock.GetById(1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExpenseRepository_Save_New_Item_With_Non_Zero_Id_Does_Nothing()
        {
            var mock = RepositoryMocks.GetMockExpenseRepository();

            // filters out the object added to make the mock work
            var actual = mock.GetAll().Where(e => e.Id != 0);
            Assert.IsTrue(actual.Count() == 0);

            var expected = mock.GetById(1);
            Assert.IsNull(expected);
            expected = baseTestData.Copy();
            expected.Id = 2;
            mock.Save(expected);

            var after = mock.GetAll();
            Assert.IsFalse(after.Contains(expected));
        }
    }
}
