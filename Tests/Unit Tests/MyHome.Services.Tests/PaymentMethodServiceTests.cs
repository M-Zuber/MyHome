using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHome.TestUtils;
using MyHome.DataClasses;
using System.Collections.Generic;
using System.Linq;

namespace MyHome.Services.Tests
{
    [TestClass]
    public class PaymentMethodServiceTests
    {
        private readonly PaymentMethod _baseTestData = new PaymentMethod(1, "test");

        [TestMethod]
        public void PaymentMethodService_GetById_Item_Exists()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { _baseTestData });
            var result = mock.GetById(_baseTestData.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(_baseTestData, result);
        }

        [TestMethod]
        public void PaymentMethodService_GetById_Item_Does_Not_Exist_Returns_Null()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            var result = mock.GetById(_baseTestData.Id);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void PaymentMethodService_GetAll_Returns_Existing_Data()
        {
            var expected = Enumerable.Range(1, 5).Select(i => new PaymentMethod(i, $"{i} test")).ToList();
            var mock = ServiceMocks.GetMockPaymentMethodService(expected);

            var actual = mock.GetAll();

            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        public void PaymentMethodService_GetAll_Returns_Empty_List_If_No_Data_Exists()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();

            var actual = mock.GetAll();

            CollectionAssert.AreEquivalent(new List<PaymentMethod>(), actual.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Create_From_Object_Object_Is_Null_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            mock.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Create_From_Object_Name_Is_Null_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            mock.Create(new PaymentMethod(1, null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Create_From_Object_Name_Is_WhiteSpace_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            mock.Create(new PaymentMethod(1, "\t"));
        }

        [TestMethod]
        public void PaymentMethodService_Create_From_Object_Name_Has_A_Value_Adds_Item()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();

            var before = mock.GetAll();
            Assert.IsFalse(before.Contains(_baseTestData));

            mock.Create(_baseTestData);

            var after = mock.GetAll();
            Assert.IsTrue(after.Contains(_baseTestData));

            var actual = mock.GetById(_baseTestData.Id);
            Assert.IsNotNull(actual);
            Assert.AreEqual(_baseTestData, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Create_From_Object_Name_Already_Exists_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { _baseTestData });
            mock.Create(_baseTestData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Create_From_Object_Name_Already_Exists_Not_Case_Sensitive_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { _baseTestData });
            mock.Create(new PaymentMethod(1, _baseTestData.Name.ToUpper()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Create_From_Params_Name_Is_Null_No_Id_Given_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            mock.Create(name: null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Create_From_Params_Name_Is_Null_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            mock.Create(null, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Create_From_Params_Name_Is_WhiteSpace_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            mock.Create("\t", 1);
        }

        [TestMethod]
        public void PaymentMethodService_Create_From_Params_Name_Has_A_Value_Adds_Item()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();

            var before = mock.GetAll();
            Assert.IsFalse(before.Any(e => e.Name.Equals(_baseTestData.Name, StringComparison.OrdinalIgnoreCase)));

            mock.Create(_baseTestData.Name, _baseTestData.Id);

            var after = mock.GetAll();
            Assert.IsTrue(after.Any(e => e.Name.Equals(_baseTestData.Name, StringComparison.OrdinalIgnoreCase)));

            var actual = mock.GetById(_baseTestData.Id);
            Assert.IsNotNull(actual);
            Assert.IsTrue(_baseTestData.Equals(actual));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Create_From_Params_Name_Already_Exists_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { _baseTestData });
            mock.Create(_baseTestData.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Create_From_Params_Name_Already_Exists_Not_Case_Sensitive_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { _baseTestData });
            mock.Create(_baseTestData.Name.ToUpper());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Exists_Name_Is_Null_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            mock.Exists(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Exists_Name_Is_Whitespace_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            mock.Exists("\r\n");
        }

        [TestMethod]
        public void PaymentMethodService_Exists_Item_Exists_Returns_True()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { _baseTestData });
            var actual = mock.Exists(_baseTestData.Name);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void PaymentMethodService_Exists_Item_Exists_Case_Insensitive_Returns_True()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { _baseTestData });
            var actual = mock.Exists(_baseTestData.Name.ToLower());

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void PaymentMethodService_Exists_Item_Does_Not_Exists_Returns_False()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            var actual = mock.Exists(_baseTestData.Name);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Delete_Name_Is_Null_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            mock.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Delete_Name_Is_Whitespace_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            mock.Delete("            ");
        }

        [TestMethod]
        public void PaymentMethodService_Delete_Name_Exists_Removes_Item()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { _baseTestData });

            var before = mock.GetAll();
            Assert.IsTrue(before.Contains(_baseTestData));

            mock.Delete(_baseTestData.Name);

            var after = mock.GetAll();
            Assert.IsFalse(after.Contains(_baseTestData));

            var actual = mock.GetById(_baseTestData.Id);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void PaymentMethodService_Delete_Name_Does_Not_Exist_Does_Nothing()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();

            mock.Delete(_baseTestData.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Save_Name_Is_Null_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            mock.Save(_baseTestData.Id, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Save_Name_Is_Whitespace_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            mock.Save(_baseTestData.Id, "\n\n\n");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Save_Name_Exists_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { _baseTestData });
            mock.Save(_baseTestData.Id, _baseTestData.Name);
        }

        [TestMethod]
        public void PaymentMethodService_Save_Item_Exists_Saves_It()
        {
            var name = "new-test";
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { _baseTestData });
            mock.Save(_baseTestData.Id, name);

            var actual = mock.GetById(_baseTestData.Id);
            Assert.IsNotNull(actual);
            Assert.AreEqual(name, actual.Name);
        }

        [TestMethod]
        public void PaymentMethodService_Save_Item_Does_Not_Exist_Zero_Id_Creates_It()
        {
            var name = "new-test";
            var mock = ServiceMocks.GetMockPaymentMethodService();

            var before = mock.GetAll().FirstOrDefault(c => c.Name == name);
            Assert.IsNull(before);

            mock.Save(0, name);

            var actual = mock.GetAll().FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void PaymentMethodService_Save_Item_Does_Not_Exist_Non_Zero_Id_Does_Nothing()
        {
            var name = "new-test";
            var mock = ServiceMocks.GetMockPaymentMethodService();

            var before = mock.GetById(_baseTestData.Id);
            Assert.IsNull(before);

            mock.Save(_baseTestData.Id, name);

            var actual = mock.GetAll().FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            Assert.IsNull(actual);
        }
    }
}