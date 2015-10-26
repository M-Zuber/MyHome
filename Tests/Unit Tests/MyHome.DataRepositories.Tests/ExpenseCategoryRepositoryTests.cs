using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyHome.DataRepository;
using MyHome.TestUtils;
using System.Collections.Generic;
using MyHome.DataClasses;
using System.Linq;

namespace MyHome.DataRepositories.Tests
{
    [TestClass]
    public class ExpenseCategoryRepositoryTests
    {
        private ExpenseCategory baseTestData = new ExpenseCategory(1, "test");
        [TestMethod]
        public void ExpenseCategoryRepository_GetById_Null_If_Not_Found()
        {
            var mock = RepositoryMocks.GetMockExpenseCategoryRepository();
            var result = mock.GetById(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ExpenseCategoryRepository_GetById_Returns_Object_If_Exists()
        {
            var mock = RepositoryMocks.GetMockExpenseCategoryRepository(new List<ExpenseCategory>() { baseTestData });
            var result = mock.GetById(baseTestData.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(baseTestData, result);
        }

        [TestMethod]
        public void ExpenseCategoryRepository_GetByName_Null_If_Not_found()
        {
            var mock = RepositoryMocks.GetMockExpenseCategoryRepository(new List<ExpenseCategory>() { baseTestData});
            var result = mock.GetByName("not-test");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ExpenseCategoryRepository_GetByName_Returns_Object_If_Exists()
        {
            var mock = RepositoryMocks.GetMockExpenseCategoryRepository(new List<ExpenseCategory>() { baseTestData});
            var result = mock.GetByName(baseTestData.Name);

            Assert.IsNotNull(result);
            Assert.AreEqual(baseTestData, result);
        }

        [TestMethod]
        public void ExpenseCategoryRepository_GetByName_Works_With_Diff_In_Casing()
        {
            var mock = RepositoryMocks.GetMockExpenseCategoryRepository(new List<ExpenseCategory>() { baseTestData });
            var result = mock.GetByName(baseTestData.Name.ToUpper());

            Assert.IsNotNull(result);
            Assert.AreEqual(baseTestData, result);
        }

        [TestMethod]
        public void ExpenseCategoryRepository_GetByName_Returns_Null_For_Empty_String()
        {
            var mock = RepositoryMocks.GetMockExpenseCategoryRepository(new List<ExpenseCategory>() { baseTestData });
            var result = mock.GetByName(string.Empty);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ExpenseCategoryRepository_GetByName_Returns_Null_For_Null_String()
        {
            var mock = RepositoryMocks.GetMockExpenseCategoryRepository(new List<ExpenseCategory>() { baseTestData });
            var result = mock.GetByName(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ExpenseCategoryRepository_GetAll_Returns_Empty_Non_Null_List_If_No_Data()
        {
            var mock = RepositoryMocks.GetMockExpenseCategoryRepository();

            var result = mock.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void ExpenseCategoryRepository_GetAll_Returns_All_Data()
        {
            var expected = new List<ExpenseCategory>();
            for (int i = 0; i < 5; i++)
            {
                expected.Add(new ExpenseCategory(baseTestData.Id + i, $"{baseTestData.Name}::{i}"));
            }
            var mock = RepositoryMocks.GetMockExpenseCategoryRepository(expected);

            var result = mock.GetAll();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public void ExpenseCategoryRepository_Create_Adds_New_Item()
        {
            var testObject = new ExpenseCategory(0, "test");

            var mock = RepositoryMocks.GetMockExpenseCategoryRepository();
            mock.Create(testObject);

            var result = mock.GetAll();
            Assert.IsTrue(result.Contains(testObject));

            var singleItem = mock.GetById(testObject.Id);
            Assert.IsNotNull(singleItem);
            Assert.AreEqual(testObject, singleItem);
        }

        [TestMethod]
        public void ExpenseCategoryRepository_Create_Does_Nothing_If_Item_Is_Null()
        {
            var mock = RepositoryMocks.GetMockExpenseCategoryRepository();
            mock.Create(null);

            var result = mock.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void ExpenseCategoryRepository_Update_Changes_The_Name()
        {
            var newName = "new-test";
            var mock = RepositoryMocks.GetMockExpenseCategoryRepository(new List<ExpenseCategory> { baseTestData });

            var expected = mock.GetById(baseTestData.Id);
            expected.Name = newName;

            mock.Update(expected);

            var actual = mock.GetByName(newName);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExpenseCategoryRepository_Update_Changes_The_Id()
        {
            var mock = RepositoryMocks.GetMockExpenseCategoryRepository(new List<ExpenseCategory> { baseTestData });

            var expected = mock.GetById(baseTestData.Id);
            expected.Id++;

            mock.Update(expected);

            var actual = mock.GetById(expected.Id);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExpenseCategoryRepository_Update_Object_That_Was_Not_In_Database_Does_Nothing()
        {
            var mock = RepositoryMocks.GetMockExpenseCategoryRepository();

            mock.Update(baseTestData);

            var actual = mock.GetById(baseTestData.Id);

            Assert.IsNull(actual);
        }
    }
}
