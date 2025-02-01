using Logiware.Application.DTOs;
using Logiware.Application.Interfaces;
using LogiWare.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Logiware.API.Controllers;

public class AuthenticationController : BaseAPIController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticatedDto>> Login(LoginRequestDto loginRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var auth = await _authenticationService.Login(loginRequestDto);
        return Ok(auth);
    }
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _authenticationService.Register(registerDto);
        return Ok();
    }


}
