using FrameWork;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Action addToNullCollection = () => target.AddRange(Enumerable.Range(1, 10));

            // Assert
            var message = Assert.Throws<ArgumentNullException>(() => addToNullCollection()).Message;
            Assert.That(message, Is.EqualTo("Value cannot be null.\r\nParameter name: target"));
        }

        [Test]
        public void Extensions_AddRange_With_Null_Source_Throws_Exception()
        {
            ICollection<int> target = Enumerable.Range(1, 10).ToList();

            // Act
            Action addFromNullCollection = () => target.AddRange(null);

            // Assert
            var message = Assert.Throws<ArgumentNullException>(() => addFromNullCollection()).Message;
            Assert.That(message, Is.EqualTo("Value cannot be null.\r\nParameter name: source"));
        }

        [Test]
        public void Extensions_AddRange_Adds_Every_Item()
        {
            ICollection<int> target = Enumerable.Range(1, 10).ToList();
            var source = Enumerable.Range(11, 10);
            target.AddRange(source);

            CollectionAssert.IsSubsetOf(source, target);
        }
    }
}
