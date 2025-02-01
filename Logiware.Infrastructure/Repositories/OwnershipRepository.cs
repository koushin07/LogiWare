using AutoMapper;
using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;
using Logiware.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logiware.Infrastructure.Repositories;

public class OwnershipRepository : IOwnershipRepository
{
    private readonly MyDbContext _context;


    public OwnershipRepository(MyDbContext context)
    {
        _context = context;

    }
    public async Task<List<Ownership>> GetAll()
    {
        return await _context.Ownerships.ToListAsync();
    }

    public async Task<Ownership?> GetById(int id)
    {
        return await _context.Ownerships.FindAsync(id);
    }

    public async Task<Ownership> Insert(Ownership entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        var ownership = await _context.Ownerships.AddAsync(entity);
        return ownership.Entity;
    }

    public Task Update(Ownership entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task Delete(Ownership entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Ownership?> GetByItemAndSite(int itemId, int siteId)
    {
        var owner = await _context.Ownerships
            .Include(o=>o.Item)
             .Include(o=>o.ItemHistories)
            .Include(o=>o.Site)
            .FirstOrDefaultAsync(o => o.SiteId == siteId && o.ItemId == itemId);

        return owner;
    }

    public async Task<List<Ownership>> GetOwnershipBySiteId(int siteId)
    {
        return await _context.Ownerships.Include(o=>o.Item).Where(o=>o.SiteId == siteId).ToListAsync();
    }
}
