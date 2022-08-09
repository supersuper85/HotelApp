using HotelApp.BLL.Exceptions;
using HotelApp.BLL.Extensions.Audit.AuditHelpers;
using Xunit;

namespace HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests
{
    public class ActionTypeValidator
    {
        [Fact]
        public void TestWrongActionType()
        {
            var inputActionType = "wrong action type";
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyActionType(inputActionType);
            AuditSenderException exception = Assert.Throws<AuditSenderException>(act);
            Assert.Equal("The ActionType entered is not valid!", exception.Message);
        }

        [Fact]
        public void TestCorrectActionType()
        {
            var inputActionType = "DELETE";
            var auditValidator = new AuditSenderValidator();

            Action act = () => auditValidator.VerifyActionType(inputActionType);
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
