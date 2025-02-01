using Logiware.Application.DTOs;
using Logiware.Application.Interfaces;
using Logiware.Domain.Models.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Logiware.API.Controllers;

public class ShipmentController : BaseSecureAPIController
{
    private readonly IShipmentService _shipmentService;
   
    public ShipmentController(IShipmentService shipmentService)
    {
        _shipmentService = shipmentService;
       
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<ShipmentDto>>> GetAllShipment(int id)
    {

        return Ok(await _shipmentService.GetAllShipmentOfSite(id));
    }
    [HttpGet]
    public async Task<ActionResult<List<ShipmentDto>>> GetShipmentPerStatus([FromQuery] ShipmentDirectionDto direction)
    {
        return Ok(await _shipmentService.GetShipmentByDireciton(direction));
    }


    [HttpPost]
    public async Task<ActionResult> CreateShipment(CreateShipmentDto createShipmentDto)
    {
        await _shipmentService.RegisterShipment(createShipmentDto);
        return Ok();
    }

    [HttpPost("authorize")]
    public async Task<ActionResult> AuthorizingCheckout(AuthorizeCheckoutDto authorizeCheckoutDto)
    {
        await _shipmentService.AuthorizeShipment(authorizeCheckoutDto);
        return Ok();
        
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<ShipmentDto>> GetByCode(string code)
    {
        return Ok(await _shipmentService.GetShpmentByCode(code));
    }


    [HttpGet("in-out-bound/{id}")]
    public ActionResult<ShipmentByDate> GetShpmentGroupByDate(int id)
    {
        return Ok(_shipmentService.GetShipmentGroupByDate(id));
    }

    [HttpPut("cancel/{code}")]
    public async Task<ActionResult> CancelShipment(string code)
    {
        await _shipmentService.CancelShipment(code);
        return Ok();
    }
  

}