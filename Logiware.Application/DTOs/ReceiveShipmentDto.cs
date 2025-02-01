using Logiware.Domain;

namespace Logiware.Application.DTOs;

public class ReceiveShipmentDto
{
    public PersonnelDto Driver { get; set; }
    public PersonnelDto AuthorizedBy { get; set; }
    public string ShipmentCode { get; set; }
    
    public Status Status { get; set; }
    public int SiteId { get; set; }
    public List<CreateShipmentReceive> ShipmentReceives { get; set; }
    
}