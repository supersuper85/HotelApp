using HotelApp.API.Exceptions;
using HotelApp.API.Validations;
using Xunit;

namespace HotelApp.UnitTests.ModelsValidatorTests
{
    public class PriceValidator
    {
        [Fact]
        public void TestPriceIsLessThanOne()
        {
            var validator = new ModelsValidator();
            var price = 0.9f;

            Action act = () => validator.CheckPrice(price, nameof(price));
            ModelValidationException exception = Assert.Throws<ModelValidationException>(act);
            Assert.Equal($"The {nameof(price)} property cannot have a value less than 1.0$!", exception.Message);
        }

        [Fact]
        public void TestPriceIsGreaterThanOne()
        {
            var validator = new ModelsValidator();
            var price = 1.0f;

            Action act = () => validator.CheckPrice(price, nameof(price));
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
