using Logiware.Domain.Models.Entities;

namespace Logiware.Application.DTOs;

public class ShipmentItemDto
{
    public int Id { get; set; }
    public string ShipmentItemCode { get; set; }
    public OwnershipDto Ownership { get; set; }
    public int Quantity { get; set; }
    public List<ShipmentReceive> ShipmentReceives { get; set; }
}