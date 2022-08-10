using AuditApp.API.Exceptions;
using AuditApp.API.Validations;
using Xunit;

namespace AuditApp.UnitTests.ModelsValidatorTests
{
    public class DateIsInPastValidator
    {
        [Fact]
        public void TestDateIsInFuture()
        {
            var validator = new ModelsValidator();
            var inputDate = DateTime.UtcNow.AddDays(1);

            Action act = () => validator.CheckDateIsInPast(inputDate, nameof(inputDate));
            ModelValidationException exception = Assert.Throws<ModelValidationException>(act);
            Assert.Equal($"The {nameof(inputDate)} has the value of a future date!", exception.Message);
        }

        [Fact]
        public void TestDateIsInPast()
        {
            var validator = new ModelsValidator();
            var inputDate = DateTime.UtcNow.AddDays(-1);

            Action act = () => validator.CheckDateIsInPast(inputDate, nameof(inputDate));
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
