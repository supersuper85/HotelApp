using AuditApp.API.Exceptions;
using System.Text.RegularExpressions;

namespace AuditApp.API.Validations
{
    public class ModelsValidator
    { 
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

        public void CheckDateIsInPast(DateTime dateTime, string propertyName)
        {
            if (dateTime.ToUniversalTime() > DateTime.UtcNow)
            {
                throw new ModelValidationException($"The {propertyName} has the value of a future date!");
            }
        }
        public void CheckAuditValuesAreDifferent(string? oldValuesString, string? newValuesString)
        {
            if(oldValuesString == newValuesString)
            {
                throw new ModelValidationException($"The OldValues and NewValues cannot have the same value!");
            }
        }
    }
}
