﻿using HotelApp.API.Models.ReservationModels;

namespace HotelApp.API.Validations.ModelsValidations
{
    public class ReservationValidator
    {
        public void CheckReservationPostModel(ReservationPostModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));
            validator.CheckObjectIsntNull(model.Customer, nameof(model.Customer));

            validator.CheckIdIsntZero(model.NumberOfDays, nameof(model.NumberOfDays));
            validator.CheckIdIsntZero(model.ApartmentId, nameof(model.ApartmentId));
            validator.CheckIdIsntZero(model.HotelId, nameof(model.HotelId));

            validator.CheckAge(model.Customer.Age);

            validator.CheckNameIsValid(model.Customer.Name, nameof(model.Customer.Name));

            validator.CheckPropertyHaveSpecificLength(model.Customer.CNP, 13, nameof(model.Customer.CNP));

            validator.CheckIsDigitsOnly(model.Customer.CNP, nameof(model.Customer.CNP));
        }
        public void CheckReservationPutModel(ReservationPutModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));

            validator.CheckIdIsntZero(model.ApartmentId, nameof(model.ApartmentId));

            validator.CheckValidDateRelease(model.ReleaseDate, nameof(model.ReleaseDate));
            
        }
        public void CheckReservationDeleteModel(ReservationDeleteModel model)
        {
            var validator = new ModelsValidator();
            validator.CheckObjectIsntNull(model, nameof(model));
            validator.CheckObjectIsntNull(model.Customer, nameof(model.Customer));

            validator.CheckIdIsntZero(model.Id, nameof(model.Id));
            validator.CheckIdIsntZero(model.Customer.Id, nameof(model.Customer.Id));
        }
    }
}