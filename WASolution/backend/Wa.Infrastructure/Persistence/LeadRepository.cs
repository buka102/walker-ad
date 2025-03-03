using Wa.Application.Entities;
using Wa.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class LeadRepository : ILeadRepository
{
    private static readonly List<LeadDto> _leads = new()
    {
        new LeadDto { Id = 1, Name = "John Doe", Email = "john.doe@example.com", PhoneNumber = "123-456-7890", ZipCode = "90001", ConsentToContact = true},
        new LeadDto { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", PhoneNumber = "987-654-3210", ZipCode = "90002" , ConsentToContact = true},
        new LeadDto { Id = 3, Name = "Alice Johnson", Email = "alice.johnson@example.com", PhoneNumber = "555-123-4567", ZipCode = "90003" , ConsentToContact = true}
    };
    private static int _idCounter = 4;
    private readonly ILogger<LeadRepository> _logger;

    public LeadRepository(ILogger<LeadRepository> logger)
    {
        _logger = logger;
    }

    public async Task<List<LeadDto>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all leads");
        return await Task.FromResult(_leads);
    }

    public async Task<LeadDto> AddAsync(LeadDto lead)
    {
        lead.Id = _idCounter++;
        _leads.Add(lead);
        _logger.LogInformation($"Lead added: {lead.Name} ({lead.Email})");
        return await Task.FromResult(lead);
    }
}