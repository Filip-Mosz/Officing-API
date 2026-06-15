using System.ComponentModel.DataAnnotations;

namespace Officing_API.DTOs;

public class CreateWorkspaceDto
{
    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = string.Empty;
    [Required(ErrorMessage = "Street is required")]
    public string Street { get; set; } = string.Empty;
    public string StreetNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Post Code is required")]
    public string PostalCode { get; set; } = string.Empty;
    [Range(1,10000, ErrorMessage = "Daily Rate have to be between 1 and 10000")]
    public decimal DailyRate { get; set; }
    
}