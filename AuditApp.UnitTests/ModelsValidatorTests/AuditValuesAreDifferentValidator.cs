using AuditApp.API.Exceptions;
using AuditApp.API.Validations;
using Xunit;

namespace AuditApp.UnitTests.ModelsValidatorTests
{
    public class AuditValuesAreDifferentValidator
    {
        [Fact]
        public void TestAuditValuesAreTheSame()
        {
            var validator = new ModelsValidator();
            var oldValues = "value1";
            var newValues = "value1";

            Action act = () => validator.CheckAuditValuesAreDifferent(oldValues, newValues);
            ModelValidationException exception = Assert.Throws<ModelValidationException>(act);
            Assert.Equal($"The OldValues and NewValues cannot have the same value!", exception.Message);
        }

        [Fact]
        public void TestDateIsInPast()
        {
            var validator = new ModelsValidator();
            var oldValues = "value1";
            string? newValues = null;

            Action act = () => validator.CheckAuditValuesAreDifferent(oldValues, newValues);
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
