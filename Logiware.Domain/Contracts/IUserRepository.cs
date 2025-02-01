
using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;

namespace Logiware.Domain.Contracts;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByUsername(string username);
    Task<User?> GetByEmail(string email);

    bool IsUserExistByEmail(string email);
}