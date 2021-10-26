using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Contracts.Filters;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Domain.Repositories.Contracts
{
    public interface IClientRepository
    {
        Task<PaginatedList<Client>> Filter(IFilterPagination request);
        Task<Client> Get(IIdentifiable<int> identifiable);
        Task<Client> GetByCode(string code);
        Task<PaginatedList<Client>> List(int apiId, Pagination pagination);
        Task<PaginatedList<Client>> List(Pagination pagination);
        Task<List<Client>> ListAll(int apiId);
        Task<Client> Save(Client client);
        Task<Client> ClientUpdate(Client client);
        Task<Client> Delete(IIdentifiable<int> identifiable);
        Task<int> GetIdByCode(string code);
    }
}