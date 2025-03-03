using Wa.Application.Entities;

namespace Wa.Application.Interfaces
{
    public interface ILeadRepository
    {
        Task<List<LeadDto>> GetAllAsync();

        Task<LeadDto> AddAsync(LeadDto lead);
    }
}
