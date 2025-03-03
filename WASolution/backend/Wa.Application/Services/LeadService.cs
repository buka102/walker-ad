using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Wa.Application.Entities;
using Wa.Application.Interfaces;

namespace Wa.Application.Services
{
    public class LeadService : ILeadService
    {
        private readonly ILogger<LeadService> _logger;
        private readonly IMapper _mapper;
        private readonly ILeadRepository _leadRepository;
        private readonly INotifierService _notifierService;
        private readonly IValidator<CreateLeadDto> _validator;

        public LeadService(ILogger<LeadService> logger, 
            IMapper mapper, 
            ILeadRepository leadRepository,
            INotifierService notifierService,
            IValidator<CreateLeadDto> validator) =>
            (_logger, _mapper, _leadRepository, _notifierService, _validator) = 
            (logger, mapper, leadRepository, notifierService, validator);
        
        public async Task<List<LeadDto>> GetAllLeadsAsync()
        {
            _logger.LogDebug("GetAllLeadsAsync() was called");
            var leads = await _leadRepository.GetAllAsync();
            return leads;
        }

        public async Task<LeadDto> CreateLeadAsync(CreateLeadDto createLeadDto)
        {
            _logger.LogDebug("CreateLeadAsync() was called");

            var validationResult = await _validator.ValidateAsync(createLeadDto);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for lead creation: {Errors}", validationResult.Errors);
                // todo: return response object with error messages
                throw new ValidationException(validationResult.Errors);
            }
 
            var leadDto = _mapper.Map<LeadDto>(createLeadDto);
            var createdLead = await _leadRepository.AddAsync(leadDto);
            await _notifierService.NotifyLeadCreationAsync(createLeadDto.Email, createLeadDto.Name);
            return createdLead;
        }
    }
}