using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHome.TestUtils;
using System.Collections.Generic;
using MyHome.DataClasses;
using System.Linq;

namespace MyHome.DataRepositories.Tests
{
    [TestClass]
    public class IncomeCategoryRepositoryTests
    {
        private IncomeCategory baseTestData = new IncomeCategory(1, "test");
        [TestMethod]
        public void IncomeCategoryRepository_GetById_Null_If_Not_Found()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository();
            var result = mock.GetById(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void IncomeCategoryRepository_GetById_Returns_Object_If_Exists()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository(new List<IncomeCategory>() { baseTestData });
            var result = mock.GetById(baseTestData.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(baseTestData, result);
        }

        [TestMethod]
        public void IncomeCategoryRepository_GetByName_Null_If_Not_found()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository(new List<IncomeCategory>() { baseTestData });
            var result = mock.GetByName("not-test");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void IncomeCategoryRepository_GetByName_Returns_Object_If_Exists()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository(new List<IncomeCategory>() { baseTestData });
            var result = mock.GetByName(baseTestData.Name);

            Assert.IsNotNull(result);
            Assert.AreEqual(baseTestData, result);
        }

        [TestMethod]
        public void IncomeCategoryRepository_GetByName_Works_With_Diff_In_Casing()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository(new List<IncomeCategory>() { baseTestData });
            var result = mock.GetByName(baseTestData.Name.ToUpper());

            Assert.IsNotNull(result);
            Assert.AreEqual(baseTestData, result);
        }

        [TestMethod]
        public void IncomeCategoryRepository_GetByName_Returns_Null_For_Empty_String()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository(new List<IncomeCategory>() { baseTestData });
            var result = mock.GetByName(string.Empty);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void IncomeCategoryRepository_GetByName_Returns_Null_For_Null_String()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository(new List<IncomeCategory>() { baseTestData });
            var result = mock.GetByName(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void IncomeCategoryRepository_GetAll_Returns_Empty_Non_Null_List_If_No_Data()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository();

            var result = mock.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void IncomeCategoryRepository_GetAll_Returns_All_Data()
        {
            var expected = new List<IncomeCategory>();
            for (int i = 0; i < 5; i++)
            {
                expected.Add(new IncomeCategory(baseTestData.Id + i, $"{baseTestData.Name}::{i}"));
            }
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository(expected);

            var result = mock.GetAll();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public void IncomeCategoryRepository_Create_Adds_New_Item()
        {
            var testObject = new IncomeCategory(0, "test");

            var mock = RepositoryMocks.GetMockIncomeCategoryRepository();
            mock.Create(testObject);

            var result = mock.GetAll();
            Assert.IsTrue(result.Contains(testObject));

            var singleItem = mock.GetById(testObject.Id);
            Assert.IsNotNull(singleItem);
            Assert.AreEqual(testObject, singleItem);
        }

        [TestMethod]
        public void IncomeCategoryRepository_Create_Does_Nothing_If_Item_Is_Null()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository();
            mock.Create(null);

            var result = mock.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void IncomeCategoryRepository_Update_Changes_The_Name()
        {
            var newName = "new-test";
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository(new List<IncomeCategory> { baseTestData });

            var expected = mock.GetById(baseTestData.Id);
            expected.Name = newName;

            mock.Update(expected);

            var actual = mock.GetByName(newName);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IncomeCategoryRepository_Update_Changes_The_Id()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository(new List<IncomeCategory> { baseTestData });

            var expected = mock.GetById(baseTestData.Id);
            expected.Id++;

            mock.Update(expected);

            var actual = mock.GetById(expected.Id);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IncomeCategoryRepository_Update_Object_That_Was_Not_In_Database_Does_Nothing()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository();

            mock.Update(baseTestData);

            var actual = mock.GetById(baseTestData.Id);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void ExpenseCategpryRepository_Save_Id_Zero_Adds_Item()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository();
            var before = mock.GetAll();
            Assert.IsTrue(before.Count() == 0);

            var newItem = new IncomeCategory(0, "test");
            mock.Save(newItem);

            var after = mock.GetAll();
            Assert.IsTrue(after.Contains(newItem));
        }

        [TestMethod]
        public void ExpenseCategpryRepository_Save_Id_Non_Zero_Updates_Item()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository(new List<IncomeCategory> { baseTestData });

            var before = mock.GetAll();
            Assert.IsTrue(before.Contains(baseTestData));

            var expected = mock.GetById(baseTestData.Id);
            Assert.IsNotNull(expected);
            expected.Name = "save-test";
            mock.Save(expected);

            var after = mock.GetAll();
            Assert.IsTrue(after.Contains(expected));

            var actual = mock.GetById(baseTestData.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IncomeCategoryRepository_Save_New_Item_With_Non_Zero_Id_DOes_Nothing()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository();

            var before = mock.GetAll();
            Assert.IsTrue(before.Count() == 0);

            var expected = mock.GetById(baseTestData.Id);
            Assert.IsNull(expected);
            expected = new IncomeCategory(1, "save-test");
            mock.Save(expected);

            var after = mock.GetAll();
            Assert.IsTrue(after.Count() == 0);
        }

        [TestMethod]
        public void IncomeCategoryRepository_RemoveByName_Name_Exists_Item_Is_Removed()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository(new List<IncomeCategory> { baseTestData });

            var before = mock.GetAll();
            Assert.IsTrue(before.Contains(baseTestData));

            mock.RemoveByName(baseTestData.Name);

            var after = mock.GetAll();
            Assert.IsFalse(after.Contains(baseTestData));
        }

        [TestMethod]
        public void IncomeCategoryRepository_RemoveByName_Name_Does_Not_Exist_Nothing_Happens()
        {
            var mock = RepositoryMocks.GetMockIncomeCategoryRepository(new List<IncomeCategory> { baseTestData });

            var before = mock.GetAll();

            mock.RemoveByName(baseTestData.Name + " not really");

            var after = mock.GetAll();
            CollectionAssert.AreEqual(before.ToList(), after.ToList());
        }
    }
}
