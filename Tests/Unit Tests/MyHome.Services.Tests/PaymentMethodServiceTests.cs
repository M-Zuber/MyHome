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
        private PaymentMethod baseTestData = new PaymentMethod(1, "test");

        [TestMethod]
        public void PaymentMethodService_GetById_Item_Exists()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { baseTestData });
            var result = mock.GetById(baseTestData.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(baseTestData, result);
        }

        [TestMethod]
        public void PaymentMethodService_GetById_Item_Does_Not_Exist_Returns_Null()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            var result = mock.GetById(baseTestData.Id);

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
            Assert.IsFalse(before.Contains(baseTestData));

            mock.Create(baseTestData);

            var after = mock.GetAll();
            Assert.IsTrue(after.Contains(baseTestData));

            var actual = mock.GetById(baseTestData.Id);
            Assert.IsNotNull(actual);
            Assert.AreEqual(baseTestData, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Create_From_Object_Name_Already_Exists_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { baseTestData });
            mock.Create(baseTestData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Create_From_Object_Name_Already_Exists_Not_Case_Sensitive_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { baseTestData });
            mock.Create(new PaymentMethod(1, baseTestData.Name.ToUpper()));
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
            Assert.IsFalse(before.Where(e => e.Name.Equals(baseTestData.Name, StringComparison.OrdinalIgnoreCase)).Count() > 0);

            mock.Create(baseTestData.Name, baseTestData.Id);

            var after = mock.GetAll();
            Assert.IsTrue(after.Where(e => e.Name.Equals(baseTestData.Name, StringComparison.OrdinalIgnoreCase)).Count() > 0);

            var actual = mock.GetById(baseTestData.Id);
            Assert.IsNotNull(actual);
            Assert.IsTrue(baseTestData.Equals(actual));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Create_From_Params_Name_Already_Exists_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { baseTestData });
            mock.Create(baseTestData.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Create_From_Params_Name_Already_Exists_Not_Case_Sensitive_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { baseTestData });
            mock.Create(baseTestData.Name.ToUpper());
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
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { baseTestData });
            var actual = mock.Exists(baseTestData.Name);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void PaymentMethodService_Exists_Item_Exists_Case_Insensitive_Returns_True()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { baseTestData });
            var actual = mock.Exists(baseTestData.Name.ToLower());

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void PaymentMethodService_Exists_Item_Does_Not_Exists_Returns_False()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            var actual = mock.Exists(baseTestData.Name);

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
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { baseTestData });

            var before = mock.GetAll();
            Assert.IsTrue(before.Contains(baseTestData));

            mock.Delete(baseTestData.Name);

            var after = mock.GetAll();
            Assert.IsFalse(after.Contains(baseTestData));

            var actual = mock.GetById(baseTestData.Id);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void PaymentMethodService_Delete_Name_Does_Not_Exist_Does_Nothing()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();

            mock.Delete(baseTestData.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Save_Name_Is_Null_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            mock.Save(baseTestData.Id, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Save_Name_Is_Whitespace_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService();
            mock.Save(baseTestData.Id, "\n\n\n");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PaymentMethodService_Save_Name_Exists_Throws_Exception()
        {
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { baseTestData });
            mock.Save(baseTestData.Id, baseTestData.Name);
        }

        [TestMethod]
        public void PaymentMethodService_Save_Item_Exists_Saves_It()
        {
            var name = "new-test";
            var mock = ServiceMocks.GetMockPaymentMethodService(new List<PaymentMethod> { baseTestData });
            mock.Save(baseTestData.Id, name);

            var actual = mock.GetById(baseTestData.Id);
            Assert.IsNotNull(actual);
            Assert.AreEqual(name, actual.Name);
        }

        [TestMethod]
        public void PaymentMethodService_Save_Item_Does_Not_Exist_Creates_It()
        {
            var name = "new-test";
            var mock = ServiceMocks.GetMockPaymentMethodService();

            var before = mock.GetById(baseTestData.Id);
            Assert.IsNull(before);

            mock.Save(baseTestData.Id, name);

            var actual = mock.GetAll().FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            Assert.IsNotNull(actual);
        }
    }
}