using HotelApp.API.Exceptions;
using HotelApp.API.Validations;
using Xunit;

namespace HotelApp.UnitTests.ModelsValidatorTests
{
    public class NameLengthValidator
    {
        [Fact]
        public void TestNameHaveLessCharactersThanThree()
        {
            var validator = new ModelsValidator();
            var name = "as";

            Action act = () => validator.CheckNameHaveCorrectLength(name, nameof(name));
            ModelValidationException exception = Assert.Throws<ModelValidationException>(act);
            Assert.Equal($"The {nameof(name)} property cannot be longer than 100 characters and less than 3 characters!", exception.Message);
        }

        [Fact]
        public void TestNameHaveMoreCharactersThanOneHundred()
        {
            var validator = new ModelsValidator();
            var name = "asdassdadsadsadsasdadsasdaasdasdasuisadfbuyasfgbasuyfbsuyfbfuybsayfbaspdfuwhdoqwiuhdaoiuwdhuLAWUdhAid";

            Action act = () => validator.CheckNameHaveCorrectLength(name, nameof(name));
            ModelValidationException exception = Assert.Throws<ModelValidationException>(act);
            Assert.Equal($"The {nameof(name)} property cannot be longer than 100 characters and less than 3 characters!", exception.Message);
        }

        [Fact]
        public void TestNameHaveCorrectLength()
        {
            var validator = new ModelsValidator();
            var name = "asdassdadsadsadsasdadsasdaasdasdasuisadfbuyasfgbasuyfbsuyfbfuybsayfbaspdfuwhdoqwiuhdaoiuwdhuLAWUdhA";

            Action act = () => validator.CheckNameHaveCorrectLength(name, nameof(name));
            var exception = Record.Exception(() => act());
            Assert.Null(exception);
        }
    }
}
