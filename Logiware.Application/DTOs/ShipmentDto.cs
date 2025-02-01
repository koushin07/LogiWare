using Logiware.Domain;
using Logiware.Domain.Models.Entities;

namespace Logiware.Application.DTOs;

public class ShipmentDto
{
    

    public int Id { get; set; }
    public DateTime ShipmentDate { get; set; }
    public Status Status { get; set; }
    public string ShipmentCode { get; set; }
    public PersonnelDto AuthorizedBy { get; set; } 
    public PersonnelDto Driver { get; set; }
   public DateTime StatusUpdate { get; set; }
    public SiteDto Site { get; set; }
    public SiteDto DestinationSite { get; set; }
    public List<ShipmentItemDto> ShipmentItems { get; set; }
   
    
}