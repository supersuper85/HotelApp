using HotelApp.API.Models.ReservationModels;

namespace HotelApp.API.Validations.ModelsValidations
{
    public class ReservationValidator
    {
        public void CheckReservationPostModel(ReservationCreateModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));
            validator.CheckObjectIsntNull(model.Customer, nameof(model.Customer));

            validator.CheckIntIsntEqualOrLessThanZero(model.NumberOfDays, nameof(model.NumberOfDays));
            validator.CheckIntIsntEqualOrLessThanZero(model.ApartmentId, nameof(model.ApartmentId));
            validator.CheckIntIsntEqualOrLessThanZero(model.HotelId, nameof(model.HotelId));

            validator.CheckIntIsntEqualOrLessThanZero(model.Customer.Id, nameof(model.Customer.Id));
        }
        public void CheckReservationPutModel(ReservationEditModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckIntIsntEqualOrLessThanZero(model.ApartmentId, nameof(model.ApartmentId));

            validator.CheckReleaseDateGreaterThanNow(model.ReleaseDate, nameof(model.ReleaseDate));
            validator.CheckReleaseDateHaveCorrectReleaseHour(model.ReleaseDate, nameof(model.ReleaseDate));
        }
        public void CheckReservationDeleteModel(ReservationDeleteModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckIntIsntEqualOrLessThanZero(model.Id, nameof(model.Id));
        }
    }
}
