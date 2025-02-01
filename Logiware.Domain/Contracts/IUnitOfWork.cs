
namespace Logiware.Domain.Contracts;

public interface IUnitOfWork
{
    
    IUserRepository UserRepository { get;  }
    ISiteRepository SiteRepository { get; }
    IPersonnelRepository PersonnelRepository { get; }
    IItemRepository ItemRepository { get; }
    IShipmentRepository ShipmentRepository { get; }
    IShipmentItemRepository ShipmentItemRepository { get; }
    IOwnershipRepository OwnershipRepository { get; }
    
    IShipmentReceiveRepository ShipmentReceiveRepository { get; }
    Task<bool> Complete();
    bool HasChanges();
}