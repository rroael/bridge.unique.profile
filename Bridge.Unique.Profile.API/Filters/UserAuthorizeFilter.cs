using Bridge.Commons.System.Exceptions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Resources;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bridge.Unique.Profile.API.Filters
{
    /// <summary>
    ///     Filtro de autenticação de usuário
    /// </summary>
    public class UserAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IAuthenticationBusiness _authenticationBusiness;

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="authenticationBusiness"></param>
        public UserAuthorizeFilter(IAuthenticationBusiness authenticationBusiness)
        {
            _authenticationBusiness = authenticationBusiness;
        }

        /// <summary>
        ///     Regras para autenticação
        /// </summary>
        /// <param name="context">Contexto de autenticação</param>
        /// <exception cref="AuthenticationException"></exception>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorization = context.HttpContext.Request.Headers["Authorization"];
            var appAuthorization = context.HttpContext.Request.Headers["App-Authorization"];

            var user = _authenticationBusiness.Authenticate(authorization, appAuthorization);
            //task.Wait();
            //var user = task.Result;
            if (user == null || user.Id <= 0)
                throw new AuthenticationException((int)EError.ACCESS_TOKEN_EXPIRED, Errors.AccessTokenExpired);

            var task = _authenticationBusiness.AuthenticateApi(appAuthorization);
            task.Wait();
            var api = task.Result;
            if (api == null || api.Id <= 0)
                throw new AuthenticationException((int)EError.API_UNAUTHORIZED, Errors.ApiUnauthorized);

            if (api.Api.Code != "PAP")
                if (api.ClientId != user.ClientId)
                    throw new AuthenticationException((int)EError.API_UNAUTHORIZED, Errors.ApiUnauthorized);

            context.HttpContext.Items.Add("userId", user.Id);
            context.HttpContext.Items.Add("profileId", user.ProfileId);
            context.HttpContext.Items.Add("apiClientId", user.ApplicationId);
            context.HttpContext.Items.Add("clientId", user.ClientId);
            context.HttpContext.Items.Add("apiClient", api);
        }
    }
}