using Logiware.Domain.Models.Entities;

namespace Logiware.Domain.Contracts;

public interface IShipmentReceiveRepository : IRepository<ShipmentReceive>
{
    Task<ShipmentReceive> ReceivingItem(ShipmentItem shipmentItem);
    int CountMissingShipment(int id);
    int CountReceivedShipment(int id);
}