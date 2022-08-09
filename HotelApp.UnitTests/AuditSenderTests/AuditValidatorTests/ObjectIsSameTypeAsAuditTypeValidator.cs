using HotelApp.BLL.Exceptions;
using HotelApp.BLL.Extensions.Audit.AuditHelpers;
using HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests.TestModels;
using Xunit;

namespace HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests
{
    public class ObjectIsSameTypeAsAuditTypeValidator
    {
        [Fact]
        public void TestDifferentParametersType()
        {
            var auditType = "ModelWithId";
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyNewValueObjectIsSameTypeAsOldValueObject<ModelWithoutId>(auditType);
            AuditSenderException exception = Assert.Throws<AuditSenderException>(act);
            Assert.Equal("The object entered as a setter for the NewValues ​​property does not correspond to the object used to set the OldValues ​​property!", exception.Message);
        }

        [Fact]
        public void TestSameParametersType()
        {
            var auditType = "ModelWithoutId";
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyNewValueObjectIsSameTypeAsOldValueObject<ModelWithoutId>(auditType);
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
