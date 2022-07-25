using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Implementations;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;
using Moq;
using Xunit;

namespace HotelApp.UnitTests.ServicesTests
{
    public class ReservationServiceTests
    {
        private readonly ReservationService _sut;

        private readonly Mock<ICustomerRepository> _customerRepoMock;
        private readonly Mock<IApartmentRepository> _apartmentRepoMock;
        private readonly Mock<IRepository<Hotel>> _hotelRepoMock;
        private readonly Mock<IReservationRepository> _reservationRepoMock;
        private readonly Mock<IMapper> _mapperMock;


        public ReservationServiceTests()
        {
            _customerRepoMock = new Mock<ICustomerRepository>();
            _apartmentRepoMock = new Mock<IApartmentRepository>();
            _hotelRepoMock = new Mock<IRepository<Hotel>>();
            _reservationRepoMock = new Mock<IReservationRepository>();
            _mapperMock = new Mock<IMapper>();

            _sut = new ReservationService(
                _reservationRepoMock.Object,
                _customerRepoMock.Object,
                _apartmentRepoMock.Object,
                _hotelRepoMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task GetReservationWithIncludedCustomer_ShouldReturnReservation()
        {
            //Arrange
            var inputId = 1;

            var databaseReservation = GenerateDatabaseReservationMockData();
            var outputReservation = GenerateReservationDtoMockData();

            _reservationRepoMock.Setup(x => x.GetReservationById(It.IsAny<int>())).ReturnsAsync(databaseReservation);

            _mapperMock.Setup(x => x.Map<ReservationDto>(databaseReservation)).Returns(outputReservation);

            //Act
            var actualResult = await _sut.GetAReservationById(It.IsAny<int>());

            //Assert
            Assert.Equal(actualResult.Id, inputId);
        }

        

        [Fact]
        public async Task AddReservationWithIncludedCustomer_ShouldReturnReservation()
        {
            //Arrange
            var databaseReservation = GenerateDatabaseReservationMockData();

            var apartmentId = 1;
            var databaseApartment = GenerateDatabaseApartmentMockData(apartmentId);

            var inputReservation = GenerateReservationDtoMockData();
            var outputReservation = GenerateReservationDtoMockData();



            _mapperMock.Setup(x => x.Map<ReservationDto>(databaseReservation)).Returns(outputReservation);
            _mapperMock.Setup(x => x.Map<ReservationDto, Reservation>(inputReservation)).Returns(databaseReservation);



            _apartmentRepoMock.Setup(x => x.ExistsAsync(x => x.Id == inputReservation.ApartmentId, CancellationToken.None)).ReturnsAsync(true);
            _apartmentRepoMock.Setup(x => x.ExistsAsync(x => x.Id == inputReservation.HotelId, CancellationToken.None)).ReturnsAsync(true);

            _reservationRepoMock.Setup(x => x.ExistsAsync(x => x.Customer.CNP == inputReservation.Customer.CNP, CancellationToken.None)).ReturnsAsync(false);
            _reservationRepoMock.Setup(x => x.ExistsAsync(x => x.HotelId == inputReservation.HotelId, CancellationToken.None)).ReturnsAsync(false);

            _reservationRepoMock.Setup(x => x.ExistsAsync(x => x.ApartmentId == inputReservation.ApartmentId, CancellationToken.None)).ReturnsAsync(false);

            

            _reservationRepoMock.Setup(x => x.AddAsync(databaseReservation, CancellationToken.None)).ReturnsAsync(databaseReservation);

            _apartmentRepoMock.Setup(x => x.SingleOrDefaultAsync(x => x.Id == inputReservation.ApartmentId, CancellationToken.None)).ReturnsAsync(databaseApartment);
            _apartmentRepoMock.Setup(x => x.UpdateAsync(databaseApartment , CancellationToken.None)).ReturnsAsync(true);


            //Act
            var actualResult = await _sut.Add(inputReservation);

            //Assert
            Assert.Equal(actualResult.Id, inputReservation.Id);
        }

        private Reservation GenerateDatabaseReservationMockData()
        {
            return new Reservation
            {
                Id = 1,
                Customer = new Customer { Id = 1, Name = "TestCustomer", CNP = "1111111111111" },
                RegistrationDate = new DateTime(),
                HotelId = 1,
                ApartmentId = 1,
            };
        }
        private ReservationDto GenerateReservationDtoMockData()
        {
            return new ReservationDto
            {
                Id = 1,
                Customer = new CustomerDto { Id = 1, Name = "TestCustomer", CNP = "1111111111111" },
                RegistrationDate = new DateTime(),
                HotelId = 1,
                ApartmentId = 1,
            };
        }

        private Apartment GenerateDatabaseApartmentMockData(int id)
        {
            return new Apartment
            {
                Id = id,
                HotelId = 1,
            };
        }

        [Fact]
        public async Task EditReservation_ShouldReturnTrue()
        {
            //Arrange
            var databaseReservation = GenerateDatabaseReservationMockData();

            var oldApartmentId = 1;
            var oldDatabaseApartment = GenerateDatabaseApartmentMockData(oldApartmentId);

            var newApartmentId = 2;
            var newDatabaseApartment = GenerateDatabaseApartmentMockData(newApartmentId);

            var inputReservation = GenerateReservationDtoMockData();
            inputReservation.ApartmentId = 2;

            var outputReservation = GenerateReservationDtoMockData();


            _reservationRepoMock.Setup(x => x.GetReservationById(It.IsAny<int>())).ReturnsAsync(databaseReservation);
            _apartmentRepoMock.Setup(x => x.ExistsAsync(x => x.Id == inputReservation.ApartmentId, CancellationToken.None)).ReturnsAsync(true);

            _apartmentRepoMock.Setup(x => x.SingleOrDefaultAsync(x => x.Id == inputReservation.ApartmentId, CancellationToken.None)).ReturnsAsync(newDatabaseApartment);
            _apartmentRepoMock.Setup(x => x.SingleOrDefaultAsync(x => x.Id == databaseReservation.ApartmentId, CancellationToken.None)).ReturnsAsync(oldDatabaseApartment);


            _reservationRepoMock.Setup(x => x.UpdateAsync(databaseReservation, CancellationToken.None)).ReturnsAsync(true);
            _customerRepoMock.Setup(x => x.UpdateAsync(databaseReservation.Customer, CancellationToken.None)).ReturnsAsync(true);
            _apartmentRepoMock.Setup(x => x.UpdateAsync(newDatabaseApartment, CancellationToken.None)).ReturnsAsync(true);
            _apartmentRepoMock.Setup(x => x.UpdateAsync(oldDatabaseApartment, CancellationToken.None)).ReturnsAsync(true);


            //Act
            var expectedResult = true;
            var actualResult = await _sut.EditAReservation(inputReservation);

            //Assert
            Assert.Equal(actualResult, expectedResult);
        }

        [Fact]
        public async Task DeleteReservation_ShouldReturnReservation()
        {
            //Arrange
            var databaseReservation = GenerateDatabaseReservationMockData();

            var apartmentId = 1;
            var databaseApartment = GenerateDatabaseApartmentMockData(apartmentId);

            var inputId = 1;
            var outputReservation = GenerateReservationDtoMockData();


            _reservationRepoMock.Setup(x => x.GetReservationById(It.IsAny<int>())).ReturnsAsync(databaseReservation);
            _apartmentRepoMock.Setup(x => x.SingleOrDefaultAsync(x => x.Id == databaseReservation.ApartmentId, CancellationToken.None)).ReturnsAsync(databaseApartment);


            _customerRepoMock.Setup(x => x.DeleteAsync(databaseReservation.Customer, CancellationToken.None)).ReturnsAsync(true);
            _reservationRepoMock.Setup(x => x.DeleteAsync(databaseReservation, CancellationToken.None)).ReturnsAsync(true);
            _apartmentRepoMock.Setup(x => x.UpdateAsync(databaseApartment, CancellationToken.None)).ReturnsAsync(true);


            _mapperMock.Setup(x => x.Map<ReservationDto>(databaseReservation)).Returns(outputReservation);

            //Act
            var actualResult = await _sut.Delete(inputId);

            //Assert
            Assert.Equal(actualResult.Id, inputId);
        }
    }
}
