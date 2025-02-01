using Logiware.Application.DTOs;
using Logiware.Domain;
using Logiware.Domain.Models.Queries;

namespace Logiware.Application.Interfaces;

public interface IShipmentService
{

    Task<List<ShipmentDto>> GetAllShipmentOfSite(int siteId);
    Task<List<ShipmentDto>> GetAllShipments();
    Task<ShipmentDto> GetShipmentById(int id);
    Task RegisterShipment(CreateShipmentDto createShipmentDto);

    Task<ShipmentDto> UpdateShipmentStatus(int id, Status status);

    Task AuthorizeShipment(AuthorizeCheckoutDto authorizeCheckoutDto);

    Task<ShipmentDto?> GetShpmentByCode(string code);

    Task<List<ShipmentDto>> GetShipmentByDireciton(ShipmentDirectionDto shipmentDirection);

    List<ShipmentByDate> GetShipmentGroupByDate(int id);

    Task CancelShipment(string id);


}