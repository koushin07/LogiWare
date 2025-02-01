using Logiware.Application.DTOs;
using LogiWare.Contracts.DTOs;

namespace Logiware.Application.Interfaces;

public interface IAuthenticationService
{
    Task<AuthenticatedDto> Login(LoginRequestDto loginRequestDto);
    Task Register(RegisterDto registerDto);

}
