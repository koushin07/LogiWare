
using AutoMapper;
using Logiware.Application.DTOs;
using Logiware.Application.Exception;
using Logiware.Application.Helpers.Mapper;
using Logiware.Application.Interfaces;
using Logiware.Domain;
using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;

namespace Logiware.Application.Services;

public class ShipmentReceiveService : IShipmentReceiveService
{
    private readonly IUnitOfWork _uow;
    private readonly IDefaultMapper _mapper;

    public ShipmentReceiveService(IUnitOfWork uow, IDefaultMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task ReceiveShipmentItem(ReceiveShipmentDto receiveShipmentDto)
    {
        var shipment = await _uow.ShipmentRepository.GetShipmentByCode(receiveShipmentDto.ShipmentCode);

        if (shipment is null) throw new NotFoundException($"{receiveShipmentDto.ShipmentCode} doesnt exist");
        if (receiveShipmentDto.ShipmentReceives.Count < 0)
            throw new BadRequestException("Please Provide item you receive");

        var mySite = await _uow.SiteRepository.GetById(receiveShipmentDto.SiteId);
        if(mySite is null) throw new NotFoundException("Site not found");

        // shipment.Status = receiveShipmentDto.Status;
        shipment.ChangeStatus(receiveShipmentDto.Status);

        foreach (var receive in receiveShipmentDto.ShipmentReceives)
        {

            var shipmentItem = await _uow.ShipmentItemRepository.GetByCode(receive.ShipmentItemCode);
        
            if (shipmentItem is null) throw new NotFoundException($"{receive.ShipmentItemCode}  not found");

            var item = shipmentItem.Ownership.Item;
            
            if (shipmentItem.Quantity < receive.ReceiveQuantity) throw new BadRequestException($"you receive more than the ship quantity of item {shipmentItem.Ownership.Item.Name}");
            
            var shipmentReceive = new ShipmentReceive(receive.ReceiveQuantity, receive.MissingQuantity)
            {
                ShipmentItem = shipmentItem
            };

            var status = receive.MissingQuantity == 0 ? Status.Received : Status.Partial;
            
            var location = status == Status.Partial
                ? $"Some Item Missing from {shipment.Site.Name}"
                : status == Status.Received
                    ? $"All Item Received from {shipment.Site.Name}"
                    : "";
            // shipmentItem.Ownership.AddHistory(location, status);
            shipmentItem.Ownership.AdjustQuantity(-receive.ReceiveQuantity, location, status);

            var ownership = await _uow.OwnershipRepository.GetByItemAndSite(item.Id, mySite.Id);

            if(ownership is null){
                ownership = new Ownership()
                {
                    Quantity = receive.ReceiveQuantity,
                    Site = mySite,
                    Item = item,
                    ItemHistories = shipmentItem.Ownership.ItemHistories,
                    ShipmentItems = shipmentItem.Ownership.ShipmentItems,

                };  
            await _uow.OwnershipRepository.Insert(ownership);

            }
            else
            {
                ownership.Quantity = ownership.Quantity + receive.ReceiveQuantity;
                await _uow.OwnershipRepository.Update(ownership);
            }
            await _uow.ShipmentReceiveRepository.Insert(shipmentReceive);

            
        }

        await _uow.Complete();


    }   
}