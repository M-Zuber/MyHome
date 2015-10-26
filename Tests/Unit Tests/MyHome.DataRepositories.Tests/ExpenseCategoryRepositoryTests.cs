using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyHome.DataRepository;
using MyHome.TestUtils;
using System.Collections.Generic;
using MyHome.DataClasses;

namespace MyHome.DataRepositories.Tests
{
    [TestClass]
    public class ExpenseCategoryRepositoryTests
    {
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
            var mock = RepositoryMocks.GetMockExpenseCategoryRepository(new List<ExpenseCategory>() { new ExpenseCategory(1, "test")});
            var result = mock.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("test", result.Name);
        }
    }
}
