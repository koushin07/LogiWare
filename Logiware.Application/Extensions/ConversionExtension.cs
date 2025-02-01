using Logiware.Application.DTOs;
using Logiware.Domain.Models.Entities;

namespace Logiware.Application.Extensions;

public static class ConversionExtension
{
    public static ShipmentDto ToShipmentDto(this Shipment shipment)
    {
        return new ShipmentDto()
        {
            ShipmentDate = shipment.ShipmentDate,
            Driver = new PersonnelDto()
            {
                FirstName = shipment.Driver.FirstName,
                LastName = shipment.Driver.LastName,
                role = shipment.Driver.Role
            },
            AuthorizedBy = new PersonnelDto()
            {
                FirstName = shipment.AuthorizedBy.FirstName,
                LastName = shipment.AuthorizedBy.LastName,
                role = shipment.AuthorizedBy.Role
            },
            Site = new SiteDto()
            {
                
                Description = shipment.Site.Description,
                Location = shipment.Site.Location,
                Name = shipment.Site.Name
            },
            ShipmentItems = shipment.ShipmentItems.Select(s=>new ShipmentItemDto()
            {
                Id = s.Id,
                Ownership = new OwnershipDto()
                {
                    Item = new ItemDto()
                    {
                 
                        Category = s.Ownership.Item.Category,
                        Description = s.Ownership.Item.Description,
                    
                    },
                }
               

            }
            
            ).ToList()
            
        };
    }
}