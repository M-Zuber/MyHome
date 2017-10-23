using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHome.TestUtils;
using System.Collections.Generic;
using MyHome.DataClasses;
using System.Linq;

namespace MyHome.DataRepositories.Tests
{
    [TestClass]
    public class PaymentMethodRepositoryTests
    {
        private readonly PaymentMethod _baseTestData = new PaymentMethod(1, "test");
        [TestMethod]
        public void PaymentMethodRepository_GetById_Null_If_Not_Found()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository();
            var result = mock.GetById(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void PaymentMethodRepository_GetById_Returns_Object_If_Exists()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository(new List<PaymentMethod> { _baseTestData });
            var result = mock.GetById(_baseTestData.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(_baseTestData, result);
        }

        [TestMethod]
        public void PaymentMethodRepository_GetByName_Null_If_Not_found()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository(new List<PaymentMethod> { _baseTestData });
            var result = mock.GetByName("not-test");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void PaymentMethodRepository_GetByName_Returns_Object_If_Exists()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository(new List<PaymentMethod> { _baseTestData });
            var result = mock.GetByName(_baseTestData.Name);

            Assert.IsNotNull(result);
            Assert.AreEqual(_baseTestData, result);
        }

        [TestMethod]
        public void PaymentMethodRepository_GetByName_Works_With_Diff_In_Casing()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository(new List<PaymentMethod> { _baseTestData });
            var result = mock.GetByName(_baseTestData.Name.ToUpper());

            Assert.IsNotNull(result);
            Assert.AreEqual(_baseTestData, result);
        }

        [TestMethod]
        public void PaymentMethodRepository_GetByName_Returns_Null_For_Empty_String()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository(new List<PaymentMethod> { _baseTestData });
            var result = mock.GetByName(string.Empty);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void PaymentMethodRepository_GetByName_Returns_Null_For_Null_String()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository(new List<PaymentMethod> { _baseTestData });
            var result = mock.GetByName(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void PaymentMethodRepository_GetAll_Returns_Empty_Non_Null_List_If_No_Data()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository();

            var result = mock.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void PaymentMethodRepository_GetAll_Returns_All_Data()
        {
            var expected = new List<PaymentMethod>();
            for (var i = 0; i < 5; i++)
            {
                expected.Add(new PaymentMethod(_baseTestData.Id + i, $"{_baseTestData.Name}::{i}"));
            }
            var mock = RepositoryMocks.GetMockPaymentMethodRepository(expected);

            var result = mock.GetAll();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public void PaymentMethodRepository_Create_Adds_New_Item()
        {
            var testObject = new PaymentMethod(0, "test");

            var mock = RepositoryMocks.GetMockPaymentMethodRepository();
            mock.Create(testObject);

            var result = mock.GetAll();
            Assert.IsTrue(result.Contains(testObject));

            var singleItem = mock.GetById(testObject.Id);
            Assert.IsNotNull(singleItem);
            Assert.AreEqual(testObject, singleItem);
        }

        [TestMethod]
        public void PaymentMethodRepository_Create_Does_Nothing_If_Item_Is_Null()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository();
            mock.Create(null);

            var result = mock.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void PaymentMethodRepository_Update_Changes_The_Name()
        {
            var newName = "new-test";
            var mock = RepositoryMocks.GetMockPaymentMethodRepository(new List<PaymentMethod> { _baseTestData });

            var expected = mock.GetById(_baseTestData.Id);
            expected.Name = newName;

            mock.Update(expected);

            var actual = mock.GetByName(newName);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PaymentMethodRepository_Update_Changes_The_Id()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository(new List<PaymentMethod> { _baseTestData });

            var expected = mock.GetById(_baseTestData.Id);
            expected.Id++;

            mock.Update(expected);

            var actual = mock.GetById(expected.Id);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PaymentMethodRepository_Update_Object_That_Was_Not_In_Database_Does_Nothing()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository();

            mock.Update(_baseTestData);

            var actual = mock.GetById(_baseTestData.Id);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void ExpenseCategpryRepository_Save_Id_Zero_Adds_Item()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository();
            var before = mock.GetAll();
            Assert.IsTrue(!before.Any());

            var newItem = new PaymentMethod(0, "test");
            mock.Save(newItem);

            var after = mock.GetAll();
            Assert.IsTrue(after.Contains(newItem));
        }

        [TestMethod]
        public void ExpenseCategpryRepository_Save_Id_Non_Zero_Updates_Item()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository(new List<PaymentMethod> { _baseTestData });

            var before = mock.GetAll();
            Assert.IsTrue(before.Contains(_baseTestData));

            var expected = mock.GetById(_baseTestData.Id);
            Assert.IsNotNull(expected);
            expected.Name = "save-test";
            mock.Save(expected);

            var after = mock.GetAll();
            Assert.IsTrue(after.Contains(expected));

            var actual = mock.GetById(_baseTestData.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PaymentMethodRepository_Save_New_Item_With_Non_Zero_Id_Does_Nothing()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository();

            var before = mock.GetAll();
            Assert.IsTrue(!before.Any());

            var expected = mock.GetById(_baseTestData.Id);
            Assert.IsNull(expected);
            expected = new PaymentMethod(1, "save-test");
            mock.Save(expected);

            var after = mock.GetAll();
            Assert.IsTrue(!after.Any());
        }

        [TestMethod]
        public void PaymentMethodRepository_RemoveByName_Name_Exists_Item_Is_Removed()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository(new List<PaymentMethod> { _baseTestData });

            var before = mock.GetAll();
            Assert.IsTrue(before.Contains(_baseTestData));

            mock.RemoveByName(_baseTestData.Name);

            var after = mock.GetAll();
            Assert.IsFalse(after.Contains(_baseTestData));
        }

        [TestMethod]
        public void PaymentMethodRepository_RemoveByName_Name_Exists_Item_Is_Removed_With_Different_Casing()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository(new List<PaymentMethod> { _baseTestData });

            var before = mock.GetAll();
            Assert.IsTrue(before.Contains(_baseTestData));

            mock.RemoveByName(_baseTestData.Name.ToUpper());

            var after = mock.GetAll();
            Assert.IsFalse(after.Contains(_baseTestData));
        }

        [TestMethod]
        public void PaymentMethodRepository_RemoveByName_Name_Does_Not_Exist_Nothing_Happens()
        {
            var mock = RepositoryMocks.GetMockPaymentMethodRepository(new List<PaymentMethod> { _baseTestData });

            var before = mock.GetAll();

            mock.RemoveByName(_baseTestData.Name + " not really");

            var after = mock.GetAll();
            CollectionAssert.AreEqual(before.ToList(), after.ToList());
        }
    }
}
