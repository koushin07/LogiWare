using Logiware.Application.DTOs;

namespace Logiware.Application.Interfaces;

public interface IShipmentItemService
{
    Task<ShipmentItemDto> CreateShipmentItem(CreateShipmentItemDto createShipmentItemDto);
    Task<ShipmentItemDto> GetShipmentItemById(int id);
    Task<List<ShipmentItemDto>> GetAllShipmentItem();
}