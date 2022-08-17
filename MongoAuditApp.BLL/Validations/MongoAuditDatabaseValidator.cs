using MongoAuditApp.BLL.Exceptions;
using MongoAuditApp.Data.Entities;
using MongoAuditApp.Data.Interfaces;

namespace MongoAuditApp.BLL.Validations
{
    public class MongoAuditDatabaseValidator
    {
        public void CheckAuditDeleteModel(MongoAudit entity)
        {
            CheckEntityExists(entity);
        }
        private void CheckEntityExists(MongoAudit entity)
        {
            if (entity == null)
            {
                throw new DatabaseValidatorException("The entered audit ID is not valid!");
            }
        }

    }
}
