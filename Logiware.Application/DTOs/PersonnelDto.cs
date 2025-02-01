using Logiware.Domain.Enums;

namespace Logiware.Application.DTOs;

public class PersonnelDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Role role { get; set; }
    public string Code { get; set; }
}
