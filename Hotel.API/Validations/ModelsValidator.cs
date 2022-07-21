using HotelApp.API.Exceptions;
using System.Text.RegularExpressions;

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
        public void CheckIntIsntEqualOrLessThanZero(int id, string propertyName)
        {
            if (id <= 0)
            {
                throw new ModelValidationException($"The {propertyName} property cannot have a value equal or less than 0!");
            }
        }
        public void CheckObjectIsntNull<T>(T obj, string propertyName) where T : class
        {
            if (obj == null)
            {
                throw new ModelValidationException($"The {propertyName} object can not be null!");
            }
        }
        public void CheckNameHaveCorrectLength(string name, string propertyName)
        {
            if (name.Length < 3 || name.Length > 100)
            {
                throw new ModelValidationException($"The {propertyName} property cannot be longer than 100 characters and less than 3 characters!");
            }
        }
        public void CheckNoSpecialCharacters(string text, string propertyName)
        {
            var matchedCharacters = Regex.Matches(text, @"[a-zA-Z]").Count;
            if (matchedCharacters != text.Length)
            {
                throw new ModelValidationException($"The {propertyName} property cannot contain special characters!");
            }
        }
        public void CheckPropertyHaveSpecificLength(string text, int specificLength, string propertyName)
        {
            if (text.Length != specificLength)
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
        public void CheckReleaseDateGreaterThanNow(DateTime dateTime, string propertyName)
        {
            if (dateTime.ToUniversalTime() < DateTime.UtcNow)
            {
                throw new ModelValidationException($"The {propertyName} has a value of a date that has already passed!");
            }
        }
        public void CheckReleaseDateHaveCorrectReleaseHour(DateTime dateTime, string propertyName)
        {
            var releaseHour = 12;
            var releaseMinute = 0;
            var releaseSecond = 0;
            if (dateTime.Hour != releaseHour || dateTime.Minute != releaseMinute || dateTime.Second != releaseSecond)
            {
                throw new ModelValidationException($"The {propertyName} must be set to 12:00:00 o'clock.");
            }
        }
    }
}
