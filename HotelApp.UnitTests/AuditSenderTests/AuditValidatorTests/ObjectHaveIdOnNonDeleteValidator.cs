using HotelApp.BLL.Exceptions;
using HotelApp.BLL.Extensions.Audit.AuditHelpers;
using HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests.TestModels;
using Xunit;

namespace HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests
{
    public class ObjectHaveIdOnNonDeleteValidator
    {
        [Fact]
        public void TestWrongModelInput_WithNonDeleteActionType()
        {
            var inputActionType = "UPDATE";
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectHaveIdOnNonDelete<ModelWithoutId>(inputActionType);
            AuditSenderException exception = Assert.Throws<AuditSenderException>(act);
            Assert.Equal("The entered model does not contain an ID!", exception.Message);
        }

        [Fact]
        public void TestWrongModelInput_WithDeleteActionType()
        {
            var inputActionType = "DELETE";
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectHaveIdOnNonDelete<ModelWithoutId>(inputActionType);
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }

        [Fact]
        public void TestCorrectModelInput_WithNonDeleteActionType()
        {
            var inputActionType = "UPDATE";
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectHaveIdOnNonDelete<ModelWithId>(inputActionType);
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }

        [Fact]
        public void TestCorrectModelInput_WithDeleteActionType()
        {
            var inputActionType = "DELETE";
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectHaveIdOnNonDelete<ModelWithId>(inputActionType);
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
