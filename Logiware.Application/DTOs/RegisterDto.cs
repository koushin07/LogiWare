using System.ComponentModel.DataAnnotations;

namespace Logiware.Application.DTOs;

public class RegisterDto
{
    public string username { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    public int SiteId { get; set; }
}