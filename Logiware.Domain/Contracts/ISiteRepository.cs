using Logiware.Domain.Contracts;
using Logiware.Domain.Models.Entities;

namespace Logiware.Domain.Contracts;

public interface ISiteRepository : IRepository<Site>
{
    Task<Site> GetByName(string name);

    Task<List<Site>> GetAllExcept(int id);
   
}