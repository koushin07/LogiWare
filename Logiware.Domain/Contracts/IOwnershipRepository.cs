using Logiware.Domain.Models.Entities;

namespace Logiware.Domain.Contracts;

public interface IOwnershipRepository : IRepository<Ownership>
{
    Task<Ownership?> GetByItemAndSite(int itemId, int siteId);
    Task<List<Ownership>> GetOwnershipBySiteId(int siteId);
}