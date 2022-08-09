using AuditApp.API.Models;

namespace AuditApp.API.Validations.ModelsValidations
{
    public class AuditValidator
    {
        public void CheckAuditPostModel(AuditCreateModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckIntIsntEqualOrLessThanZero(model.EntityId, nameof(model.EntityId));

            validator.CheckDateIsInPast(model.TimeStamp, nameof(model.TimeStamp));

            validator.CheckAuditValuesAreDifferent(model.OldValues, model.NewValues);
        }
        public void CheckAuditPutModel(AuditModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckIntIsntEqualOrLessThanZero(model.EntityId, nameof(model.EntityId));

            validator.CheckDateIsInPast(model.TimeStamp, nameof(model.TimeStamp));

            validator.CheckAuditValuesAreDifferent(model.OldValues, model.NewValues);
        }
        public void CheckAuditDeleteModel(AuditDeleteModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckIntIsntEqualOrLessThanZero(model.Id, nameof(model.Id));
        }
    }
}
