using AutoMapper;
using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;
using Logiware.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logiware.Infrastructure.Repositories;

public class ShipmentItemRepository : IShipmentItemRepository
{
    private readonly MyDbContext _context;

    public ShipmentItemRepository(MyDbContext context)
    {
        _context = context;

    }
    public async Task<List<ShipmentItem>> GetAll()
    {
        return await _context.ShipmentItems.AsNoTracking().ToListAsync();
    }

    public async Task<ShipmentItem?> GetById(int id)
    {
        return await _context.ShipmentItems.FindAsync(id);
    }

    public async Task<ShipmentItem> Insert(ShipmentItem entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        var shipmentItem = await _context.ShipmentItems.AddAsync(entity);
        return shipmentItem.Entity;
    }

    public Task Update(ShipmentItem entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(ShipmentItem entity)
    {
        throw new NotImplementedException();
    }

    public async Task<ShipmentItem?> GetByCode(string code)

    {
        return await _context.ShipmentItems.Include(s=>s.Ownership).ThenInclude(o=>o.Item).FirstOrDefaultAsync(si => si.ShipmentItemCode == code);
    }
}
