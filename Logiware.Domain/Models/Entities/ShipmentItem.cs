namespace Logiware.Domain.Models.Entities;

public class ShipmentItem : BaseEntity
{
    

    public int ShipmentId { get; set; }
    public Shipment Shipment { get; set; }

    public string ShipmentItemCode { get; set; }
    public Ownership Ownership { get; set; }
    public int Quantity { get; set; }

    public List<ShipmentReceive> ShipmentReceives { get; set; }
    
    public ShipmentItem(Shipment shipment, Ownership ownership, int quantity)
    {
        ShipmentItemCode = GenerateCode();
        CreatedAt = DateTime.Now;
        Shipment = shipment;
        Ownership = ownership;
        Quantity = quantity;
    }

    public ShipmentItem()
    {
        ShipmentItemCode = GenerateCode();
        
        CreatedAt = DateTime.Now;
    }

    private string GenerateCode()
    {
        var randomNumber = new Random().Next(100, 1000);
        // Combine parts to form the CustomId
        return $"SPT-{randomNumber}-ITM";
    }
    
}