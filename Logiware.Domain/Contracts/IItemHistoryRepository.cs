using Logiware.Domain.Models.Entities;

namespace Logiware.Domain.Contracts;

public interface IItemHistoryRepository : IRepository<ItemHistory>
{
    Task<ItemHistory?> GetHistoryByOwnerId(int ownerId);
}