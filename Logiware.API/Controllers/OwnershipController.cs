using Logiware.Application.DTOs;
using Logiware.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Logiware.API.Controllers;

public class OwnershipController : BaseSecureAPIController
{
    private readonly IOwnershipService _ownershipService;

    public OwnershipController(IOwnershipService ownershipService)
    {
        _ownershipService = ownershipService;
    }
    [HttpGet("item/{itemId}/site/{siteId}")]
    public async Task<ActionResult<OwnershipDto>> GetByItemAndSiteId(int itemId, int siteId)
    {
        return Ok(await _ownershipService.GetOwnershipByItemAndSite(itemId, siteId));
    }

    [HttpGet("site/{siteId}")]
    public async Task<ActionResult<List<OwnershipDto>>> GetBySiteId(int siteId)
    {
        return Ok(await _ownershipService.GetOwnershipBySiteId(siteId));
    }
}