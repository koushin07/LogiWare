using AutoMapper;
using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;
using Logiware.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logiware.Infrastructure.Repositories;

public class ItemHistoryRepository : IItemHistoryRepository
{
    private readonly MyDbContext _context;
 

    public ItemHistoryRepository(MyDbContext context)
    {
        _context = context;
     
    }
    public Task<List<ItemHistory>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<ItemHistory?> GetById(int id)
    {
        return await _context.ItemHistories.FindAsync(id);
    }

    public Task<ItemHistory> Insert(ItemHistory entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(ItemHistory entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(ItemHistory entity)
    {
        throw new NotImplementedException();
    }

    public async Task<ItemHistory?> GetHistoryByOwnerId(int ownerId)
    {
        return await _context.ItemHistories.FirstOrDefaultAsync(i=>i.OwnershipId == ownerId);
    }
}