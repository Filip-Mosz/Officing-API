namespace Officing_API.DTOs;

public class CreateWorkspaceDto
{
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string StreetNumber { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public decimal DailyRate { get; set; }
}