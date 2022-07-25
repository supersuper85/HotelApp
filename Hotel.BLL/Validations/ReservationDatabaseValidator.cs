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
        private readonly IReservationRepository _reservationRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IRepository<Hotel> _hotelRepository;

        public ReservationDatabaseValidator(IReservationRepository reservationRepository, IApartmentRepository apartmentRepository, IRepository<Hotel> hotelRepository)
        {
            _reservationRepository = reservationRepository;
            _apartmentRepository = apartmentRepository;
            _hotelRepository = hotelRepository;
        }
        public ReservationDatabaseValidator(IReservationRepository reservationRepository, IApartmentRepository apartmentRepository)
        {
            _reservationRepository = reservationRepository;
            _apartmentRepository = apartmentRepository;
        }
        public ReservationDatabaseValidator(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
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

            if (!await _apartmentRepository.ExistsAsync(x => x.Id == model.ApartmentId))
            {
                throw new DatabaseValidatorException("The entered apartment ID is not valid!");
            }
        }
        private async Task CheckHotelExists(ReservationDto model)
        {
            if (!await _apartmentRepository.ExistsAsync(x => x.Id == model.HotelId))
            {
                throw new DatabaseValidatorException("The entered hotel ID is not valid!");
            }
        }
        private async Task CheckCustomerAlreadyHaveRoomInHotel(ReservationDto model)
        {

            if (await _reservationRepository.ExistsAsync(x => x.Customer.CNP == model.Customer.CNP) && await _reservationRepository.ExistsAsync(x => x.HotelId == model.HotelId))
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
            if (await _reservationRepository.ExistsAsync(x => x.ApartmentId == model.ApartmentId) && await _reservationRepository.ExistsAsync(x => x.HotelId == entity.HotelId))
            {
                throw new DatabaseValidatorException("The introduced apartment is already rented!");
            }
        }
        private async Task CheckApartmentAlreadyRent(ReservationDto model)
        {
            if (await _reservationRepository.ExistsAsync(x => x.ApartmentId == model.ApartmentId) && await _reservationRepository.ExistsAsync(x => x.HotelId == model.HotelId))
            {
                throw new DatabaseValidatorException("The introduced apartment is already rented!");
            }
        }

        public async Task CheckReservationPutModel(ReservationDto model, Reservation entity)
        {
            CheckObjectNotNull(entity);
            CheckIfReservationWasModified(model, entity);
            await CheckApartmentExists(model);
            CheckApartmentModified(model, entity);
           
        }
        private void CheckObjectNotNull(Reservation entity)
        {
            if (entity == null)
            {
                throw new DatabaseValidatorException("The reservation ID entered is invalid!");
            }
        }
        private void CheckIfReservationWasModified(ReservationDto model, Reservation entity)
        {
            if (model.ApartmentId == entity.ApartmentId && model.ReleaseDate == entity.ReleaseDate)
            {
                throw new DatabaseValidatorException("No change was detected between the model entered and the one from the database.");
            }
        }

        public async Task CheckReservationDeleteModel(Reservation entity)
        {
            CheckObjectNotNull(entity);
        }
    }
}
