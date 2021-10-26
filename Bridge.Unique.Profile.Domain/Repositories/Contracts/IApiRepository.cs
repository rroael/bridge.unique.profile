using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Domain.Repositories.Contracts
{
    public interface IApiRepository
    {
        Task<Api> Authenticate(string auth);
        Task<PaginatedList<Api>> List(Pagination pagination);
        Task<Api> Get(IIdentifiable<int> identifiable);
        Task<List<Api>> GetByCodes(List<string> codes);
        Task<Api> Get(string auth);
        Task<Api> Save(Api request);
        Task<Api> Delete(IIdentifiable<int> identifiable);
    }
}