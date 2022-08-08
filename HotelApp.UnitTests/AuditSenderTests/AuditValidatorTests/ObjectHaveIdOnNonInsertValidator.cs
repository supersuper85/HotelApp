using HotelApp.BLL.Exceptions;
using HotelApp.BLL.Extensions.Audit.AuditHelpers;
using HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests.TestModels;
using Xunit;

namespace HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests
{
    public class ObjectHaveIdOnNonInsertValidator
    {
        [Fact]
        public void TestWrongModelInput_WithNonInsertActionType()
        {
            var inputActionType = "UPDATE";
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectHaveIdOnNonInsert<ModelWithoutId>(inputActionType);
            AuditSenderException exception = Assert.Throws<AuditSenderException>(act);
            Assert.Equal("The entered model does not contain an ID!", exception.Message);
        }

        [Fact]
        public void TestWrongModelInput_WithInsertActionType()
        {
            var inputActionType = "INSERT";
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectHaveIdOnNonInsert<ModelWithoutId>(inputActionType);
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }

        [Fact]
        public void TestCorrectModelInput_WithNonInsertActionType()
        {
            var inputActionType = "UPDATE";
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectHaveIdOnNonInsert<ModelWithId>(inputActionType);
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }

        [Fact]
        public void TestCorrectModelInput_WithInsertActionType()
        {
            var inputActionType = "INSERT";
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyObjectHaveIdOnNonInsert<ModelWithId>(inputActionType);
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
