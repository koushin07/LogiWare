using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Logiware.Application.DTOs;
using Logiware.Application.Interfaces;
using LogiWare.Contracts;
using Logiware.Domain.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Logiware.Application.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    public TokenService(ITokenSettings tokenSettings)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.TokenKey));
    }
    public string CreateToken(User user)
    {
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
            };



            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}