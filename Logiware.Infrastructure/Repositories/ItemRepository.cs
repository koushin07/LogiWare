using AutoMapper;
using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;
using Logiware.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Logiware.Infrastructure.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly MyDbContext _context;

    public ItemRepository(MyDbContext context)
    {
        _context = context;

    }
    public async Task<List<Item>> GetAll()
    {
        return await _context.Items.ToListAsync();
    }

    public async Task<Item?> GetById(int id)
    {
     return await _context.Items.Include(i=>i.Ownerships).FirstOrDefaultAsync(i=>i.Id == id);

    }

    public async Task<Item> Insert(Item entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        var item = await _context.Items.AddAsync(entity);
        return item.Entity;
    }

    public Task Update(Item entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Item entity)
    {
        throw new NotImplementedException();
    }



    public async Task<Item> Upsert(Item item)
    {
        var existingItem = await _context.Items.FindAsync(item.Id);

        if (item == null)
        {
            await _context.Items.AddAsync(item);
            return item;
        }
        else
        {
            _context.Entry(existingItem).CurrentValues.SetValues(item);
            return existingItem;
        }
    }

    public async Task<List<Item>> GetItemWithOwner( int siteId)
    {
        var result = await _context.Items.Include(i=>i.Ownerships)
            .Where(i=>i.Ownerships.Any(o=>o.SiteId == siteId)).ToListAsync();

        return result;
    }

    public int CountItem(int id)
    {
        return _context.Ownerships.Count(o => o.SiteId == id);
    }
}
