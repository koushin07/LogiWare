using Logiware.Application.DTOs;
using LogiWare.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Logiware.API.Controllers;


public class SiteController : BaseSecureAPIController
{
    private readonly ISiteService _siteService;

    public SiteController(ISiteService siteService)
    {
        _siteService = siteService;
    }

    [HttpGet("dashboard/{siteId}")]
    public ActionResult<DashboardDto> Dashboard(int siteId)
    {
        return Ok(_siteService.Dashboard(siteId));
    }

    [HttpGet]
    public async Task<ActionResult<List<SiteDto>>> GetAllSite()
    {
        return Ok(await _siteService.GetAllSite());
    }
    [HttpPost("create")]
    public async Task<ActionResult<SiteDto>> CreateSite(CreateSiteDto createSiteDto)
    {
        return Ok(await _siteService.CreateSite(createSiteDto));
    }

    [HttpGet("except/{id}")]
    public async Task<ActionResult<List<SiteDto>>> GetAllSiteExcept(int id)
    {
        return Ok(await _siteService.GetAllSiteExcept(id));
    }







}
