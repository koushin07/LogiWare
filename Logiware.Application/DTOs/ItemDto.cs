namespace Logiware.Application.DTOs;

public class ItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string ItemCode { get; set; }
    public int Quantity { get; set; }
    
    public List<ItemHistoryDto>? ItemHistories { get; set; }
    public List<ShipmentItemDto> ShipmentItems { get; set; }
}