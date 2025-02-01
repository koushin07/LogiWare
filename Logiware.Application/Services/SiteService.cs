using AutoMapper;
using Logiware.Application.DTOs;
using Logiware.Application.Exception;
using Logiware.Application.Helpers.Mapper;
using Logiware.Application.Helpers.Profiles;
using LogiWare.Contracts;
using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;



namespace Logiware.Application.Services;

public class SiteService : ISiteService
{
    private readonly IUnitOfWork _uow;
    private readonly IDefaultMapper _mapper;

    public SiteService(IUnitOfWork uow, IDefaultMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }


    public async Task<List<SiteDto>> GetAllSite()
    {
        // only admin can do this
        var sites = await _uow.SiteRepository.GetAll();
        return sites.Select(s => _mapper.Map<SiteDto>(s)).ToList();

    }

    public async Task<SiteDto> CreateSite(CreateSiteDto createSiteDto)
    {
        // only admin can do this
        var existSite = await _uow.SiteRepository.GetByName(createSiteDto.Name);

        if (existSite != null) throw new BadRequestException("this site is already taken");
        var site = _mapper.Map<Site>(createSiteDto);
        await _uow.SiteRepository.Insert(site);
        await _uow.Complete();
        return _mapper.Map<SiteDto>(site);
    }

    public async Task<List<SiteDto>> GetAllSiteExcept(int id)
    {
        return _mapper.Map<List<SiteDto>>(await _uow.SiteRepository.GetAllExcept(id));
    }

    public DashboardDto Dashboard(int id)
    {
        return new DashboardDto()
        {
            TotalInbound = _uow.ShipmentRepository.CountInBoundShipment(id),
            TotalOutbound = _uow.ShipmentRepository.CountOutBoundShipment(id),
            
            TotalMissing = _uow.ShipmentReceiveRepository.CountMissingShipment(id),
            TotalReceived = _uow.ShipmentReceiveRepository.CountReceivedShipment(id),

            TotalInventory = _uow.ItemRepository.CountItem(id)
        };
    }

    public async Task<SiteDto?> GetSiteById(int id)
    {
        return _mapper.Map<SiteDto>(await _uow.SiteRepository.GetById(id));
    }
}