using AuditApp.BLL.Dto;
using AuditApp.BLL.Exceptions;
using AuditApp.Data.Entities;
using AuditApp.Data.Interfaces;
using AutoMapper;

namespace AuditApp.BLL.Validations
{
    public class AuditDatabaseValidator
    {
        private readonly IAuditRepository _auditRepository;

        public AuditDatabaseValidator(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;

        }
        public void CheckAuditPutModel(Audit entity, AuditDto model)
        {
            CheckEntityExists(entity);
            CheckObjectPropertiesValueAreTheSame(entity, model);
        }
        private void CheckEntityExists(Audit entity)
        {
            if (entity == null)
            {
                throw new DatabaseValidatorException("The entered audit ID is not valid!");
            }
        }
        private void CheckObjectPropertiesValueAreTheSame<T,U>(T self, U to) where T : class where U : class
        {
            var areEqual = true;
            if (self != null && to != null)
            {
                Type typeT = typeof(T);
                Type typeU = typeof(U);
                foreach (System.Reflection.PropertyInfo pi in typeT.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    object selfValue = typeT.GetProperty(pi.Name).GetValue(self, null);
                    object toValue = typeU.GetProperty(pi.Name).GetValue(to, null);

                    if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                    {
                        areEqual = false;
                        break;
                    } 
                }
            }

            if (areEqual)
            {
                throw new DatabaseValidatorException("There is no difference between the entered model and the one in the database!");
            }
        }
        public void CheckAuditDeleteModel(Audit entity)
        {
            CheckEntityExists(entity);
        }
        
    }
}
