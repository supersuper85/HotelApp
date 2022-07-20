using HotelApp.API.Exceptions;

namespace HotelApp.API.Validations
{
    public class ModelsValidator
    { 
        public void CheckAge(int age)
        {
            if(age < 18)
            {
                throw new ModelValidationException("The age cannot be less than 18 years old!");
            }
        }
        public void CheckIdIsntZero(int id, string propertyName)
        {
            if (id == 0)
            {
                throw new ModelValidationException($"The {propertyName} property cannot have a value of 0!");
            }
        }
        public void CheckObjectIsntNull<T>(T obj, string propertyName) where T : class
        {
            if (obj == null)
            {
                throw new ModelValidationException($"The {propertyName} object can not be null!");
            }
        }
        public void CheckNameIsValid(string name, string propertyName)
        {
            if (name.Length < 3 && name.Length > 100)
            {
                throw new ModelValidationException($"The {propertyName} cannot be longer than 100 characters and less than 3 characters");
            }
        }
        public void CheckPropertyHaveSpecificLength(string name, int specificLength, string propertyName)
        {
            if (name.Length != specificLength)
            {
                throw new ModelValidationException($"The {propertyName} property must have {specificLength} characters!");
            }
        }
        public void CheckIsDigitsOnly(string property, string propertyName)
        {
            foreach (char c in property)
            {
                if (c < '0' || c > '9')
                {
                    throw new ModelValidationException($"The {propertyName} property must contains just numbers!");
                }    
            }
        }
        public void CheckValidDateRelease(DateTime dateTime, string propertyName)
        {
            var releaseHour = 12;
            var releaseMinute = 0;
            var releaseSecond = 0;
            if (dateTime.Hour != releaseHour || dateTime.Minute != releaseMinute || dateTime.Second != releaseSecond)
            {
                throw new ModelValidationException($"The {propertyName} must be set to 12:00:00 o'clock.");
            }
            if (dateTime.ToUniversalTime() < DateTime.UtcNow)
            {
                throw new ModelValidationException($"The {propertyName} property is entered incorrectly!");
            }

        }
    }
}
