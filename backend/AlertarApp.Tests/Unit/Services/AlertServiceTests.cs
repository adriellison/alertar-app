using System;
using System.Threading.Tasks;
using AlertarApp.Application.DTOs.Alert;
using AlertarApp.Application.Services;
using AlertarApp.Domain.Entities;
using AlertarApp.Domain.Interfaces;
using Moq;
using Xunit;

namespace AlertarApp.Tests.Unit.Services
{
    public class AlertServiceTests
    {
        private readonly Mock<IAlertRepository> _mockAlertRepository;
        private readonly AlertService _alertService;

        public AlertServiceTests()
        {
            _mockAlertRepository = new Mock<IAlertRepository>();
            _alertService = new AlertService(_mockAlertRepository.Object);
        }

        [Fact]
        public async Task GetAlertByIdAsync_ExistingId_ReturnsAlertDto()
        {
            // Arrange
            var alertId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var alert = new Alert(userId, "Emergência", "Preciso de ajuda", -23.5505, -46.6333);
            
            _mockAlertRepository.Setup(repo => repo.GetByIdAsync(alertId))
                .ReturnsAsync(alert);

            // Act
            var result = await _alertService.GetAlertByIdAsync(alertId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(alert.Id, result.Id);
            Assert.Equal(alert.Title, result.Title);
            Assert.Equal(alert.Description, result.Description);
            Assert.Equal(alert.Latitude, result.Latitude);
            Assert.Equal(alert.Longitude, result.Longitude);
        }

        [Fact]
        public async Task GetAlertByIdAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            var alertId = Guid.NewGuid();
            
            _mockAlertRepository.Setup(repo => repo.GetByIdAsync(alertId))
                .ReturnsAsync((Alert)null);

            // Act
            var result = await _alertService.GetAlertByIdAsync(alertId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAlertAsync_ValidData_ReturnsCreatedAlert()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var createAlertDto = new CreateAlertDto
            {
                Title = "Emergência",
                Description = "Preciso de ajuda",
                Latitude = -23.5505,
                Longitude = -46.6333
            };

            // Act
            var result = await _alertService.CreateAlertAsync(userId, createAlertDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createAlertDto.Title, result.Title);
            Assert.Equal(createAlertDto.Description, result.Description);
            Assert.Equal(createAlertDto.Latitude, result.Latitude);
            Assert.Equal(createAlertDto.Longitude, result.Longitude);
            Assert.Equal(userId, result.UserId);
            Assert.Equal("Active", result.Status);
            
            _mockAlertRepository.Verify(repo => repo.AddAsync(It.IsAny<Alert>()), Times.Once);
        }
    }
}
