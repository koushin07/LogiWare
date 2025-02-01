using System.ComponentModel.DataAnnotations;

namespace Logiware.Application.DTOs;

public class CreatePersonnelDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [AllowedValues(["Driver", "Staff", "Manager"])]
    public string Role { get; set; }

    public int SiteId { get; set; }

}
