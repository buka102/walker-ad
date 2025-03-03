using AutoMapper;
using FluentValidation;

[AutoMap(typeof(LeadDto), ReverseMap = true)]
public class CreateLeadDto
{
    public required string Name { get; init; } 
    
    public required string PhoneNumber { get; init; } 
    
    public required string ZipCode { get; init; }
    
    public required bool ConsentToContact { get; init; }
    
    public string? Email { get; init; }
}

public class CreateLeadDtoValidator : AbstractValidator<CreateLeadDto>
{
    public CreateLeadDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Lead name is required.");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");
        RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Zip code is required.");
    }
}