using HotelApp.BLL.Exceptions;
using HotelApp.BLL.Extensions.Audit.AuditHelpers;
using HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests.TestModels;
using Xunit;

namespace HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests
{
    public class ObjectHaveAnIdValidator
    {
        [Fact]
        public void TestWrongModelInput()
        {
            var auditValidator = new AuditSenderValidator<ModelWithoutId>();

            Action act = () => auditValidator.VerifyObjectHaveAnId();
            AuditSenderException exception = Assert.Throws<AuditSenderException>(act);
            Assert.Equal("The entered model does not contain an ID!", exception.Message);
        }

        [Fact]
        public void TestCorrectModelInput()
        {
            var auditValidator = new AuditSenderValidator<ModelWithId>();

            Action act = () => auditValidator.VerifyObjectHaveAnId();
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
