using Logiware.Domain.Models.Entities;
using Logiware.Domain.Models.Queries;

namespace Logiware.Domain.Contracts;

public interface IShipmentRepository : IRepository<Shipment>
{
    Task<List<Shipment>?> GetAllShipmentOfSite(int siteId);
    Task<bool> AuthorizeShipment(string code, int id);
    Task<Shipment?> GetShipmentByCode(string code);

    Task<List<Shipment>> GetInBoundShipment(int id);
    Task<List<Shipment>> GetOutBoundShipment(int id);
    int CountOutBoundShipment(int id);
    int CountInBoundShipment(int id);
    List<ShipmentByDate> GetShipmentsGroupedByDate(int id);
}