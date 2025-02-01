using System.Data;
using Logiware.Domain.Enums;

namespace Logiware.Domain.Models.Entities;

public class Shipment : BaseEntity
{
    public DateTime ShipmentDate { get; set; }
    public int? AuthorizedById { get; set; }
    public string ShipmentCode { get; set; }
    public Personnel AuthorizedBy { get; private set; }
    public int? DriverId { get; set; }
    public Personnel Driver { get; set; }
    public int SiteId { get; set; }
    public Site Site { get; set; }
    public Status Status { get; set; }
    public DateTime StatusUpdate { get; set; }
    public int DestinationSiteId { get; set; }
    public Site DestinationSite { get; set; }
    public List<ShipmentItem> ShipmentItems { get; set; } = new List<ShipmentItem>();

    
    public Shipment()
    {
        CreatedAt = DateTime.Now;
        ShipmentCode = GenerateCode();
    }
    public Shipment(DateTime shipmentDate, Personnel driver, Site site, Site destinationSite, Status status, Personnel authorizedBy)
    {
        ShipmentCode = GenerateCode();
        ShipmentDate = shipmentDate;
        Driver = driver;
        Site = site;
        DestinationSite = destinationSite;
        Status = status;
        CreatedAt = DateTime.Now;
        AuthorizedBy = authorizedBy;
    }


  
   

    public void AddShipmentItem(Ownership ownership, int quantity)
    {

        var shipmentItem = new ShipmentItem(this, ownership, quantity)
        {
            CreatedAt = DateTime.Now
        };
       
        ShipmentItems.Add(shipmentItem);
    }

    private string GenerateCode()
    {
        var randomNumber = new Random().Next(1000, 10000);
        
        // Get the current date in the format yyyyMMdd
        var currentDate = DateTime.Now.ToString("yyyyMMdd");

        // Combine parts to form the CustomId
        return $"SPT-{randomNumber}-{currentDate}";
    }


    public void ChangeStatus(Status status)
    {
        Status = status;
        StatusUpdate = DateTime.Now;
    }


   
    
    
    
}