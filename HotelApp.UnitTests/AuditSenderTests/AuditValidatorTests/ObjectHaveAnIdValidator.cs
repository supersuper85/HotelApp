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
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectHaveAnId<ModelWithoutId>();
            AuditSenderException exception = Assert.Throws<AuditSenderException>(act);
            Assert.Equal("The entered model does not contain an ID!", exception.Message);
        }

        [Fact]
        public void TestCorrectModelInput()
        {
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectHaveAnId<ModelWithId>();
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
