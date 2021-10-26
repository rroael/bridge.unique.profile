using System.Threading.Tasks;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.System.Models;

namespace Bridge.Unique.Profile.Domain.Business.Contracts
{
    public interface IAuthenticationBusiness
    {
        AuthUser Authenticate(string accessToken, string apiClientToken);
        Task<ApiClient> AuthenticateApi(string apiClientToken);
        Task<ApiClient> AuthenticateClient(string apiClientToken);
        Task<string> RefreshToken(string refreshToken, string apiClientToken);
        Task<string> GenerateApiClientToken(ApiClient apiClient);
        Task<Token> GenerateUserToken(AuthUser user, string apiClientToken);
        Task<(AuthUser, ApiClient)> GetApiClientAndRefreshTokenInfo(string refreshToken, string apiClientToken);
        Task<bool> InvalidateApiClientToken(string apiClientToken);
        Task<bool> InvalidateUserTokens(string apiClientToken);
        Task<bool> InvalidateApiClientKeyOnly(string apiClientToken);
        Task<bool> Logout(Token token);
    }
}