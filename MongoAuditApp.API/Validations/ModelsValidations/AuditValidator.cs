using MongoAuditApp.API.Models;

namespace MongoAuditApp.API.Validations.ModelsValidations
{
    public class AuditValidator
    {
        public void CheckAuditPostModel(MongoAuditCreateModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckIntIsntEqualOrLessThanZero(model.EntityId, nameof(model.EntityId));

            validator.CheckDateIsInPast(model.TimeStamp, nameof(model.TimeStamp));

            validator.CheckAuditValuesAreDifferent(model.OldValues, model.NewValues);
        }
        public void CheckAuditPutModel(MongoAuditModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckIntIsntEqualOrLessThanZero(model.EntityId, nameof(model.EntityId));

            validator.CheckDateIsInPast(model.TimeStamp, nameof(model.TimeStamp));

            validator.CheckAuditValuesAreDifferent(model.OldValues, model.NewValues);
        }
    }
}
