using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Bridge.Commons.Extension;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;
using Bridge.Unique.Profile.System.Helpers;
using Bridge.Unique.Profile.System.Models;
using Bridge.Unique.Profile.System.Settings;
using Microsoft.IdentityModel.Tokens;

namespace Bridge.Unique.Profile.Domain.Business
{
    public class AuthenticationBusiness : BaseBusiness, IAuthenticationBusiness
    {
        private readonly IApiClientRepository _apiClientRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthenticationBusiness(IAuthenticationRepository authenticationRepository,
            IApiClientRepository apiClientRepository, AppSettings appSettings)
        {
            _authenticationRepository = authenticationRepository;
            _jwtHelper = new JwtHelper(appSettings.Jwt);
            _apiClientRepository = apiClientRepository;
        }

        public AuthUser Authenticate(string accessToken, string apiClientToken)
        {
            return _jwtHelper.ValidateToken(accessToken);
        }

        public async Task<ApiClient> AuthenticateApi(string apiClientToken)
        {
            ApiClient result = null;

            try
            {
                result = await ValidAuthenticateApiClient(apiClientToken);
            }
            catch
            {
                await InvalidateApiClientKeyOnly(apiClientToken);
                result = await ValidAuthenticateApiClient(apiClientToken);
            }

            if (result != null && result.IsAdmin)
                return result;

            return null;
        }

        public async Task<ApiClient> AuthenticateClient(string apiClientToken)
        {
            ApiClient result;

            try
            {
                result = await ValidAuthenticateApiClient(apiClientToken);
            }
            catch
            {
                await InvalidateApiClientKeyOnly(apiClientToken);
                result = await ValidAuthenticateApiClient(apiClientToken);
            }

            return result;
        }

        public async Task<string> GenerateApiClientToken(ApiClient apiClient)
        {
            var result = TokenHelper.GenerateBupToken();
            var resultAuth = GetApiClientAuth(result);

            //Cache sem dados sensíveis
            apiClient.Token = string.Empty;

            return await Execute(_authenticationRepository.CacheApiClientToken, resultAuth, apiClient) ? result : null;
        }

        public async Task<Token> GenerateUserToken(AuthUser user, string apiClientToken)
        {
            var result = new Token();
            var appAuth = GetApiClientAuth(apiClientToken);
            var apiClient = await AuthenticateApi(apiClientToken);

            result.AccessToken = GenerateAccessToken(user, apiClient.Api.AccessTokenTtl);
            result.RefreshToken = await GenerateRefreshToken(user, appAuth, apiClient.Api.RefreshTokenTtl);

            return result;
        }

        public async Task<(AuthUser, ApiClient)> GetApiClientAndRefreshTokenInfo(string refreshToken,
            string apiClientToken)
        {
            var apiClientAuth = GetApiClientAuth(apiClientToken);
            var refreshTokenAuth = refreshToken.ToHash(HashType);
            return await Execute(_authenticationRepository.GetApiClientAndRefreshTokenInfo, refreshTokenAuth,
                apiClientAuth);
        }

        public async Task<bool> InvalidateApiClientToken(string apiClientToken)
        {
            var appAuth = GetApiClientAuth(apiClientToken);

            //Primeiro invalida todos os usuários
            if (!await Execute(_authenticationRepository.InvalidateUserTokens, appAuth)) return false;

            return await Execute(_authenticationRepository.InvalidateApiClientToken, appAuth);
        }

        public async Task<bool> InvalidateApiClientKeyOnly(string apiClientToken)
        {
            var appAuth = GetApiClientAuth(apiClientToken);

            return await Execute(_authenticationRepository.InvalidateApiClientToken, appAuth);
        }

        public async Task<bool> InvalidateUserTokens(string apiClientToken)
        {
            var appAuth = GetApiClientAuth(apiClientToken);

            return await Execute(_authenticationRepository.InvalidateUserTokens, appAuth);
        }

        public async Task<bool> Logout(Token token)
        {
            //token.AccessToken = token.AccessToken?.ToHash(HashType);
            token.RefreshToken = token.RefreshToken.ToHash(HashType);
            token.ApplicationToken = token.ApplicationToken.ToHash(HashType);

            return await Execute(_authenticationRepository.Logout, token);
        }

        public async Task<string> RefreshToken(string refreshToken, string apiClientToken)
        {
            var refreshTokenAuth = refreshToken.ToHash(HashType);
            var apiClientAuth = apiClientToken.ToHash(HashType);

            var (user, apiClient) =
                await Execute(_authenticationRepository.GetApiClientAndRefreshTokenInfo, refreshTokenAuth,
                    apiClientAuth);

            return GenerateAccessToken(user, apiClient.Api.AccessTokenTtl);
        }

        #region Private

        private string GenerateAccessToken(AuthUser user, TimeSpan ttl)
        {
            var expiration = DateTime.UtcNow.Add(ttl);

            var result = _jwtHelper.GetSecurityToken
            (
                new SecurityTokenDescriptor
                {
                    Expires = expiration,
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("userId", user.Id.ToString()),
                        new Claim("applicationId", user.ApplicationId.ToString()),
                        new Claim("clientId", user.ClientId.ToString()),
                        new Claim("profileId", user.ProfileId.ToString())
                    }),
                    Audience = _jwtHelper.Audience,
                    Issuer = _jwtHelper.Issuer
                }
            );

            return result;
        }

        private static string GetApiClientAuth(string apiClientToken)
        {
            return apiClientToken.ToHash(HashType);
        }

        private static string GetUserRefreshAuth(string refreshToken)
        {
            return refreshToken.ToHash(HashType);
        }

        private async Task<string> GenerateRefreshToken(AuthUser user, string apiClientAuth, TimeSpan ttl)
        {
            var result = TokenHelper.GenerateBupToken();

            var refreshTokenAuth = GetUserRefreshAuth(result);

            return await _authenticationRepository.CacheRefreshToken(refreshTokenAuth, user, apiClientAuth, ttl)
                ? result
                : string.Empty;
        }

        private async Task<ApiClient> ValidAuthenticateApiClient(string apiClientToken)
        {
            var appAuth = GetApiClientAuth(apiClientToken);
            var result = await Execute(_authenticationRepository.AuthenticateApiClient, appAuth);

            if (result != null) return result;

            var apiClient = await _apiClientRepository.Get(appAuth);

            if (apiClient != null)
                return await Execute(_authenticationRepository.AddAuthenticateApiClient, apiClient, appAuth);

            return null;
        }

        #endregion
    }
}