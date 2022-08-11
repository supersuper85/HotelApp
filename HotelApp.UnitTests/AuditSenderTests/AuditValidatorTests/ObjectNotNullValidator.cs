using HotelApp.BLL.Exceptions;
using HotelApp.BLL.Extensions.Audit.AuditHelpers;
using HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests.TestModels;
using Xunit;

namespace HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests
{
    public class ObjectNotNullValidator
    {
        [Fact]
        public void TestNullInput()
        {
            var auditValidator = new AuditSenderValidator<ModelWithoutId>();
            var inputObj = new ModelWithoutId(); 

            Action act = () => auditValidator.VerifyObjectNotNull(null, nameof(inputObj));
            AuditSenderException exception = Assert.Throws<AuditSenderException>(act);
            Assert.Equal($"The {nameof(inputObj)} object cannot be null!", exception.Message);
        }

        [Fact]
        public void TestNotNullInput()
        {
            var auditValidator = new AuditSenderValidator<ModelWithoutId>();
            var inputObj = new ModelWithoutId();

            Action act = () => auditValidator.VerifyObjectNotNull(inputObj, nameof(inputObj));
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
