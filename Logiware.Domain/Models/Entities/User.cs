namespace Logiware.Domain.Models.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }

    public Site? Site { get; set; }
    public int? SiteId { get; set; }

}
