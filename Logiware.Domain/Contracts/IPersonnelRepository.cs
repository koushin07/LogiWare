using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;

namespace Logiware.Domain.Contracts;

public interface IPersonnelRepository : IRepository<Personnel>
{
    Task<List<Personnel>> GetBySiteId(int siteId);

    Task<Personnel?> GetByFullname(string firstName, string lastName);
    Task<List<Personnel>?> GetDriverPersonnel(int siteId);
    Task<List<Personnel>?> GetManagerPersonnel(int siteId);
    Task<Personnel?> GetPersonnelByCode(string code);


}
