namespace Logiware.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string username { get; set; }
    public string Email { get; set; } 
    public SiteDto Site { get; set; }
}