namespace Logiware.Application.DTOs;

public class SiteDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public List<PersonnelDto> Personnels { get; set; }
}