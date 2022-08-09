using HotelApp.API.Models.ApartmentModels;

namespace HotelApp.API.Validations.ModelsValidations
{
    public class ApartmentValidator
    {
        public void CheckApartmentPostModel(ApartmentCreateModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckIntIsntEqualOrLessThanZero(model.NumberOfRooms, nameof(model.NumberOfRooms));
            validator.CheckIntIsntEqualOrLessThanZero(model.ApartmentNumber, nameof(model.ApartmentNumber));
            validator.CheckIntIsntEqualOrLessThanZero(model.HotelId, nameof(model.HotelId));

            validator.CheckPrice(model.DailyRentInEuro, nameof(model.DailyRentInEuro));
        }
        public void CheckApartmentPutModel(ApartmentEditModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckIntIsntEqualOrLessThanZero(model.NumberOfRooms, nameof(model.NumberOfRooms));
            validator.CheckIntIsntEqualOrLessThanZero(model.ApartmentNumber, nameof(model.ApartmentNumber));
            validator.CheckIntIsntEqualOrLessThanZero(model.Id, nameof(model.Id));

            validator.CheckPrice(model.DailyRentInEuro, nameof(model.DailyRentInEuro));
        }
        public void CheckApartmentDeleteModel(ApartmentDeleteModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckIntIsntEqualOrLessThanZero(model.Id, nameof(model.Id));
        }
    }
}
