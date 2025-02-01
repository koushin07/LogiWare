using Logiware.Domain.Models.Entities;

namespace Logiware.Domain.Contracts;

public interface IShipmentItemRepository : IRepository<ShipmentItem>
{
    Task<ShipmentItem?> GetByCode(string code);
}