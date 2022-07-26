using HotelApp.API.Exceptions;
using HotelApp.API.Validations;
using Xunit;

namespace HotelApp.UnitTests.ModelsValidatorTests
{
    public class NoSpecialCharactersValidator
    {
        [Fact]
        public void TestTextHaveSpecialCharacters()
        {
            var validator = new ModelsValidator();
            var text = "asd?";

            Action act = () => validator.CheckNoSpecialCharacters(text, nameof(text));
            ModelValidationException exception = Assert.Throws<ModelValidationException>(act);
            Assert.Equal($"The {nameof(text)} property cannot contain special characters!", exception.Message);
        }

        [Fact]
        public void TestNameHaveCorrectLength()
        {
            var validator = new ModelsValidator();
            var text = "asd";

            Action act = () => validator.CheckNoSpecialCharacters(text, nameof(text));
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
