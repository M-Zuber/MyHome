using System;
using MyHome.Infrastructure.Validation;
using NUnit.Framework;

namespace MyHome.Infrastructure.Tests.Validation
{
    [TestFixture]
    public class ContractTests
    {
        [Test]
        public void Contract_Requires_WithNoMessage_ShouldLeaveNoMessage()
        {
            // Act
            Action failingRequire = () => Contract.Requires<Exception>(false);

            // Assert
            var message = Assert.Throws<Exception>(() => failingRequire()).Message;
            Assert.That(message, Is.Empty);
        }

        [Test]
        public void Contract_Requires_WithMessage_ShouldUseGivenMessage()
        {
            // Arrange
            const string exceptionMessage = "TheExceptionMessage";

            // Act
            Action failingRequire = () => Contract.Requires<Exception>(false, exceptionMessage);

            // Assert
            var actualMessage = Assert.Throws<Exception>(() => failingRequire()).Message;
            Assert.That(actualMessage, Is.EqualTo(exceptionMessage));
        }
    }
}
