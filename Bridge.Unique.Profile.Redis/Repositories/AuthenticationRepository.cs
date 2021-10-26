using System;
using System.Threading.Tasks;
using Bridge.Commons.Redis.Contracts;
using Bridge.Commons.System.Exceptions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Resources;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;
using Bridge.Unique.Profile.System.Models;
using Bridge.Unique.Profile.System.Settings;

namespace Bridge.Unique.Profile.Redis.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly string _redisAppAuthKey;
        private readonly IRedisSingle _redisSingle;
        private readonly string _redisUserAuthKey;
        private readonly string _redisUserRefreshAuthKey;

        public AuthenticationRepository(IRedisSingle redisSingle, AppSettings appSettings)
        {
            _redisSingle = redisSingle;

            var keyPrefix = $"{appSettings.Redis.KeyPrefix}:auth";

            _redisAppAuthKey = $"{keyPrefix}:apiclients:{{0}}";
            _redisUserAuthKey = $"{keyPrefix}:users:{{0}}:at:{{1}}";
            _redisUserRefreshAuthKey = $"{keyPrefix}:users:{{0}}:rt:{{1}}";
        }

        public async Task<AuthUser> Authenticate(string accessTokenAuth, string apiClientAuth)
        {
            var key = GetUserAuthKey(accessTokenAuth, apiClientAuth);

            return await _redisSingle.GetAsync<AuthUser>(key);
        }

        public async Task<ApiClient> AuthenticateApiClient(string apiClientAuth)
        {
            var key = GetApiClientAuthKey(apiClientAuth);

            return await _redisSingle.GetAsync<ApiClient>(key);
        }

        public async Task<ApiClient> AddAuthenticateApiClient(ApiClient apiClient, string apiClientAuth)
        {
            var key = GetApiClientAuthKey(apiClientAuth);

            await _redisSingle.SetAsync(key, apiClient);

            apiClient = await _redisSingle.GetAsync<ApiClient>(key);

            return apiClient;
        }

        public async Task<bool> CacheAccessToken(string accessTokenAuth, AuthUser user, string apiClientAuth,
            TimeSpan ttl)
        {
            var key = GetUserAuthKey(accessTokenAuth, apiClientAuth);

            return await _redisSingle.SetAsync(key, user, ttl);
        }

        public async Task<bool> CacheApiClientToken(string apiClientAuth, ApiClient apiClient)
        {
            var key = GetApiClientAuthKey(apiClientAuth);

            return await _redisSingle.SetAsync(key, apiClient);
        }

        public async Task<bool> CacheRefreshToken(string refreshTokenAuth, AuthUser user, string apiClientAuth,
            TimeSpan ttl)
        {
            var key = GetUserRefreshAuthKey(refreshTokenAuth, apiClientAuth);

            return await _redisSingle.SetAsync(key, user, ttl);
        }

        public async Task<bool> InvalidateApiClientToken(string apiClientAuth)
        {
            var key = GetApiClientAuthKey(apiClientAuth);

            return await _redisSingle.RemoveAsync(key);
        }

        public async Task<bool> InvalidateUserTokens(string apiClientAuth)
        {
            var atSearchPattern = string.Format(_redisUserAuthKey, apiClientAuth, string.Empty).Trim();
            atSearchPattern = atSearchPattern.Substring(0, atSearchPattern.Length - 1);

            var rtSearchPattern = string.Format(_redisUserRefreshAuthKey, apiClientAuth, string.Empty).Trim();
            rtSearchPattern = rtSearchPattern.Substring(0, rtSearchPattern.Length - 1);

            var atKeys = _redisSingle.GetKeysBySearch(atSearchPattern);
            var rtKeys = _redisSingle.GetKeysBySearch(rtSearchPattern);

            foreach (string key in atKeys)
                await _redisSingle.RemoveAsync(key);

            foreach (string key in rtKeys)
                await _redisSingle.RemoveAsync(key);

            return true;
        }

        public async Task<bool> Logout(Token token)
        {
            var refreshTokenKey = GetUserRefreshAuthKey(token.RefreshToken, token.ApplicationToken);

            await _redisSingle.RemoveAsync(refreshTokenKey);

            return true;
        }

        public async Task<(AuthUser, ApiClient)> GetApiClientAndRefreshTokenInfo(string refreshTokenAuth,
            string apiClientAuth)
        {
            var key = GetUserRefreshAuthKey(refreshTokenAuth, apiClientAuth);
            var appKey = GetApiClientAuthKey(apiClientAuth);

            if (!await _redisSingle.ExistsAsync(key))
                throw new AuthenticationException((int)EError.REFRESH_TOKEN_EXPIRED, Errors.RefreshTokenExpired);

            var user = await _redisSingle.GetAsync<AuthUser>(key);
            var apiClient = await _redisSingle.GetAsync<ApiClient>(appKey);

            return (user, apiClient);
        }

        #region Private

        private string GetApiClientAuthKey(string apiClientAuth)
        {
            return string.Format(_redisAppAuthKey, apiClientAuth);
        }

        private string GetUserAuthKey(string accessTokenAuth, string apiClientAuth)
        {
            return string.Format(_redisUserAuthKey, apiClientAuth, accessTokenAuth);
        }

        private string GetUserRefreshAuthKey(string refreshTokenAuth, string apiClientAuth)
        {
            return string.Format(_redisUserRefreshAuthKey, apiClientAuth, refreshTokenAuth);
        }

        #endregion
    }
}