using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Implementations;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;
using Moq;
using Xunit;
namespace HotelApp.UnitTests.ServicesTests
{
    public class CustomerServiceTests
    {
        private readonly CustomerService _sut;

        private readonly Mock<ICustomerRepository> _customerRepoMock;
        private readonly Mock<IApartmentRepository> _apartmentRepoMock;
        private readonly Mock<IRepository<Hotel>> _hotelRepoMock;
        private readonly Mock<IReservationRepository> _reservationRepoMock;
        private readonly Mock<IMapper> _mapperMock;

        public CustomerServiceTests()
        {
            _customerRepoMock = new Mock<ICustomerRepository>();
            _apartmentRepoMock = new Mock<IApartmentRepository>();
            _hotelRepoMock = new Mock<IRepository<Hotel>>();
            _reservationRepoMock = new Mock<IReservationRepository>();
            _mapperMock = new Mock<IMapper>();

            _sut = new CustomerService(
                _customerRepoMock.Object,
                _reservationRepoMock.Object,
                _mapperMock.Object
                );
        }


        [Fact]
        public async Task GetCustomer_ShouldReturnCustomer()
        {
            //Arrange
            var inputId = 1;

            var databaseReservation = GenerateDatabaseCustomerMockData();
            var outputReservation = GenerateCustomerDtoMockData();

            _customerRepoMock.Setup(x => x.GetCustomerById(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(databaseReservation);

            _mapperMock.Setup(x => x.Map<CustomerDto>(databaseReservation)).Returns(outputReservation);

            //Act
            var actualResult = await _sut.Get(It.IsAny<int>());

            //Assert
            Assert.Equal(actualResult.Id, inputId);
        }

        private Customer GenerateDatabaseCustomerMockData()
        {
            return new Customer
            {
                Id = 1,
                Name = "TestCustomer",
                CNP = "1111111111111" ,
            };
        }
        private CustomerDto GenerateCustomerDtoMockData()
        {
            return new CustomerDto
            {
                Id = 1,
                Name = "TestCustomer",
                CNP = "1111111111111",
            };
        }

        [Fact]
        public async Task AddCustomer_ShouldReturnCustomer()
        {
            //Arrange
            var databaseCustomer = GenerateDatabaseCustomerMockData();
            var inputCustomer = GenerateCustomerDtoMockData();
            var outputCustomer = GenerateCustomerDtoMockData();


            _mapperMock.Setup(x => x.Map<CustomerDto, Customer>(inputCustomer)).Returns(databaseCustomer);

            _customerRepoMock.Setup(x => x.ExistsAsync(x => x.CNP == It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(true);
            _customerRepoMock.Setup(x => x.AddAsync(databaseCustomer, CancellationToken.None)).ReturnsAsync(databaseCustomer);

            _mapperMock.Setup(x => x.Map<CustomerDto>(databaseCustomer)).Returns(outputCustomer);

            //Act
            var actualResult = await _sut.Add(inputCustomer);

            //Assert
            Assert.Equal(actualResult.Id, inputCustomer.Id);
        }

        [Fact]
        public async Task EditCustomer_ShouldReturnTrue()
        {
            //Arrange
            var databaseCustomer = GenerateDatabaseCustomerMockData();
            var inputCustomer = GenerateCustomerDtoMockData();
            var outputCustomer = GenerateCustomerDtoMockData();

            var inputId = 1;

            _customerRepoMock.Setup(x => x.ExistsAsync(x => x.Id == inputId, CancellationToken.None)).ReturnsAsync(true);
            _customerRepoMock.Setup(x => x.ExistsAsync(x => x.CNP == inputCustomer.CNP, CancellationToken.None)).ReturnsAsync(false);
            _customerRepoMock.Setup(x => x.ExistsAsync(x => x.CNP == inputCustomer.CNP && x.Id == inputCustomer.Id, CancellationToken.None)).ReturnsAsync(false);

            _mapperMock.Setup(x => x.Map<CustomerDto, Customer>(inputCustomer)).Returns(databaseCustomer);

            _customerRepoMock.Setup(x => x.UpdateAsync(databaseCustomer, CancellationToken.None)).ReturnsAsync(true);

            //Act
            var expectedResult = true;
            var actualResult = await _sut.Edit(inputCustomer);

            //Assert
            Assert.Equal(actualResult, expectedResult);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldReturnCustomer()
        {
            //Arrange
            var databaseCustomer = GenerateDatabaseCustomerMockData();
            var outputCustomer = GenerateCustomerDtoMockData();
            var inputId = 1;

            _reservationRepoMock.Setup(x => x.ExistsAsync(x => x.CustomerId == inputId, CancellationToken.None)).ReturnsAsync(false);
            _customerRepoMock.Setup(x => x.ExistsAsync(x => x.Id == inputId, CancellationToken.None)).ReturnsAsync(true);


            _customerRepoMock.Setup(x => x.SingleOrDefaultAsync(x => x.Id == inputId, CancellationToken.None)).ReturnsAsync(databaseCustomer);
            _customerRepoMock.Setup(x => x.DeleteAsync(databaseCustomer, CancellationToken.None)).ReturnsAsync(true);


            _mapperMock.Setup(x => x.Map<CustomerDto>(databaseCustomer)).Returns(outputCustomer);

            //Act
            var actualResult = await _sut.Delete(inputId);

            //Assert
            Assert.Equal(actualResult.Id, inputId);
        }
    }
}
