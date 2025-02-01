using Logiware.Application.DTOs;

namespace Logiware.Application.Interfaces;

public interface IItemService
{
    Task<List<ItemDto>> GetAllItem(int siteId);
    Task<ItemDto> GetById(int id);

    Task<ItemDto> CreateItem(CreateItemDto createItemDto);
    Task UpdateItem(UpdateItemDto item);

}