using HotelApp.API.Validations;
using HotelApp.BLL.Exceptions;
using HotelApp.BLL.Extensions.Audit.AuditHelpers;
using Xunit;

namespace HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests
{
    public class ObjectIsInitializedValidator
    {
        [Fact]
        public void TestObjectIsNotInitialized()
        {
            var objectIsInitialized = false;
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectIsInitialized(objectIsInitialized);
            AuditSenderException exception = Assert.Throws<AuditSenderException>(act);
            Assert.Equal("The object must be initialized before sending a post request!", exception.Message);
        }

        [Fact]
        public void TestObjectIsInitialized()
        {
            var objectIsInitialized = true;
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectIsInitialized(objectIsInitialized);
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
