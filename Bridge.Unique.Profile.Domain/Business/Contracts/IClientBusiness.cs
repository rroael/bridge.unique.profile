using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.System.Contracts.Filters;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Domain.Business.Contracts
{
    public interface IClientBusiness
    {
        Task<ApiClient> Authenticate(string token);
        Task<PaginatedList<Client>> Filter(IFilterPagination request);
        Task<Client> Get(int id);
        Task<PaginatedList<Client>> List(int apiId, Pagination pagination);
        Task<PaginatedList<Client>> List(Pagination pagination);
        Task<List<Client>> ListAll(int apiId);
        Task<Client> Save(Client client, List<string> apiCodes);
        Task<bool> Delete(int id);
        Task<bool> DeleteClientApi(Client clientRequest, List<string> apiCodes);
        Task<int> GetIdByCode(string code);
        Task<Client> Update(Client request);
        Task<Client> ClientUpdate(Client client);
    }
}