using HotelApp.API.Exceptions;
using HotelApp.API.Validations;
using HotelApp.Data.Entities;
using Xunit;

namespace HotelApp.UnitTests.ModelsValidatorTests
{
    public class NullObjectValidator
    {
        [Fact]
        public void TestObjectIsNull()
        {
            var validator = new ModelsValidator();
            Reservation obj = null;

            Action act = () => validator.CheckObjectIsntNull(obj, nameof(obj));
            ModelValidationException exception = Assert.Throws<ModelValidationException>(act);
            Assert.Equal($"The {nameof(obj)} object can not be null!", exception.Message);
        }

        [Fact]
        public void TestObjectIsntNull()
        {
            var validator = new ModelsValidator();
            Reservation obj = new Reservation();

            Action act = () => validator.CheckObjectIsntNull(obj, nameof(obj));
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
