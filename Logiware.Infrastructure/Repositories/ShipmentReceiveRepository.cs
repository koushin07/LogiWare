using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;
using Logiware.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logiware.Infrastructure.Repositories;

public class ShipmentReceiveRepository : IShipmentReceiveRepository
{
    private readonly MyDbContext _context;

    public ShipmentReceiveRepository(MyDbContext context)
    {
        _context = context;
    }
    public async Task<List<ShipmentReceive>> GetAll()
    {
        return await _context.ShipmentReceives.ToListAsync();
    }

    public async Task<ShipmentReceive?> GetById(int id)
    {
        return await _context.ShipmentReceives.FindAsync(id);
    }

    public async Task<ShipmentReceive> Insert(ShipmentReceive entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        var shipmentReceive = await _context.ShipmentReceives.AddAsync(entity);
        return shipmentReceive.Entity;
    }

    public Task Update(ShipmentReceive entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(ShipmentReceive entity)
    {
        throw new NotImplementedException();
    }

    public Task<ShipmentReceive> ReceivingItem(ShipmentItem shipmentItem)
    {
        throw new NotImplementedException();
    }

    public int CountMissingShipment(int id)
    {
      return _context.Shipments
    .Where(sh => sh.SiteId == id) // Filter for the specific Shipment
    .SelectMany(sh => sh.ShipmentItems) // Select all related ShipmentItems
    .Where(si => si.ShipmentReceives != null && si.ShipmentReceives.Any()) // Ensure ShipmentReceives is not null and has elements
    .SelectMany(si => si.ShipmentReceives) // Flatten the ShipmentReceives list
    .Sum(sr => sr.QuantityMissing); // Sum the QuantityMissing values

    }

    public int CountReceivedShipment(int id)
    {
        return _context.Shipments
            .Where(sh => sh.SiteId == id) // Filter for the specific Shipment
            .SelectMany(sh => sh.ShipmentItems) // Select all related ShipmentItems
            .Where(si => si.ShipmentReceives != null && si.ShipmentReceives.Any()) // Ensure ShipmentReceive exists
            .SelectMany(si=>si.ShipmentReceives)
            .Sum(si => si.QuantityReceived);
    }
}
