using AutoMapper;
using Logiware.Application.DTOs;
using Logiware.Application.Exception;
using Logiware.Application.Helpers.Mapper;
using Logiware.Application.Helpers.Profiles;
using Logiware.Application.Interfaces;
using Logiware.Domain.Contracts;

namespace Logiware.Application.Services;

public class ShipmentItemService : IShipmentItemService
{
    private readonly IUnitOfWork _uow;
    private readonly IDefaultMapper _mapper;

    public ShipmentItemService(IUnitOfWork uow, IDefaultMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<ShipmentItemDto> CreateShipmentItem(CreateShipmentItemDto createShipmentItemDto)
    {
        throw new NotImplementedException();
    }

    public Task<ShipmentItemDto> GetShipmentItemById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ShipmentItemDto>> GetAllShipmentItem()
    {
        throw new NotImplementedException();
    }
}