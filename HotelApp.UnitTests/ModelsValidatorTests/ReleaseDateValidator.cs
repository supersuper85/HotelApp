using HotelApp.API.Exceptions;
using HotelApp.API.Validations;
using Xunit;

namespace HotelApp.UnitTests.ModelsValidatorTests
{
    public class ReleaseDateValidator
    {
        [Fact]
        public void TestDateIsPassed()
        {
            var validator = new ModelsValidator();
            var daysInPast = 1;
            DateTime releaseDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - daysInPast, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            Action act = () => validator.CheckReleaseDateGreaterThanNow(releaseDate, nameof(releaseDate));
            ModelValidationException exception = Assert.Throws<ModelValidationException>(act);
            Assert.Equal($"The {nameof(releaseDate)} has a value of a date that has already passed!", exception.Message);
        }

        [Fact]
        public void TestDateIsCorrect()
        {
            var validator = new ModelsValidator();
            var daysInFuture = 1;
            DateTime releaseDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + daysInFuture, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            Action act = () => validator.CheckReleaseDateGreaterThanNow(releaseDate, nameof(releaseDate));
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
