using Logiware.Application.DTOs;
using Logiware.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Logiware.API.Controllers;

public class PersonnelController : BaseAPIController
{
    private readonly IPersonnelService _personnelService;

    public PersonnelController(IPersonnelService personnelService)
    {
        _personnelService = personnelService;
    }
    [HttpGet]
    public async Task<ActionResult<PersonnelDto>> GetAllPersonnel()
    {
        return Ok(await _personnelService.GetAll());
    }
    [HttpPost]
    public async Task<ActionResult<PersonnelDto>> CreatePersonnel(CreatePersonnelDto createPersonnelDto)
    {
        return Ok(await _personnelService.CreatePersonnel(createPersonnelDto));
    }

    [HttpGet("{role}")]
    public async Task<ActionResult<List<PersonnelDto>>> GetDriverPersonnel(string role)
    {
       var personnels = await _personnelService.GetByRole(role);
        return Ok(personnels);
    }

    [HttpGet("authorize/{code}")]
    public async Task<ActionResult<PersonnelDto>> AuthorizingCheckout(string? code)
    {

        if (string.IsNullOrEmpty(code)) return NotFound("code must not be empty");
        var personnel = await _personnelService.GetByCode(code);
        return Ok(personnel);

    }
    [HttpGet("site/get/{siteId}")]
    public async Task<ActionResult<List<PersonnelDto>>> GetAllPersonnelBySite(int siteId)
    {
        var personnel = await _personnelService.GetBySite(siteId);
        return Ok(personnel);
    }




}
