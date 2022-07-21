using HotelApp.API.Exceptions;
using HotelApp.API.Validations;
using Xunit;

namespace HotelApp.UnitTests.ModelsValidatorTests
{
    public class AgeValidator
    {
        [Fact]
        public void TestAgeIsLessThanEighteen()
        {
            var validator = new ModelsValidator();
            var age = 17;

            Action act = () => validator.CheckAge(age);
            ModelValidationException exception = Assert.Throws<ModelValidationException>(act);
            Assert.Equal("The age cannot be less than 18 years old!", exception.Message);
        }

        [Fact]
        public void TestAgeIsGreaterThanEighteen()
        {
            var validator = new ModelsValidator();
            var age = 18;

            Action act = () => validator.CheckAge(age);
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
