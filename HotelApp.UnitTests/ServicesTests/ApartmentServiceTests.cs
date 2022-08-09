using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Implementations;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;
using Moq;
using Xunit;

namespace HotelApp.UnitTests.ServicesTests
{
    public class ApartmentServiceTests
    {
        private readonly ApartmentService _sut;

        private readonly Mock<IApartmentRepository> _apartmentRepoMock;
        private readonly Mock<IMapper> _mapperMock;

        public ApartmentServiceTests()
        {
            _apartmentRepoMock = new Mock<IApartmentRepository>();
            _mapperMock = new Mock<IMapper>();

            _sut = new ApartmentService(
                _apartmentRepoMock.Object,
                _mapperMock.Object
                );
        }

        [Fact]
        public async Task GetApartment_ShouldReturnApartment()
        {
            //Arrange
            var inputId = 1;

            var databaseApartment = GenerateDatabaseApartmentMockData();
            var outputApartment = GenerateApartmentDtoMockData();

            _apartmentRepoMock.Setup(x => x.GetApartmentById(It.IsAny<int>())).ReturnsAsync(databaseApartment);

            _mapperMock.Setup(x => x.Map<ApartmentDto>(databaseApartment)).Returns(outputApartment);

            //Act
            var actualResult = await _sut.Get(It.IsAny<int>());

            //Assert
            Assert.Equal(actualResult.Id, inputId);
        }

        private Apartment GenerateDatabaseApartmentMockData()
        {
            return new Apartment
            {
                Id = 1,
                DailyRentInEuro = 30,
                NumberOfRooms = 2,
            };
        }
        private ApartmentDto GenerateApartmentDtoMockData()
        {
            return new ApartmentDto
            {
                Id = 1,
                DailyRentInEuro = 30,
                NumberOfRooms = 2,
            };
        }

        [Fact]
        public async Task AddApartment_ShouldReturnApartment()
        {
            //Arrange
            var databaseApartment = GenerateDatabaseApartmentMockData();
            var inputApartment = GenerateApartmentDtoMockData();
            var outputApartment = GenerateApartmentDtoMockData();


            _mapperMock.Setup(x => x.Map<ApartmentDto, Apartment>(inputApartment)).Returns(databaseApartment);

            _apartmentRepoMock.Setup(x => x.ExistsAsync(x => x.ApartmentNumber == inputApartment.ApartmentNumber, CancellationToken.None)).ReturnsAsync(false);
            _apartmentRepoMock.Setup(x => x.ExistsAsync(x => x.HotelId == inputApartment.HotelId, CancellationToken.None)).ReturnsAsync(true);

            _apartmentRepoMock.Setup(x => x.AddAsync(databaseApartment, CancellationToken.None)).ReturnsAsync(databaseApartment);

            _mapperMock.Setup(x => x.Map<ApartmentDto>(databaseApartment)).Returns(outputApartment);

            //Act
            var actualResult = await _sut.Add(inputApartment);

            //Assert
            Assert.Equal(actualResult.Id, inputApartment.Id);
        }

        [Fact]
        public async Task EditApartmentWithSameApartmentNumber_ShouldReturnTrue()
        {
            //Arrange
            var databaseApartment = GenerateDatabaseApartmentMockData();
            var inputApartment = GenerateApartmentDtoMockData();
            var outputApartment = GenerateApartmentDtoMockData();

            var inputId = 1;

            _apartmentRepoMock.Setup(x => x.SingleOrDefaultAsync(x => x.Id == inputApartment.Id, CancellationToken.None)).ReturnsAsync(databaseApartment);

            _apartmentRepoMock.Setup(x => x.UpdateAsync(databaseApartment, CancellationToken.None)).ReturnsAsync(true);

            //Act
            var expectedResult = true;
            var actualResult = await _sut.Edit(inputApartment);

            //Assert
            Assert.Equal(actualResult, expectedResult);
        }

        [Fact]
        public async Task EditApartmentWithDifferentApartmentNumber_ShouldReturnTrue()
        {
            //Arrange
            var databaseApartment = GenerateDatabaseApartmentMockData();
            var inputApartment = GenerateApartmentDtoMockData();
            inputApartment.ApartmentNumber = 2;

            var outputApartment = GenerateApartmentDtoMockData();
            outputApartment.ApartmentNumber = 2;

            _apartmentRepoMock.Setup(x => x.SingleOrDefaultAsync(x => x.Id == inputApartment.Id, CancellationToken.None)).ReturnsAsync(databaseApartment);

            _apartmentRepoMock.Setup(x => x.ExistsAsync(x => x.ApartmentNumber == inputApartment.ApartmentNumber, CancellationToken.None)).ReturnsAsync(false);

            _apartmentRepoMock.Setup(x => x.UpdateAsync(databaseApartment, CancellationToken.None)).ReturnsAsync(true);

            //Act
            var expectedResult = true;
            var actualResult = await _sut.Edit(inputApartment);

            //Assert
            Assert.Equal(actualResult, expectedResult);
        }

        [Fact]
        public async Task DeleteApartment_ShouldReturnApartment()
        {
            //Arrange
            var databaseApartment = GenerateDatabaseApartmentMockData();
            var outputApartment = GenerateApartmentDtoMockData();
            var inputId = 1;

            _apartmentRepoMock.Setup(x => x.SingleOrDefaultAsync(x => x.Id == inputId, CancellationToken.None)).ReturnsAsync(databaseApartment);
            _apartmentRepoMock.Setup(x => x.DeleteAsync(databaseApartment, CancellationToken.None)).ReturnsAsync(true);

            _mapperMock.Setup(x => x.Map<ApartmentDto>(databaseApartment)).Returns(outputApartment);

            //Act
            var actualResult = await _sut.Delete(inputId);

            //Assert
            Assert.Equal(actualResult.Id, inputId);
        }
    }
}
