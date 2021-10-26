using System.Threading.Tasks;
using Bridge.Commons.System.Exceptions;
using Bridge.Commons.System.Models;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.Out;
using Bridge.Unique.Profile.Communication.Resources;
using Bridge.Unique.Profile.Wrapper.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using RestSharp;

namespace Bridge.Unique.Profile.Wrapper.Filters
{
    /// <summary>
    ///     Autorização do usuário
    /// </summary>
    public class UserAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IBupAuthenticationService _bupAuthenticationService;

        /// <summary>
        ///     User authorize filter
        /// </summary>
        /// <param name="bupAuthenticationService"></param>
        public UserAuthorizeFilter(IBupAuthenticationService bupAuthenticationService)
        {
            _bupAuthenticationService = bupAuthenticationService;
        }

        /// <summary>
        ///     Na autorização
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="AuthenticationException"></exception>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorization = context.HttpContext.Request.Headers["Authorization"];
            var appAuthorization = context.HttpContext.Request.Headers["App-Authorization"];
            var headers = new BaseHeader(authorization, appAuthorization, appAuthorization);

            var task = string.IsNullOrWhiteSpace(appAuthorization)
                ? _bupAuthenticationService.AdminUserAuthenticate(authorization)
                : _bupAuthenticationService.AdminUserAuthenticate(headers);

            task.Wait();
            var user = task.Result.Data;
            if (user is not { Id: > 0 })
                throw new AuthenticationException((int)EError.ACCESS_TOKEN_EXPIRED, Errors.AccessTokenExpired);

            var taskApi = _bupAuthenticationService.AdminClientAuthenticate(headers);
            taskApi.Wait();
            var apiClient = taskApi.Result.Data;
            if (apiClient is not { Id: > 0 })
                throw new AuthenticationException((int)EError.CLIENT_UNAUTHORIZED, Errors.ClientUnauthorized);

            context.HttpContext.Items.Add("userId", user.Id);
            context.HttpContext.Items.Add("profileId", user.ProfileId);
            context.HttpContext.Items.Add("apiClientId", user.ApiClientId);
            context.HttpContext.Items.Add("clientId", user.ClientId);
            context.HttpContext.Items.Add("apiClient", apiClient);
        }
    }
}