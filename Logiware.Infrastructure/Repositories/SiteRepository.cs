using AutoMapper;
using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;
using Logiware.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logiware.Infrastructure.Repositories;

public class SiteRepository : ISiteRepository
{
    private readonly MyDbContext _context;


    public SiteRepository(MyDbContext context)
    {
        _context = context;

    }
    public async Task<List<Site>> GetAll()
    {
        return await _context.Sites.AsNoTracking().ToListAsync();
    }

    public async Task<Site?> GetById(int id)
    {
        return await _context.Sites.FindAsync(id);
    }

    public async Task<Site> Insert(Site entity)
    {
        entity.CreatedAt = DateTime.UtcNow;

         var site = await _context.Sites.AddAsync(entity);
         return site.Entity;
    }

    public Task Update(Site entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Site entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Site> GetByName(string name)
    {
        return await _context.Sites.FirstOrDefaultAsync(s=>s.Name == name);
    }

    public async Task<List<Site>> GetAllExcept(int id)
    {
        return await _context.Sites.Where(s => s.Id != id).ToListAsync();
    }


}
