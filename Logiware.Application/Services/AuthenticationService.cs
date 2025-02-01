using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Logiware.Application.DTOs;
using Logiware.Application.Exception;
using Logiware.Application.Helpers.Mapper;
using Logiware.Application.Helpers.Profiles;
using Logiware.Application.Interfaces;
using LogiWare.Contracts.DTOs;
using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;

namespace Logiware.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork _uow;
    private readonly IDefaultMapper _mapper;

    private readonly ITokenService _tokenService;

    public AuthenticationService(IUnitOfWork uow, IDefaultMapper mapper, ITokenService tokenService)
    {
        _uow = uow;
        _mapper = mapper;

        _tokenService = tokenService;
    }



    public async Task<AuthenticatedDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = await _uow.UserRepository.GetByEmail(loginRequestDto.Email);
        if (user == null) throw new UnauthorizeException("Email is not registerd");
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginRequestDto.Password));

        var passwordCheck = user.PasswordHash.SequenceEqual(computedHash);
        if (!passwordCheck) throw new UnauthorizeException("wrong password");
        return new AuthenticatedDto()
        {
            Token = _tokenService.CreateToken(user),
            User = _mapper.Map<UserDto>(user)

        };
    }

    public async Task Register(RegisterDto registerDto)
    {
        var emailExist = await _uow.UserRepository.GetByEmail(registerDto.Email);
        if (emailExist != null) throw new BadRequestException("This email is already taken");

        var site = await _uow.SiteRepository.GetById(registerDto.SiteId);

        if (site == null) throw new BadRequestException("This Site doesnt exist");

        using var hmac = new HMACSHA512();

        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
        var salt = hmac.Key;

        var user = new User
        {
            Username = registerDto.username,
            Email = registerDto.Email,
            PasswordHash = hash,
            PasswordSalt = salt,
            Site = site
        };

        await _uow.UserRepository.Insert(user);

        await _uow.Complete();

    }
}
