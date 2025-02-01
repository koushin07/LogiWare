using Logiware.Application.DTOs;
using Logiware.Domain.Models.Entities;

namespace Logiware.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}