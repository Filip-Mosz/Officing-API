namespace Officing_API.DTOs;

public class WorkspaceDto
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public decimal? DailyRate { get; set; }
    public decimal Deposit { get; set; }
    public bool IsAvailable { get; set; }
}