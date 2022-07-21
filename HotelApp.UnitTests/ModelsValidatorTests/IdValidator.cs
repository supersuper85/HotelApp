using HotelApp.API.Exceptions;
using HotelApp.API.Validations;
using Xunit;

namespace HotelApp.UnitTests.ModelsValidatorTests
{

    public class IdValidator
    {
        [Fact]
        public void TestIdEqualZero()
        {
            var validator = new ModelsValidator();
            var id = 0;

            Action act = () => validator.CheckIntIsntEqualOrLessThanZero(id, nameof(id));
            ModelValidationException exception = Assert.Throws<ModelValidationException>(act);
            Assert.Equal($"The {nameof(id)} property cannot have a value equal or less than 0!", exception.Message);
        }

        [Fact]
        public void TestIdIsLessThanZero()
        {
            var validator = new ModelsValidator();
            var id = -1;

            Action act = () => validator.CheckIntIsntEqualOrLessThanZero(id, nameof(id));
            ModelValidationException exception = Assert.Throws<ModelValidationException>(act);
            Assert.Equal($"The {nameof(id)} property cannot have a value equal or less than 0!", exception.Message);
        }

        [Fact]
        public void TestIdIsGreaterThanZero()
        {
            var validator = new ModelsValidator();
            var id = 1;

            Action act = () => validator.CheckIntIsntEqualOrLessThanZero(id, nameof(id));
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
