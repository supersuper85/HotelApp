using HotelApp.BLL.Exceptions;
using HotelApp.BLL.Extensions.Audit.AuditHelpers;
using HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests.TestModels;
using Xunit;

namespace HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests
{
    public class AuditHaveDifferentValuesValidator
    {
        [Fact]
        public void TestAuditHaveSameValues()
        {
            var auditValidator = new AuditSenderValidator<ModelWithId>();
            var oldValuesInput = new ModelWithId();
            oldValuesInput.Name = "Adrian";

            var newValuesInput = new ModelWithId();
            newValuesInput.Name = "Adrian";

            Action act = () => auditValidator.VerifyAuditHaveDifferentValues(oldValuesInput, newValuesInput);
            AuditSenderException exception = Assert.Throws<AuditSenderException>(act);
            Assert.Equal($"The NewValues ​​property cannot have the same error as the OldValues ​​property!", exception.Message);
        }

        [Fact]
        public void TestAuditHaveDifferentValues()
        {
            var auditValidator = new AuditSenderValidator<ModelWithId>();
            var oldValuesInput = new ModelWithId();
            oldValuesInput.Name = "Adrian";

            var newValuesInput = new ModelWithId();
            newValuesInput.Name = "George";

            Action act = () => auditValidator.VerifyAuditHaveDifferentValues(oldValuesInput, newValuesInput);
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
