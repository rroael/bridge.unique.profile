using System.Threading.Tasks;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Domain.Repositories.Contracts
{
    public interface IApiGatewayRepository
    {
        Task<ApiGateway> CreateHom(string clientName, string apiName);
        Task<ApiGateway> CreateProd(string clientName, string apiName);
        Task<string> Get(string apiName);
        Task<bool> Delete(string apiKeyId);
    }
}