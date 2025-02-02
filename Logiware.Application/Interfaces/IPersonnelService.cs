using Logiware.Application.DTOs;

namespace Logiware.Application.Interfaces;

public interface IPersonnelService
{
    Task<List<PersonnelDto>> GetAll();
    Task<List<PersonnelDto>> GetBySite(int SiteId);
    Task<PersonnelDto> GetPersonnelById(int id);
    Task<PersonnelDto> CreatePersonnel(CreatePersonnelDto createPersonnelDto);
    Task<List<PersonnelDto>> GetByRole(string role, int siteId);
    Task<PersonnelDto> GetByCode(string code);

}
