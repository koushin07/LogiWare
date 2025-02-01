using System.Diagnostics;
using AutoMapper;
using Logiware.Application.DTOs;
using Logiware.Application.Exception;
using Logiware.Application.Extensions;
using Logiware.Application.Helpers.Mapper;
using Logiware.Application.Helpers.Profiles;
using Logiware.Application.Interfaces;
using Logiware.Domain;
using Logiware.Domain.Contracts;
using Logiware.Domain.Enums;
using Logiware.Domain.Models.Entities;
using Logiware.Domain.Models.Queries;

namespace Logiware.Application.Services;

public class ShipmentService : IShipmentService
{
    private readonly IUnitOfWork _uow;
    private readonly IDefaultMapper _mapper;
    private readonly IShipmentMapper _shipmentMapper;

    public ShipmentService(IUnitOfWork uow, IDefaultMapper mapper, IShipmentMapper shipmentMapper)
    {
        _uow = uow;
        _mapper = mapper;
        _shipmentMapper = shipmentMapper;
    }

    public async Task<List<ShipmentDto>> GetAllShipmentOfSite(int siteId)
    {
        var shipments = await _uow.ShipmentRepository.GetAllShipmentOfSite(siteId);


        return shipments.Select(sh => _shipmentMapper.Map<ShipmentDto>(sh)).ToList();
    }

    public async Task<List<ShipmentDto>> GetAllShipments()
    {
        var shipments = await _uow.ShipmentRepository.GetAll();
        return shipments.Select(sh => _mapper.Map<ShipmentDto>(sh)).ToList();
    }

    public async Task<ShipmentDto> GetShipmentById(int id)
    {
        return _mapper.Map<ShipmentDto>(await _uow.ShipmentRepository.GetById(id));
    }

    public async Task RegisterShipment(CreateShipmentDto createShipmentDto)
    {

        var authorize = await _uow.PersonnelRepository.GetById(createShipmentDto.AuthorizedBy);


        var authSite = await _uow.SiteRepository.GetById(createShipmentDto.SiteId);
        if (authSite is null)
            throw new NotFoundException($"{createShipmentDto.SiteId} this Site Id is not found");

        var destinationSite = await _uow.SiteRepository.GetById(createShipmentDto.DestinationSiteId);
        if (destinationSite is null) throw new NotFoundException("Destination Site is not found");

        var driver = await _uow.PersonnelRepository.GetById(createShipmentDto.DriverId);
        if (driver is null) throw new NotFoundException($"{createShipmentDto.DriverId} this personnel id is not found");

        // var newOwner = _uow.OwnershipRepository.Insert(new Ownership(createShipmentDto.Quantity, ownerSite, item));
        DateTime shipmentDate = DateTime.UtcNow;
        shipmentDate.AddDays(createShipmentDto.ShipmentDate.Day);
        shipmentDate.AddHours(createShipmentDto.ShipmentDate.Hour).AddMinutes(createShipmentDto.ShipmentDate.Minute);

        var shipment = new Shipment(shipmentDate, driver, authSite, destinationSite, Status.Shipped, authorize);


        foreach (var shipmentItem in createShipmentDto.ShipmentItem)
        {
            var ownership = await _uow.OwnershipRepository.GetByItemAndSite(shipmentItem.Ownership.Item.Id, shipmentItem.Ownership.Site.Id);
            if (ownership is null) throw new NotFoundException($"{shipmentItem.Ownership} this item id is not found");

            // owner.AdjustQuantity(-createShipmentDto.Quantity, $"Shipped from {ownerSite.Name}");
            shipment.AddShipmentItem(ownership, shipmentItem.Ownership.Quantity);
        }

        await _uow.ShipmentRepository.Insert(shipment);

        if (!await _uow.Complete()) throw new BadRequestException("error in inserting data");

    }

    public async Task<ShipmentDto> UpdateShipmentStatus(int id, Status status)
    {
        var shipment = await _uow.ShipmentRepository.GetById(id);
        if (shipment == null) throw new NotFoundException("Shipment is not found");

        if (shipment.Status != Status.Shipped) throw new BadRequestException("the shipment status is already updated");
        shipment.Status = status;
        shipment.StatusUpdate = DateTime.UtcNow;



        return shipment.ToShipmentDto();

    }

    public async Task AuthorizeShipment(AuthorizeCheckoutDto authorizeCheckoutDto)
    {

        var isAuthorize =
            await _uow.ShipmentRepository.AuthorizeShipment(authorizeCheckoutDto.code, authorizeCheckoutDto.id);
        if (!isAuthorize) throw new BadRequestException("cannot authorize");
    }

    public async Task<ShipmentDto?> GetShpmentByCode(string code)
    {
        var shipments = await _uow.ShipmentRepository.GetShipmentByCode(code);
        var mapped = _shipmentMapper.Map<ShipmentDto>(shipments);
        return mapped;
    }

    public async Task<List<ShipmentDto>> GetShipmentByDireciton(ShipmentDirectionDto shipmentDirection)
    {
        var shipments = shipmentDirection.Adjective switch
        {
            "INBOUND" => await _uow.ShipmentRepository.GetInBoundShipment(shipmentDirection.SiteId),
            "OUTBOUND" => await _uow.ShipmentRepository.GetOutBoundShipment(shipmentDirection.SiteId),
            _ => throw new BadRequestException("Please Ensure Direction Value (INBOUND or OUTBOUND only)")
        };
        return _shipmentMapper.Map<List<ShipmentDto>>(shipments);
    }

    public List<ShipmentByDate> GetShipmentGroupByDate(int id)
    {
        return _uow.ShipmentRepository.GetShipmentsGroupedByDate(id);
    }

    public async Task CancelShipment(string code)
    {
        var shipment = await _uow.ShipmentRepository.GetShipmentByCode(code);
        if (shipment is null) throw new BadRequestException("No shipment found");
        shipment.ChangeStatus(Status.Cancelled);
        await _uow.ShipmentRepository.Update(shipment);


         if (!await _uow.Complete()) throw new BadRequestException("error in inserting data");


    }
}
