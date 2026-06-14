namespace Officing_API.Models;

public class Workspace
{
    public int Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string StreetNumber { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Deposit { get; set; }
    public bool IsAvailable { get; set; }
    public int OwnerId { get; set; }
}