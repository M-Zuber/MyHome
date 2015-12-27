using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyHome.DataClasses.Tests
{
    [TestClass]
    public class IncomeTests
    {
        [TestMethod]
        public void Equals_All_Fields_Are_Same()
        {
            var first = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            var second = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            Assert.AreNotSame(first, second);
            Assert.IsTrue(first.Equals(second));
            Assert.IsTrue(second.Equals(first));
        }

        [TestMethod]
        public void Equals_Amount_Differs()
        {
            var first = new Income
            {
                Amount = 150,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            var second = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            Assert.AreNotSame(first, second);
            Assert.IsFalse(first.Equals(second));
            Assert.IsFalse(second.Equals(first));
        }

        [TestMethod]
        public void Equals_Category_Differs()
        {
            var first = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Other_Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            var second = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            Assert.AreNotSame(first, second);
            Assert.IsFalse(first.Equals(second));
            Assert.IsFalse(second.Equals(first));
        }

        [TestMethod]
        public void Equals_Comment_Differs()
        {
            var first = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff, but not that kind",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            var second = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            Assert.AreNotSame(first, second);
            Assert.IsFalse(first.Equals(second));
            Assert.IsFalse(second.Equals(first));
        }

        [TestMethod]
        public void Equals_Date_Differs()
        {
            var first = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.MinValue,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            var second = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            Assert.AreNotSame(first, second);
            Assert.IsFalse(first.Equals(second));
            Assert.IsFalse(second.Equals(first));
        }

        [TestMethod]
        public void Equals_Id_Differs()
        {
            var first = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 10,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            var second = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            Assert.AreNotSame(first, second);
            Assert.IsFalse(first.Equals(second));
            Assert.IsFalse(second.Equals(first));
        }

        [TestMethod]
        public void Equals_Method_Differs()
        {
            var first = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Other_Method"
                }
            };

            var second = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            Assert.AreNotSame(first, second);
            Assert.IsFalse(first.Equals(second));
            Assert.IsFalse(second.Equals(first));
        }

        [TestMethod]
        public void GetHashCode_Two_Objects()
        {
            var first = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            var second = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            Assert.AreNotSame(first, second);
            Assert.AreNotEqual(first.GetHashCode(), second.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_One_Object()
        {
            var Income = new Income
            {
                Amount = 120,
                Category = new IncomeCategory
                {
                    Id = 0,
                    Name = "Category"
                },
                Comments = "Stuff",
                Date = DateTime.Today,
                Id = 0,
                Method = new PaymentMethod
                {
                    Id = 0,
                    Name = "Method"
                }
            };

            Assert.AreEqual(Income.GetHashCode(), Income.GetHashCode());
        }
    }
}
