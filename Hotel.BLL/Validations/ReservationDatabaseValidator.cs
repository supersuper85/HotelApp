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
        private readonly ICustomerRepository _customerRepository;

        public ReservationDatabaseValidator(IReservationRepository reservationRepository, ICustomerRepository customerRepository, IApartmentRepository apartmentRepository, IRepository<Hotel> hotelRepository)
        {
            _reservationRepository = reservationRepository;
            _apartmentRepository = apartmentRepository;
            _hotelRepository = hotelRepository;
            _customerRepository = customerRepository;
        }
        public async Task CheckReservationPostModel(ReservationDto model)
        {
            await CheckApartmentExists(model);
            await CheckHotelExists(model);
            await CheckCustomerAlreadyHaveRoomInHotel(model);
            await CheckApartmentAlreadyRent(model);
            await CheckCustomerExists(model);
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

            if (await _reservationRepository.ExistsAsync(x => x.CustomerId == model.CustomerId && x.HotelId == model.HotelId))
            {
                throw new DatabaseValidatorException("This CustomerId already owns an apartment in this hotel!");
            }
        }
        private async Task CheckApartmentModified(ReservationDto model, Reservation entity)
        {
            if (entity.ApartmentId != model.ApartmentId)
            {
                await CheckApartmentAlreadyRentWithoutHotelId(model, entity);
            }
        }
        private async Task CheckApartmentAlreadyRentWithoutHotelId(ReservationDto model, Reservation entity)
        {
            if (await _reservationRepository.ExistsAsync(x => x.ApartmentId == model.ApartmentId && x.HotelId == entity.HotelId))
            {
                throw new DatabaseValidatorException("The introduced apartment is already rented!");
            }
        }
        private async Task CheckApartmentAlreadyRent(ReservationDto model)
        {
            if (await _reservationRepository.ExistsAsync(x => x.ApartmentId == model.ApartmentId && x.HotelId == model.HotelId))
            {
                throw new DatabaseValidatorException("The introduced apartment is already rented!");
            }
        }
        private async Task CheckCustomerExists(ReservationDto model)
        {

            if (!await _customerRepository.ExistsAsync(x => x.Id == model.Customer.Id))
            {
                throw new DatabaseValidatorException("The entered customer ID is not valid!");
            }
        }


        public async Task CheckReservationPutModel(ReservationDto model, Reservation entity)
        {
            CheckObjectNotNull(entity);
            CheckIfReservationWasModified(model, entity);
            await CheckApartmentExists(model);
            await CheckApartmentModified(model, entity);
           
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
