namespace Logiware.Domain.Models.Entities;

public class ItemHistory : BaseEntity
{
    public Ownership Ownership { get; set; }
    public int OwnershipId { get; set; }
    public string Remarks { get; set; }
    public Status Status { get; set; }
}