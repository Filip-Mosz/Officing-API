using System.ComponentModel.DataAnnotations;

namespace Officing_API.DTOs;

public class UpdateWorkspaceDto
{
    [Range(1,10000, ErrorMessage = "DailyRate must be in between 1 and 10000")]
    public decimal? DailyRate { get; set; }
    [Required(ErrorMessage = "IsAvailable is required")]
    public bool IsAvailable { get; set; } = true;
}