using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.System.Models.Results;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Domain.Business.Contracts
{
    public interface IApiBusiness
    {
        Task<ApiClient> Authenticate(string token);
        Task<PaginatedList<Api>> List(int page);
        Task<Api> Get(int id);
        Task<List<Api>> GetByCodes(List<string> codes);
        Task<Api> Get(string applicationToken);
        Task<Api> Save(Api api);
        Task<Api> Delete(int id);
    }
}