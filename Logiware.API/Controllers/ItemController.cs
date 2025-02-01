using Logiware.Application.DTOs;
using Logiware.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Logiware.API.Controllers;

public class ItemController : BaseSecureAPIController
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetAllItem(int id)
    {
        return Ok(await _itemService.GetAllItem(id));
    }
    
    
    [HttpPost]
    public async Task<ActionResult<ItemDto>> RegisterItem(CreateItemDto createItemDto)
    {
        return Ok(await _itemService.CreateItem(createItemDto));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateItem(UpdateItemDto item)
    {
        await _itemService.UpdateItem(item);
        return Ok();
    }


}