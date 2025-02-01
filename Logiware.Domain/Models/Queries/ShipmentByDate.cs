
namespace Logiware.Domain.Models.Queries;
public class ShipmentByDate
{
    public DateTime CreatedAt { get; set; }
    public int TotalInbound { get; set; }
    public int TotalOutbound { get; set; }
}