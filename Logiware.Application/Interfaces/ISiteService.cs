using Logiware.Application.DTOs;


namespace LogiWare.Contracts;

public interface ISiteService
{
    Task<List<SiteDto>> GetAllSite();
    Task<SiteDto> CreateSite(CreateSiteDto createSiteDto);
    Task<List<SiteDto>> GetAllSiteExcept(int id);

    DashboardDto Dashboard(int id);
    Task<SiteDto?> GetSiteById(int id);


}