using System.Threading.Tasks;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Domain.Repositories.Contracts
{
    public interface IApiClientRepository
    {
        Task<ApiClient> Get(string auth);
        Task<ApiClient> Get(string apiCode, int clientId);
        Task<int> GetApiClientId(string apiCode, int clientId);
        Task<ApiClient> Save(ApiClient request);
        Task<ApiClient> GetByUser(string apiCode, string email);
    }
}