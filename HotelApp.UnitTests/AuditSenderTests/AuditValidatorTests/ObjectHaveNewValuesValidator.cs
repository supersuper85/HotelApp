
using HotelApp.BLL.Exceptions;
using HotelApp.BLL.Extensions.Audit.AuditHelpers;
using Xunit;

namespace HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests
{
    public class ObjectHaveNewValuesValidator
    {
        [Fact]
        public void TestObjectDoesntHaveNewValues()
        {
            var objectHaveNewValues = false;
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectHaveSetNewValues(objectHaveNewValues);
            AuditSenderException exception = Assert.Throws<AuditSenderException>(act);
            Assert.Equal("The NewValues ​​property must be set before sending a post request!", exception.Message);
        }

        [Fact]
        public void TestObjectHaveNewValues()
        {
            var objectHaveNewValues = true;
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectHaveSetNewValues(objectHaveNewValues);
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
