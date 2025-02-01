using AutoMapper;
using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;
using Logiware.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logiware.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _context;

    public UserRepository(MyDbContext context)
    {
        _context = context;
    }
    public async Task<List<User>> GetAll()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> Insert(User entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        var user = await _context.Users.AddAsync(entity);
        return user.Entity;
    }

    public Task Update(User entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _context.Users.Include(u=>u.Site).FirstOrDefaultAsync(u=>u.Email==email);
    }

    public bool IsUserExistByEmail(string email)
    {
       return _context.Users.Any(u => u.Email==email);
    }
}
