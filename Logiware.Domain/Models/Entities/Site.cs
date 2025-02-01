namespace Logiware.Domain.Models.Entities;

public class Site : BaseEntity
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public User User { get; set; }
    public List<Ownership> Ownerships { get; set; }
    public List<Personnel> Personnels { get; set; }
    public List<Shipment>? Shipments { get; set; }

    public List<Shipment>? ShipmentDestination { get; set; }
    
}