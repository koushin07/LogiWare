namespace Logiware.Application.DTOs;

public class CreateShipmentDto
{
    public DateTime ShipmentDate { get; set; }
  
    public int DriverId { get; set; }
    public int SiteId { get; set; }
    public int DestinationSiteId { get; set; }
     public int AuthorizedBy { get; set; }
    public List<CreateShipmentItemDto> ShipmentItem { get; set; }
 
}