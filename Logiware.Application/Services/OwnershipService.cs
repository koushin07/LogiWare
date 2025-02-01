using AutoMapper;
using Logiware.Application.DTOs;
using Logiware.Application.Helpers.Mapper;
using Logiware.Application.Helpers.Profiles;
using Logiware.Application.Interfaces;
using Logiware.Domain.Contracts;

namespace Logiware.Application.Services;

public class OwnershipService: IOwnershipService
{
    private readonly IUnitOfWork _uow;
    private readonly IDefaultMapper _mapper;

    public OwnershipService(IUnitOfWork uow, IDefaultMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<OwnershipDto?> GetOwnershipByItemAndSite(int itemId, int siteId)
    {
        return _mapper.Map<OwnershipDto>(await _uow.OwnershipRepository.GetByItemAndSite(itemId, siteId));
    }

    public Task<OwnershipDto> AssignOwnerToItem(OwnershipDto ownershipDto)
    {
        throw new NotImplementedException();
    }

    public async Task<List<OwnershipDto>> GetOwnershipBySiteId(int siteId)
    {
        var ownerships = await _uow.OwnershipRepository.GetOwnershipBySiteId(siteId);

        return ownerships.Select(ownerships => _mapper.Map<OwnershipDto>(ownerships)).ToList();
    }
}