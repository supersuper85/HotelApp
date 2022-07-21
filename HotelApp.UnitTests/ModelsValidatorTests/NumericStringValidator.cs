using HotelApp.API.Exceptions;
using HotelApp.API.Validations;
using Xunit;

namespace HotelApp.UnitTests.ModelsValidatorTests
{
    public class NumericStringValidator
    {
        [Fact]
        public void TestWrongInputString()
        {
            var validator = new ModelsValidator();
            var text = "123c5";

            Action act = () => validator.CheckIsDigitsOnly(text, nameof(text));
            ModelValidationException exception = Assert.Throws<ModelValidationException>(act);
            Assert.Equal($"The {nameof(text)} property must contains just numbers!", exception.Message);
        }

        [Fact]
        public void TestCorrectInputString()
        {
            var validator = new ModelsValidator();
            var text = "123123123";

            Action act = () => validator.CheckIsDigitsOnly(text, nameof(text));
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
