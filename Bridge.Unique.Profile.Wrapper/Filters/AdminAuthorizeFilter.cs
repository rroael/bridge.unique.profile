using Bridge.Commons.System.Exceptions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Resources;
using Bridge.Unique.Profile.Wrapper.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bridge.Unique.Profile.Wrapper.Filters
{
    /// <summary>
    ///     Autorização do Admin
    /// </summary>
    public class AdminAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IBupAuthenticationService _bupAuthenticationService;

        /// <summary>
        ///     Admin authorize filter
        /// </summary>
        /// <param name="bupAuthenticationService"></param>
        public AdminAuthorizeFilter(IBupAuthenticationService bupAuthenticationService)
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
            var task = _bupAuthenticationService.AdminApiAuthenticate();
            task.Wait();
            var api = task.Result.Data;
            if (api is not { Id: > 0 })
                throw new AuthenticationException((int)EError.API_UNAUTHORIZED, Errors.ApiUnauthorized);

            context.HttpContext.Items.Add("apiClient", api);
            context.HttpContext.Items.Add("apiClientId", api.Id);
            context.HttpContext.Items.Add("clientId", api.Client.Id);
        }
    }
}