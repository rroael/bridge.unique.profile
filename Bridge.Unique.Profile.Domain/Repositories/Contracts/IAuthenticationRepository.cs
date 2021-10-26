using System;
using System.Threading.Tasks;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.System.Models;

namespace Bridge.Unique.Profile.Domain.Repositories.Contracts
{
    public interface IAuthenticationRepository
    {
        Task<AuthUser> Authenticate(string accessTokenAuth, string apiClientAuth);
        Task<ApiClient> AuthenticateApiClient(string apiClientAuth);
        Task<ApiClient> AddAuthenticateApiClient(ApiClient apiClient, string apiClientAuth);
        Task<bool> CacheAccessToken(string accessTokenAuth, AuthUser authUser, string apiClientAuth, TimeSpan ttl);
        Task<bool> CacheApiClientToken(string apiClientAuth, ApiClient apiClient);
        Task<bool> CacheRefreshToken(string refreshTokenAuth, AuthUser authUser, string apiClientAuth, TimeSpan ttl);
        Task<bool> InvalidateApiClientToken(string apiClientAuth);
        Task<bool> InvalidateUserTokens(string apiClientAuth);
        Task<bool> Logout(Token token);
        Task<(AuthUser, ApiClient)> GetApiClientAndRefreshTokenInfo(string refreshTokenAuth, string apiClientAuth);
    }
}