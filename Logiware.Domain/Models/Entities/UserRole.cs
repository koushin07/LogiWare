using System.ComponentModel.DataAnnotations;

namespace Logiware.Domain.Models.Entities;

public class UserRole : BaseEntity
{
    [AllowedValues("personnel", "site")]
    public string Type { get; set; }

    public string role { get; set; }
}