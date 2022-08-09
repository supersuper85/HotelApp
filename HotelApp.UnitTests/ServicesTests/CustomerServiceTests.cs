using AutoMapper;
using HotelApp.BLL.Constants;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Implementations;
using HotelApp.BLL.Models.AuditModels;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace HotelApp.UnitTests.ServicesTests
{
    public class CustomerServiceTests
    {
        private CustomerService _sut;

        private readonly Mock<ICustomerRepository> _customerRepoMock;
        private readonly Mock<IApartmentRepository> _apartmentRepoMock;
        private readonly Mock<IRepository<Hotel>> _hotelRepoMock;
        private readonly Mock<IReservationRepository> _reservationRepoMock;
        private readonly Mock<HttpClient> _httpClientMock;
        private readonly Mock<IMapper> _mapperMock;

        public CustomerServiceTests()
        {
            _customerRepoMock = new Mock<ICustomerRepository>();
            _apartmentRepoMock = new Mock<IApartmentRepository>();
            _hotelRepoMock = new Mock<IRepository<Hotel>>();
            _httpClientMock = new Mock<HttpClient>();
            _reservationRepoMock = new Mock<IReservationRepository>();
            _mapperMock = new Mock<IMapper>();

            _sut = new CustomerService(
                _customerRepoMock.Object,
                _reservationRepoMock.Object,
                _mapperMock.Object,
                _httpClientMock.Object
                );
        }


        [Fact]
        public async Task GetCustomer_ShouldReturnCustomer()
        {
            //Arrange
            var inputId = 1;

            var databaseCustomer = GenerateDatabaseCustomerMockData();
            var outputCustomer = GenerateCustomerDtoMockData();

            _customerRepoMock.Setup(x => x.GetCustomerById(It.IsAny<int>()) ).ReturnsAsync(databaseCustomer);

            _mapperMock.Setup(x => x.Map<CustomerDto>(databaseCustomer)).Returns(outputCustomer);

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

            var user = new
            {
                Id = 1,
                EntityId = 1,
            };

            _mapperMock.Setup(x => x.Map<CustomerDto, Customer>(inputCustomer)).Returns(databaseCustomer);

            _customerRepoMock.Setup(x => x.ExistsAsync(x => x.CNP == It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(true);
            _customerRepoMock.Setup(x => x.AddAsync(databaseCustomer, CancellationToken.None)).ReturnsAsync(databaseCustomer);

            var outputResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{ ""id"": 1, ""entityId"": 1}"),
            };

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(outputResponse)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);

            _sut = new CustomerService(
                _customerRepoMock.Object,
                _reservationRepoMock.Object,
                _mapperMock.Object,
                httpClient
                );

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
            inputCustomer.Name = "AnotherTestName";
            var outputCustomer = GenerateCustomerDtoMockData();

            var inputId = 1;

            _customerRepoMock.Setup(x => x.GetCustomerByIdAsNoTracking(inputId)).ReturnsAsync(databaseCustomer);

            _customerRepoMock.Setup(x => x.ExistsAsync(x => x.Id == inputId, CancellationToken.None)).ReturnsAsync(true);
            _customerRepoMock.Setup(x => x.ExistsAsync(x => x.CNP == inputCustomer.CNP, CancellationToken.None)).ReturnsAsync(false);
            _customerRepoMock.Setup(x => x.ExistsAsync(x => x.CNP == inputCustomer.CNP && x.Id == inputCustomer.Id, CancellationToken.None)).ReturnsAsync(false);

            _mapperMock.Setup(x => x.Map<CustomerDto, Customer>(inputCustomer)).Returns(databaseCustomer);

            _customerRepoMock.Setup(x => x.UpdateAsync(databaseCustomer, CancellationToken.None)).ReturnsAsync(true);

            var outputResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{ ""id"": 1, ""entityId"": 1}"),
            };

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(outputResponse)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);

            _sut = new CustomerService(
                _customerRepoMock.Object,
                _reservationRepoMock.Object,
                _mapperMock.Object,
                httpClient
                );

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

            _customerRepoMock.Setup(x => x.SingleOrDefaultAsync(x => x.Id == inputId, CancellationToken.None)).ReturnsAsync(databaseCustomer);

            _reservationRepoMock.Setup(x => x.ExistsAsync(x => x.CustomerId == inputId, CancellationToken.None)).ReturnsAsync(false);
            _customerRepoMock.Setup(x => x.ExistsAsync(x => x.Id == inputId, CancellationToken.None)).ReturnsAsync(true);


            _customerRepoMock.Setup(x => x.SingleOrDefaultAsync(x => x.Id == inputId, CancellationToken.None)).ReturnsAsync(databaseCustomer);
            _customerRepoMock.Setup(x => x.DeleteAsync(databaseCustomer, CancellationToken.None)).ReturnsAsync(true);


            _mapperMock.Setup(x => x.Map<CustomerDto>(databaseCustomer)).Returns(outputCustomer);

            var outputResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{ ""id"": 1, ""entityId"": 1}"),
            };

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(outputResponse)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);

            _sut = new CustomerService(
                _customerRepoMock.Object,
                _reservationRepoMock.Object,
                _mapperMock.Object,
                httpClient
                );

            //Act
            var actualResult = await _sut.Delete(inputId);

            //Assert
            Assert.Equal(actualResult.Id, inputId);
        }
    }
}
