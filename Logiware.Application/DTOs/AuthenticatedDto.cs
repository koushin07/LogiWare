
namespace Logiware.Application.DTOs;

public class AuthenticatedDto
{
    public int Id { get; set; }
    public UserDto User { get; set; }
    public string Token { get; set; }
}