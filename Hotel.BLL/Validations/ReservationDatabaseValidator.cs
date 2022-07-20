using HotelApp.BLL.Dto;
using HotelApp.BLL.Exceptions;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.BLL.Validations
{
    public class ReservationDatabaseValidator 
    {
        private readonly IRepository<Reservation> _defaultReservationRepository;
        private readonly IRepository<Apartment> _defaultApartmentRepository;
        private readonly IRepository<Hotel> _defaultHotelRepository;

        public ReservationDatabaseValidator(IRepository<Reservation> defaultReservationRepository, IRepository<Apartment> defaultApartmentRepository, IRepository<Hotel> defaultHotelRepository)
        {
            _defaultReservationRepository = defaultReservationRepository;
            _defaultApartmentRepository = defaultApartmentRepository;
            _defaultHotelRepository = defaultHotelRepository;
        }
        public ReservationDatabaseValidator(IRepository<Reservation> defaultReservationRepository, IRepository<Apartment> defaultApartmentRepository)
        {
            _defaultReservationRepository = defaultReservationRepository;
            _defaultApartmentRepository = defaultApartmentRepository;
        }
        public ReservationDatabaseValidator(IRepository<Reservation> defaultReservationRepository)
        {
            _defaultReservationRepository = defaultReservationRepository;
        }
        public async Task CheckReservationPostModel(ReservationDto model)
        {
            await CheckApartmentExists(model);
            await CheckHotelExists(model);
            await CheckCustomerAlreadyHaveRoomInHotel(model);
            await CheckApartmentAlreadyRent(model);
        }
        private async Task CheckApartmentExists(ReservationDto model)
        {

            if (!await _defaultApartmentRepository.ExistsAsync(x => x.Id == model.ApartmentId))
            {
                throw new DatabaseValidatorException("The entered apartment ID is not valid!");
            }
        }
        private async Task CheckHotelExists(ReservationDto model)
        {
            if (!await _defaultApartmentRepository.ExistsAsync(x => x.Id == model.HotelId))
            {
                throw new DatabaseValidatorException("The entered hotel ID is not valid!");
            }
        }
        private async Task CheckCustomerAlreadyHaveRoomInHotel(ReservationDto model)
        {

            if (await _defaultReservationRepository.ExistsAsync(x => x.Customer.CNP == model.Customer.CNP) && await _defaultReservationRepository.ExistsAsync(x => x.HotelId == model.HotelId))
            {
                throw new DatabaseValidatorException("Someone with this CNP already owns an apartment in this hotel!");
            }
        }
        private async Task CheckApartmentModified(ReservationDto model, Reservation entity)
        {
            if (entity.ApartmentId != model.ApartmentId)
            {
                await CheckApartmentAlreadyRent(model, entity);
            }
        }
        private async Task CheckApartmentAlreadyRent(ReservationDto model, Reservation entity)
        {
            if (await _defaultReservationRepository.ExistsAsync(x => x.ApartmentId == model.ApartmentId) && await _defaultReservationRepository.ExistsAsync(x => x.HotelId == entity.HotelId))
            {
                throw new DatabaseValidatorException("The introduced apartment is already rented!");
            }
        }
        private async Task CheckApartmentAlreadyRent(ReservationDto model)
        {
            if (await _defaultReservationRepository.ExistsAsync(x => x.ApartmentId == model.ApartmentId) && await _defaultReservationRepository.ExistsAsync(x => x.HotelId == model.HotelId))
            {
                throw new DatabaseValidatorException("The introduced apartment is already rented!");
            }
        }
        

        public async Task CheckReservationDeleteModel(ReservationDto model, Reservation entity)
        {
            CheckObjectNotNull(entity);
            CheckCorrelationsBetweenReservations(model, entity);
        }
        private void CheckObjectNotNull(Reservation entity)
        {
            if (entity == null)
            { 
                throw new DatabaseValidatorException("The reservation ID entered is invalid!");
            }
        }

        private void CheckCorrelationsBetweenReservations(ReservationDto model, Reservation entity)
        {
            if(model.Customer.Id != entity.Customer.Id)
            {
                throw new DatabaseValidatorException("The customer ID entered is not in accordance with the customer ID from the database!");
            }
        }

        public async Task CheckReservationPutModel(ReservationDto model, Reservation entity)
        {
            CheckObjectNotNull(entity);
            CheckIfReservationWasModified(model, entity);
            await CheckApartmentExists(model);
            await CheckApartmentModified(model, entity);
           
        }
        private void CheckIfReservationWasModified(ReservationDto model, Reservation entity)
        {
            if (model.ApartmentId == entity.ApartmentId && model.ReleaseDate == entity.ReleaseDate)
            {
                throw new DatabaseValidatorException("No change was detected between the model entered and the one in the database.");
            }
        }
    }
}
