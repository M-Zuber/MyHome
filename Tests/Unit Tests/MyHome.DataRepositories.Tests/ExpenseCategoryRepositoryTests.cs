using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyHome.DataRepository;

namespace MyHome.DataRepositories.Tests
{
    [TestClass]
    public class ExpenseCategoryRepositoryTests
    {
        private Mock<ExpenseCategoryRepository> GetMock()
        {
            return new Mock<ExpenseCategoryRepository>();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
