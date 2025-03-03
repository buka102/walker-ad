using AutoMapper;
using Wa.Application.Entities;

[AutoMap(typeof(Lead))]
public class LeadDto
{
    public int Id { get; set; }
    public required string Name { get; init; }
    public required string PhoneNumber { get; init; }
    public required string ZipCode { get; init; }
    public required bool ConsentToContact { get; init; }
    public string Email { get; init; }
}
