namespace Logiware.Application.DTOs;

public class ItemHistoryDto
{
    public int Id { get; set; }
    public ItemDto Item { get; set; }
    public SiteDto Site { get; set; }  
    public string Remarks { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}