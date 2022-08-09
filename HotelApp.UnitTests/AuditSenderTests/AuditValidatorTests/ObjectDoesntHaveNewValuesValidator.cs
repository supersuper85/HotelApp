using HotelApp.BLL.Exceptions;
using HotelApp.BLL.Extensions.Audit.AuditHelpers;
using Xunit;


namespace HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests
{
    public class ObjectDoesntHaveNewValuesValidator
    {
        [Fact]
        public void TestObjectHaveNewValues()
        {
            var objectHaveNewValues = true;
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectDoesntHaveNewValues(objectHaveNewValues);
            AuditSenderException exception = Assert.Throws<AuditSenderException>(act);
            Assert.Equal("The NewValues ​​property has already been set!", exception.Message);
        }

        [Fact]
        public void TestObjectDoesntHaveNewValues()
        {
            var objectHaveNewValues = false;
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectDoesntHaveNewValues(objectHaveNewValues);
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
