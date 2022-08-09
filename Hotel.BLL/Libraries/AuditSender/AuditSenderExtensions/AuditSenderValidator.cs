using HotelApp.BLL.Exceptions;

namespace HotelApp.BLL.Extensions.Audit.AuditHelpers
{
    public class AuditSenderValidator
    {
        public void VerifyObjectIsNotInitialized(bool objectInitialized)
        {
            if (objectInitialized)
            {
                throw new AuditSenderException("The object is already initialized!");
            }
        }
        public void VerifyObjectDoesntHaveNewValues(bool objectHaveNewValues)
        {
            if (objectHaveNewValues)
            {
                throw new AuditSenderException("The NewValues ​​property has already been set!");
            }
        }
        public void VerifyObjectIsInitialized(bool objectInitialized)
        {
            if (!objectInitialized)
            {
                throw new AuditSenderException("The object must be initialized before sending a post request!");
            }
        }
        public void VerifyObjectHaveSetNewValues(bool objectHaveNewValues)
        {
            if (!objectHaveNewValues)
            {
                throw new AuditSenderException("The NewValues ​​property must be set before sending a post request!");
            }
        }
        public void VerifyActionType(string actionType)
        {
            if (actionType != "INSERT" && actionType != "DELETE" && actionType != "UPDATE")
            {
                throw new AuditSenderException("The ActionType entered is not valid!");
            }
        }
        public void VerifyObjectHaveIdOnNonInsert<T>(string actionType) where T : class
        {
            if (actionType != "INSERT")
            {
                VerifyObjectHaveAnId<T>();
            }
        }
        public void VerifyObjectHaveIdOnNonDelete<T>(string actionType) where T : class
        {
            if (actionType != "DELETE")
            {
                VerifyObjectHaveAnId<T>();
            }
        }
        public void VerifyObjectHaveAnId<T>()
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
        public void VerifyNewValueObjectIsSameTypeAsOldValueObject<T>(string auditType) where T : class
        {
            if (auditType != typeof(T).Name)
            {
                throw new AuditSenderException("The object entered as a setter for the NewValues ​​property does not correspond to the object used to set the OldValues ​​property!");
            }
        }
        
    }
}
