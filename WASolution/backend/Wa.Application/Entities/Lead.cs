namespace Wa.Application.Entities;

public class Lead
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public  required string ZipCode { get; set; }
    public required bool ConsentToContact { get; set; }
    public required string Email { get; set; }
}
