using AutoMapper;
using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;
using Logiware.Domain.Models.Queries;
using Logiware.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logiware.Infrastructure.Repositories;

public class ShipmentRepository : IShipmentRepository
{
    private readonly MyDbContext _context;

    public ShipmentRepository(MyDbContext context)
    {
        _context = context;

    }
    public async Task<List<Shipment>> GetAll()
    {
        return await _context.Shipments.AsNoTracking().ToListAsync();
    }

    public async Task<Shipment?> GetById(int id)
    {
        return await _context.Shipments
            .Include(s => s.ShipmentItems)
            .ThenInclude(si => si.Ownership)
                .ThenInclude(si => si.Item).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Shipment> Insert(Shipment entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        var shipment = await _context.Shipments.AddAsync(entity);
        return shipment.Entity;
    }

    public Task Update(Shipment entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task Delete(Shipment entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Shipment>?> GetAllShipmentOfSite(int siteId)
    {


        return await _context.Shipments
            .Include(s => s.Driver)
            .Include(s => s.AuthorizedBy)
            .Include(s => s.ShipmentItems)
            .ThenInclude(si => si.Ownership)
            .ThenInclude(si => si.Item)

            .Where(s => s.Site.Id == siteId).ToListAsync();
    }

    public async Task<bool> AuthorizeShipment(string code, int id)
    {
        return await _context.Personnels.AnyAsync(p => p.Code == code && p.Id == id);
    }

    public async Task<Shipment?> GetShipmentByCode(string code)
    {
        return await _context.Shipments
            .Include(s => s.Driver)
            .Include(s => s.AuthorizedBy)
            .Include(s => s.Site)
            .Include(s => s.ShipmentItems).ThenInclude(si => si.Ownership)
                .ThenInclude(si => si.Site)
            .Include(s => s.ShipmentItems).ThenInclude(si => si.Ownership)
                .ThenInclude(si => si.Item)
            .Include(s => s.ShipmentItems)
            .ThenInclude(si => si.ShipmentReceives)
            .FirstOrDefaultAsync(s => s.ShipmentCode == code);
    }

    public async Task<List<Shipment>> GetInBoundShipment(int id)
    {
        return await _context.Shipments
            .Include(s => s.Driver)
            .Include(s => s.AuthorizedBy)
            .Include(s => s.Site)
            .Include(s => s.ShipmentItems)
                .ThenInclude(si => si.Ownership)
            .ThenInclude(o => o.Item)
            .Include(s => s.ShipmentItems)
                .ThenInclude(si => si.ShipmentReceives)
            .Where(s => s.DestinationSiteId == id && s.SiteId != id)
            .ToListAsync();

    }

    public async Task<List<Shipment>> GetOutBoundShipment(int id)
    {
        return await _context.Shipments
            .Include(s => s.Driver)
            .Include(s => s.AuthorizedBy)
            .Include(s => s.DestinationSite)
            .Include(s => s.ShipmentItems)
                .ThenInclude(si => si.Ownership)
                .ThenInclude(si => si.Item)
            .Include(s => s.ShipmentItems)
                .ThenInclude(si => si.ShipmentReceives)
            .Where(s => s.SiteId == id && s.DestinationSiteId != id).ToListAsync();
    }

    public int CountOutBoundShipment(int id)
    {
        return _context.Shipments.Count(s => s.SiteId == id);
    }

    public int CountInBoundShipment(int id)
    {
        return _context.Shipments.Count(s => s.DestinationSiteId == id);
    }


    public List<ShipmentByDate> GetShipmentsGroupedByDate(int siteId)
    {
        // Generate the last 6 months from the current date.
        var lastSixMonths = Enumerable.Range(0, 6)
            .Select(i => DateTime.UtcNow.AddMonths(-i))
            .Select(d => new { Year = d.Year, Month = d.Month })
            .ToList();

        // Get shipment data grouped by month.
        var shipmentData = _context.Shipments
            .Where(s => s.CreatedAt >= DateTime.UtcNow.AddMonths(-6))
            .GroupBy(s => new { s.CreatedAt.Year, s.CreatedAt.Month })
            .Select(g => new
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                TotalInbound = g.Count(s => s.DestinationSiteId == siteId),
                TotalOutbound = g.Count(s => s.SiteId == siteId)
            })
            .ToList();

        // Perform a left join between the last six months and the shipment data.
        var result = lastSixMonths
            .GroupJoin(
                shipmentData,
                month => new { month.Year, month.Month },
                shipment => new { shipment.Year, shipment.Month },
                (month, shipments) => new ShipmentByDate
                {
                    CreatedAt = new DateTime(month.Year, month.Month, 1),
                    TotalInbound = shipments.FirstOrDefault()?.TotalInbound ?? 0,
                    TotalOutbound = shipments.FirstOrDefault()?.TotalOutbound ?? 0
                }
            )
            .OrderBy(x => x.CreatedAt)
            .ToList();
        return result;


    }




}
