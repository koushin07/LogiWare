using Logiware.Application.DTOs;

namespace Logiware.Application.Interfaces;

public interface IShipmentReceiveService
{
    Task ReceiveShipmentItem(ReceiveShipmentDto receiveShipmentDto);
}