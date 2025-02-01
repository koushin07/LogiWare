using Logiware.Domain.Models.Entities;

namespace Logiware.Domain.Contracts;

public interface IItemRepository : IRepository<Item>
{
     Task<Item> Upsert(Item item);
     Task<List<Item>> GetItemWithOwner(int siteId);
     
     int CountItem(int id);
     
}