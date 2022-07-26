using HotelApp.API.Models.CustomerModels;

namespace HotelApp.API.Validations.ModelsValidations
{
    public class CustomerValidator
    {
        public void CheckCustomerPostModel(CustomerCreateModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckAge(model.Age);

            validator.CheckNameHaveCorrectLength(model.Name, nameof(model.Name));
            validator.CheckNoSpecialCharacters(model.Name, nameof(model.Name));

            validator.CheckPropertyHaveSpecificLength(model.CNP, 13, nameof(model.CNP));

            validator.CheckIsDigitsOnly(model.CNP, nameof(model.CNP));
        }

        public void CheckCustomerPutModel(CustomerEditModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckIntIsntEqualOrLessThanZero(model.Id, nameof(model.Id));

            validator.CheckAge(model.Age);

            validator.CheckNameHaveCorrectLength(model.Name, nameof(model.Name));
            validator.CheckNoSpecialCharacters(model.Name, nameof(model.Name));

            validator.CheckPropertyHaveSpecificLength(model.CNP, 13, nameof(model.CNP));

            validator.CheckIsDigitsOnly(model.CNP, nameof(model.CNP));
        }

        public void CheckReservationDeleteModel(CustomerDeleteModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckIntIsntEqualOrLessThanZero(model.Id, nameof(model.Id));
        }
    }
}
