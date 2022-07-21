using HotelApp.API.Models.OtherInputModels;

namespace HotelApp.API.Validations.OtherInputModelsValidations
{
    public class IdModelValidator
    {
        public void CheckIdModel(IdModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckIdIsntEqualOrLessThanZero(model.Id, nameof(model.Id));
        }
    }
}
