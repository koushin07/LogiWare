using Logiware.Application.DTOs;
using Logiware.Domain.Models;

namespace Logiware.Application.Interfaces;

public interface IOwnershipService
{
    Task<OwnershipDto?> GetOwnershipByItemAndSite(int itemId, int siteId);
    Task<OwnershipDto> AssignOwnerToItem(OwnershipDto ownershipDto);
    Task<List<OwnershipDto>> GetOwnershipBySiteId(int siteId);
}