using HotelApp.BLL.Exceptions;

namespace HotelApp.BLL.Extensions.Audit.AuditHelpers
{

    public class AuditSenderValidator<T> where T : class
    {
        public void VerifyObjectHaveAnId()
        {
            Type typeT = typeof(T);
            foreach (System.Reflection.PropertyInfo pi in typeT.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (pi.Name == "Id")
                {
                    return;
                }
            }
            throw new AuditSenderException("The entered model does not contain an ID!");
        }
        public void VerifyObjectNotNull(T obj, string propertyName) 
        {
            if (obj == null)
            {
                throw new AuditSenderException($"The {propertyName} object cannot be null!");
            }
        }
        public void VerifyAuditHaveDifferentValues(T oldValuesObj, T newValuesObj) 
        {
            if (Newtonsoft.Json.JsonConvert.SerializeObject(oldValuesObj) == Newtonsoft.Json.JsonConvert.SerializeObject(newValuesObj))
            {
                throw new AuditSenderException("The NewValues ​​property cannot have the same error as the OldValues ​​property!");
            }
        }
        
    }
}
