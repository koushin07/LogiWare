using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Logiware.Domain.Models.Entities;

public class ShipmentReceive : BaseEntity
{
    public int QuantityReceived { get; set; }
    public int QuantityMissing { get; set; }
    public int ShipmentItemId { get; set; }
    
  [JsonIgnore]
    public  ShipmentItem ShipmentItem { get; set; }

    public ShipmentReceive(int quantityReceived, int quantityMissing)
    {
        QuantityReceived = quantityReceived;
        QuantityMissing = quantityMissing;
    }
    
    
}