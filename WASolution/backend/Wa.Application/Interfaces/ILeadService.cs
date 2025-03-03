using System.Collections.Generic;
using Wa.Application.Entities;

namespace Wa.Application.Interfaces
{
    public interface ILeadService
    {
        Task<List<LeadDto>> GetAllLeadsAsync();
        Task<LeadDto> CreateLeadAsync(CreateLeadDto createLead);
    }
}