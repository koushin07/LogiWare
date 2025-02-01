using AutoMapper;
using Logiware.Application.DTOs;
using Logiware.Domain.Models.Entities;

namespace Logiware.Application.Helpers.Profiles;

public class DefaultProfile : Profile
{
    public DefaultProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Site, SiteDto>().ReverseMap();
        
        CreateMap<CreateSiteDto, Site>().ReverseMap();
        CreateMap<SiteDto, CreateSiteDto>().ReverseMap();
        CreateMap<Personnel, CreatePersonnelDto>().ReverseMap();
        CreateMap<PersonnelDto, Personnel>().ReverseMap();
        CreateMap<PersonnelDto, CreatePersonnelDto>().ReverseMap();

        CreateMap<CreateItemDto, Item>().ReverseMap();
        CreateMap<Ownership, OwnershipDto>().ReverseMap();

        CreateMap<ItemDto, Item>().ReverseMap()
            .ForMember(dest =>dest.Quantity, opt =>opt.MapFrom(src=>src.Ownerships.Sum(o=>o.Quantity)));
        CreateMap<ItemHistoryDto, ItemHistory>().ReverseMap();
        CreateMap<ShipmentDto, Shipment>()
            .ForMember(dest => dest.Status, opt =>opt.MapFrom(src =>Enum.GetName(src.Status)))
            .ReverseMap();

        CreateMap<ShipmentItemDto, ShipmentItem>()
            .ForMember(dest => dest.Shipment, opt => opt.Ignore()) 
            .ReverseMap();
        CreateMap<CreateShipmentDto, Shipment>()
            
            .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.DriverId))
             .ForMember(dest => dest.ShipmentItems, opt => opt.Ignore()) 
            .ForMember(dest => dest.ShipmentDate, opt => opt.MapFrom(src => DateTime.Now));
    }
}