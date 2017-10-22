using FrameWork;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyHome.Infrastructure.Tests
{
    [TestFixture]
    public class ExtensionsTests
    {
        [Test]
        public void Extensions_AddRange_With_Null_Target_Throws_Exception()
        {
            ICollection<int> target = null;

            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            void AddToNullCollection() => target.AddRange(Enumerable.Range(1, 10));

            // Assert
            var message = Assert.Throws<ArgumentNullException>(AddToNullCollection).Message;
            Assert.That(message, Is.EqualTo("Value cannot be null.\r\nParameter name: target"));
        }

        [Test]
        public void Extensions_AddRange_With_Null_Source_Throws_Exception()
        {
            ICollection<int> target = Enumerable.Range(1, 10).ToList();

            // Act
            void AddFromNullCollection() => target.AddRange(null);

            // Assert
            var message = Assert.Throws<ArgumentNullException>(AddFromNullCollection).Message;
            Assert.That(message, Is.EqualTo("Value cannot be null.\r\nParameter name: source"));
        }

        [Test]
        public void Extensions_AddRange_Adds_Every_Item()
        {
            // ReSharper disable PossibleMultipleEnumeration
            ICollection<int> target = Enumerable.Range(1, 10).ToList();
            var source = Enumerable.Range(11, 10);
            target.AddRange(source);

            CollectionAssert.IsSubsetOf(source, target);
            // ReSharper enable PossibleMultipleEnumeration
        }
    }
}
