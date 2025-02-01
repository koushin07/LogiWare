using System.ComponentModel.DataAnnotations;
using Logiware.Domain.Enums;

namespace Logiware.Application.DTOs;

public class ShipmentDirectionDto
{
    public int SiteId { get; set; }
    [AllowedValues("INBOUND", "OUTBOUND")]
    public string Adjective { get; set; }
}