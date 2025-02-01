namespace Logiware.Application.DTOs;

public class DashboardDto
{
    public int TotalInventory { get; set; }
    public int TotalReceived { get; set; }
    public int TotalOutbound { get; set; }
    public int TotalMissing { get; set; }
    public int TotalInbound { get; set; }
}