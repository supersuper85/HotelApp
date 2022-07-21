using HotelApp.API.Exceptions;
using HotelApp.API.Validations;
using Xunit;

namespace HotelApp.UnitTests.ModelsValidatorTests
{
    public class ReleaseTimeValidator
    {
        [Fact]
        public void TestInputTimeIsWrong()
        {
            var validator = new ModelsValidator();
            var releaseHour = 11;
            var releaseMinute = 1;
            var releaseSecond = 1;

            DateTime releaseDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, releaseHour, releaseMinute, releaseSecond);

            Action act = () => validator.CheckReleaseDateHaveCorrectReleaseHour(releaseDate, nameof(releaseDate));
            ModelValidationException exception = Assert.Throws<ModelValidationException>(act);
            Assert.Equal($"The {nameof(releaseDate)} must be set to 12:00:00 o'clock.", exception.Message);
        }

        [Fact]
        public void TestInputTimeIsCorrect()
        {
            var validator = new ModelsValidator();
            var releaseHour = 12;
            var releaseMinute = 0;
            var releaseSecond = 0;

            DateTime releaseDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, releaseHour, releaseMinute, releaseSecond);

            Action act = () => validator.CheckReleaseDateHaveCorrectReleaseHour(releaseDate, nameof(releaseDate));
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
