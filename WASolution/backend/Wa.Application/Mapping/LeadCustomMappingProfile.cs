using AutoMapper;

namespace Wa.Application.Mapping;

public class LeadCustomMappingProfile : Profile
{
    public LeadCustomMappingProfile()
    {
        CreateMap<CreateLeadDto, LeadDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id since it's not in CreateLeadDto
    }
}
