namespace Logiware.Application.DTOs;

public class CreateOwnershipDto
{
    public ItemShipmentDto Item { get; set; }
    public SiteShipmentDto Site { get; set; }
    public int Quantity { get; set; }
   
}