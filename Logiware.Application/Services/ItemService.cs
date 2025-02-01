using Logiware.Application.DTOs;
using Logiware.Application.Exception;
using Logiware.Application.Helpers.Mapper;
using Logiware.Application.Interfaces;
using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;

namespace Logiware.Application.Services;

public class ItemService : IItemService
{
    private readonly IUnitOfWork _uow;
    private readonly IDefaultMapper _mapper;

    public ItemService(IUnitOfWork uow, IDefaultMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<List<ItemDto>> GetAllItem(int siteId)
    {
        var items = await _uow.ItemRepository.GetItemWithOwner(siteId);
        return items.Select(i => _mapper.Map<ItemDto>(i)).ToList();
    }

    public async Task<ItemDto> GetById(int id)
    {
        return _mapper.Map<ItemDto>(await _uow.ItemRepository.GetById(id));
    }

    public async Task<ItemDto> CreateItem(CreateItemDto createItemDto)
    {
        var site = await _uow.SiteRepository.GetById(createItemDto.SiteId);
        if(site == null) throw new NotFoundException("Site not found");
        var item = _mapper.Map<Item>(createItemDto);
        item.AddOwnerShip(site, createItemDto.Quantity);
        var newItem = await _uow.ItemRepository.Insert(item);

        if (!await _uow.Complete()) throw new BadRequestException("Error in inserting Item to database");
        return _mapper.Map<ItemDto>(newItem);
    }

    public async Task UpdateItem(UpdateItemDto item)
    {
        var record = await _uow.ItemRepository.GetById(item.Id);

        if (record == null) throw new NotFoundException("item is not found");

        record.Name = item.Name;
        record.Description = item.Description;
        record.Category = item.Category;

        if (!await _uow.Complete()) throw new BadRequestException("there is error in saving the data");
    }
}
