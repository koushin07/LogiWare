using Logiware.Application.DTOs;
using Logiware.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Logiware.API.Controllers;

public class ShipmentReceiveController: BaseSecureAPIController
{
    private readonly IShipmentReceiveService _shipmentReceiveService;

    public ShipmentReceiveController(IShipmentReceiveService shipmentReceiveService)
    {
        _shipmentReceiveService = shipmentReceiveService;
    }
    [HttpPost("receive-item")]
    public async Task<ActionResult<ShipmentDto>> ReceiveItemShipped(ReceiveShipmentDto shipment)
    {
        await _shipmentReceiveService.ReceiveShipmentItem(shipment);
        Console.Write(shipment);
      
        return Ok(shipment);
    }
}