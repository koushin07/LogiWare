using AutoMapper;
using Logiware.Domain.Contracts;
using Logiware.Domain.Enums;
using Logiware.Domain.Models.Entities;
using Logiware.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logiware.Infrastructure.Repositories;

public class PersonnelRepository : IPersonnelRepository
{
    private readonly MyDbContext _context;


    public PersonnelRepository(MyDbContext context)
    {
        _context = context;
    }
    public async Task<List<Personnel>> GetAll()
    {
        return await _context.Personnels.ToListAsync();
    }

    public async Task<Personnel?> GetById(int id)
    {
        return await _context.Personnels.FindAsync(id);
    }

    public async Task<Personnel> Insert(Personnel entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        var personnel = await _context.Personnels.AddAsync(entity);
        return personnel.Entity;
    }

    public Task Update(Personnel entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Personnel entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Personnel>> GetBySiteId(int siteId)
    {
        return await _context.Personnels.Where(p => p.SiteId == siteId).ToListAsync();
    }

    public async Task<Personnel?> GetByFullname(string firstName, string lastName)
    {
        return await _context.Personnels.FirstOrDefaultAsync(p => p.LastName == lastName && p.FirstName == firstName);
    }

    public async Task<List<Personnel>?> GetDriverPersonnel(int siteId)
    {
        return await _context.Personnels.AsNoTracking().Where(p => p.Role == Role.Driver && p.SiteId == siteId).ToListAsync();
    }

    public async Task<List<Personnel>?> GetManagerPersonnel(int siteId)
    {
        return await _context.Personnels.AsNoTracking().Where(p => p.Role == Role.Manager && p.SiteId == siteId).ToListAsync();
    }

    public Task<Personnel?> GetPersonnelByCode(string code)
    {
        return _context.Personnels.FirstOrDefaultAsync(p => p.Role == Role.Manager && p.Code == code);
    }
}
