namespace Logiware.Application.DTOs;

public class CreateShipmentReceive
{
    public string ShipmentItemCode { get; set; }
    public int MissingQuantity { get; set; }
    public int ReceiveQuantity { get; set; }
}

