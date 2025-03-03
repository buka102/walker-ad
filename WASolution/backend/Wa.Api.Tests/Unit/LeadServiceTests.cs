using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Wa.Application.Entities;
using Wa.Application.Interfaces;
using Wa.Application.Services;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wa.Application.Tests
{
    public class LeadServiceTests
    {
        private readonly Mock<ILeadRepository> _mockRepository = new();
        private readonly Mock<INotifierService> _mockNotifier = new();
        private readonly Mock<ILogger<LeadService>> _mockLogger = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly Mock<IValidator<CreateLeadDto>> _mockValidator = new();
        private readonly LeadService _leadService;

        public LeadServiceTests()
        {
            _leadService = new LeadService(
                _mockLogger.Object,
                _mockMapper.Object,
                _mockRepository.Object,
                _mockNotifier.Object,
                _mockValidator.Object);
        }

        [Fact]
        public async Task GetAllLeadsAsync_ReturnsMappedLeads()
        {
            // Arrange
            var leads = new List<Lead> { new() { Id = 1, Name = "Test Lead", PhoneNumber = "1234567890", ZipCode = "12345", ConsentToContact = true, Email = "test@example.com" } };
            var leadDtos = new List<LeadDto> { new() { Id = 1, Name = "Test Lead", PhoneNumber = "1234567890", ZipCode = "12345", ConsentToContact = true, Email = "test@example.com" } };
            
            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(leadDtos);

            _mockMapper.Setup(m => m.Map<List<Lead>>(It.IsAny<List<LeadDto>>()))
                .Returns(leads);

            // Act
            var result = await _leadService.GetAllLeadsAsync();
            
            // Assert
            Assert.Single(result);
            Assert.Equal("Test Lead", result[0].Name);
        }

        [Fact]
        public async Task CreateLeadAsync_ValidLead_CreatesAndNotifies()
        {
            // Arrange
            var createLeadDto = new CreateLeadDto { Name = "New Lead", PhoneNumber = "1234567890", ZipCode = "12345", ConsentToContact = true, Email = "new@example.com" };
            var leadDto = new LeadDto { Id = 1, Name = "New Lead", PhoneNumber = "1234567890", ZipCode = "12345", ConsentToContact = true, Email = "new@example.com" };
            
            _mockMapper.Setup(m => m.Map<LeadDto>(createLeadDto)).Returns(leadDto);
            _mockValidator.Setup(v => v.ValidateAsync(createLeadDto, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new ValidationResult());
            _mockRepository.Setup(repo => repo.AddAsync(leadDto)).ReturnsAsync(leadDto);
            
            // Act
            var result = await _leadService.CreateLeadAsync(createLeadDto);
            
            // Assert
            Assert.Equal("New Lead", result.Name);
            _mockNotifier.Verify(n => n.NotifyLeadCreationAsync(leadDto.Email ?? string.Empty, leadDto.Name), Times.Once);
        }

        [Fact]
        public async Task CreateLeadAsync_InvalidLead_ThrowsValidationException()
        {
            // Arrange
            var createLeadDto = new CreateLeadDto { Name = "", PhoneNumber = "", ZipCode = "", ConsentToContact = false, Email = "invalid" };
            var leadDto = new LeadDto { Id = 1, Name = "", PhoneNumber = "", ZipCode = "", ConsentToContact = false, Email = "invalid" };
            
            _mockMapper
                .Setup(m => m.Map<LeadDto>(createLeadDto))
                .Returns(leadDto);
            _mockValidator.Setup(v => v.ValidateAsync(createLeadDto, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new ValidationResult(new List<ValidationFailure> { new("Name", "Name is required") }));
            
            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _leadService.CreateLeadAsync(createLeadDto));
        }
    }
}
