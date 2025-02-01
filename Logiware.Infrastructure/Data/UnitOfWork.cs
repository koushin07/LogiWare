using AutoMapper;
using Logiware.Domain.Contracts;
using Logiware.Infrastructure.Repositories;

namespace Logiware.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyDbContext _context;
    public UnitOfWork(MyDbContext context)
    {
        _context = context;
  
    }

    public IUserRepository UserRepository => new UserRepository(_context);
    public ISiteRepository SiteRepository => new SiteRepository(_context);
    public IPersonnelRepository PersonnelRepository => new PersonnelRepository(_context);
    public IItemRepository ItemRepository => new ItemRepository(_context);
    public IShipmentRepository ShipmentRepository => new ShipmentRepository(_context);
    public IShipmentItemRepository ShipmentItemRepository => new ShipmentItemRepository(_context);
    public IOwnershipRepository OwnershipRepository => new OwnershipRepository(_context);
    public IShipmentReceiveRepository ShipmentReceiveRepository => new ShipmentReceiveRepository(_context);
    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }
} 