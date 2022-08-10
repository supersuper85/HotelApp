using AutoMapper;
using AuditApp.BLL.Dto;
using AuditApp.BLL.Implementations;
using AuditApp.Data.Entities;
using AuditApp.Data.Interfaces;
using Moq;
using Xunit;


namespace AuditApp.UnitTests.ServicesTests
{
    public class AuditServiceTests
    {
        private readonly AuditService _sut;

        private readonly Mock<IAuditRepository> _auditRepoMock;
        private readonly Mock<IMapper> _mapperMock;

        public AuditServiceTests()
        {
            _auditRepoMock = new Mock<IAuditRepository>();
            _mapperMock = new Mock<IMapper>();

            _sut = new AuditService(
                _auditRepoMock.Object,
                _mapperMock.Object
                );
        }

        [Fact]
        public async Task GetAudit_ShouldReturnAudit()
        {
            //Arrange
            var databaseAudit = GenerateAuditMockData();
            var outputAudit = GenerateAuditDtoMockData();

            int inputId = 1;

            _auditRepoMock.Setup(x => x.SingleOrDefaultAsync(x => x.Id == inputId, CancellationToken.None)).ReturnsAsync(databaseAudit);

            _mapperMock.Setup(x => x.Map<AuditDto>(databaseAudit)).Returns(outputAudit);

            //Act
            var actualResult = await _sut.Get(inputId);

            //Assert
            Assert.Equal(actualResult.Id, inputId);
        }

        private Audit GenerateAuditMockData()
        {
            return new Audit
            {
                Id = 1,
                EntityId = 30,
            };
        }
        private AuditDto GenerateAuditDtoMockData()
        {
            return new AuditDto
            {
                Id = 1,
                EntityId = 30,
            };
        }

        [Fact]
        public async Task AddApartment_ShouldReturnApartment()
        {
            //Arrange
            var databaseAudit = GenerateAuditMockData();
            var inputAudit = GenerateAuditDtoMockData();
            var outputApartment = GenerateAuditDtoMockData();


            _mapperMock.Setup(x => x.Map<AuditDto, Audit>(inputAudit)).Returns(databaseAudit);

            _auditRepoMock.Setup(x => x.AddAsync(databaseAudit, CancellationToken.None)).ReturnsAsync(databaseAudit);

            _mapperMock.Setup(x => x.Map<AuditDto>(databaseAudit)).Returns(outputApartment);


            //Act
            var actualResult = await _sut.Add(inputAudit);

            //Assert
            Assert.Equal(actualResult.Id, inputAudit.Id);
        }

        [Fact]
        public async Task EditAudit_ShouldReturnTrue()
        {
            //Arrange
            var databaseAudit = GenerateAuditMockData();
            var inputAudit = GenerateAuditDtoMockData();
            inputAudit.EntityId = 2;

            var mappedInputAudit = new Audit();
            var outputApartment = GenerateAuditDtoMockData();


            _auditRepoMock.Setup(x => x.GetAuditByIdAsNoTracking(It.IsAny<int>())).ReturnsAsync(databaseAudit);

            _mapperMock.Setup(x => x.Map<Audit>(inputAudit)).Returns(mappedInputAudit);

            _auditRepoMock.Setup(x => x.UpdateAsync(mappedInputAudit, CancellationToken.None)).ReturnsAsync(true);

            
            //Act
            var expectedResult = true;
            var actualResult = await _sut.Edit(inputAudit);

            //Assert
            Assert.Equal(actualResult, expectedResult);
        }

        [Fact]
        public async Task DeleteApartment_ShouldReturnApartment()
        {
            //Arrange
            var databaseAudit = GenerateAuditMockData();
            var outputApartment = GenerateAuditDtoMockData();
            var inputId = 1;

            _auditRepoMock.Setup(x => x.SingleOrDefaultAsync(x => x.Id == inputId, CancellationToken.None)).ReturnsAsync(databaseAudit);
            _auditRepoMock.Setup(x => x.DeleteAsync(databaseAudit, CancellationToken.None)).ReturnsAsync(true);

            _mapperMock.Setup(x => x.Map<AuditDto>(databaseAudit)).Returns(outputApartment);


            //Act
            var actualResult = await _sut.Delete(inputId);

            //Assert
            Assert.Equal(actualResult.Id, inputId);
        }
    }
}
