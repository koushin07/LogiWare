using Logiware.Domain.Models.Entities;

namespace Logiware.Application.DTOs;

public class OwnershipDto
{
    public int Id { get; set; }
    public SiteDto Site { get; set; }
    public ItemDto Item { get; set; }
    public int Quantity { get; set; }
    public List<ItemHistoryDto> ItemHistories { get; set; } 

}