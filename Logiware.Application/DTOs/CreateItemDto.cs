namespace Logiware.Application.DTOs;

public class CreateItemDto
{
    public string Name { get; set; }
    public string Description { get; set; }
         
    public string Category { get; set; }
    public int Quantity { get; set; }
    public int SiteId { get; set; }
}