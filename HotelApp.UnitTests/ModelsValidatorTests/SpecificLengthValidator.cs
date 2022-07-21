using HotelApp.API.Exceptions;
using HotelApp.API.Validations;
using Xunit;

namespace HotelApp.UnitTests.ModelsValidatorTests
{
    public class SpecificLengthValidator
    {
        [Fact]
        public void TestWrongInputLength()
        {
            var validator = new ModelsValidator();
            var text = "characters";
            var length = 9;

            Action act = () => validator.CheckPropertyHaveSpecificLength(text, length, nameof(text));
            ModelValidationException exception = Assert.Throws<ModelValidationException>(act);
            Assert.Equal($"The {nameof(text)} property must have {length} characters!", exception.Message);
        }

        [Fact]
        public void TestIdIsLessThanZero()
        {
            var validator = new ModelsValidator();
            var text = "characters";
            var length = 10;

            Action act = () => validator.CheckPropertyHaveSpecificLength(text, length, nameof(text));
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
