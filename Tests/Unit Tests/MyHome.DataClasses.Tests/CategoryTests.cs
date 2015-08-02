using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyHome.DataClasses.Tests
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"name", AllowDerivedTypes = false)]
        public void Category_Throws_If_Name_Is_Empty_String()
        {
            var c = new Category(0, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "name", AllowDerivedTypes = false)]
        public void Category_Throws_If_Name_Is_Null()
        {
            var c = new Category(0, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "name", AllowDerivedTypes = false)]
        public void Category_Throws_If_Name_Is_Only_Whitespace()
        {
            var c = new Category(0, "    \t\r\n");
        }
    }
}
