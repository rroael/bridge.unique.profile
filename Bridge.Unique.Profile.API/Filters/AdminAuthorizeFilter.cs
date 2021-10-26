using Bridge.Commons.System.Exceptions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Resources;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bridge.Unique.Profile.API.Filters
{
    /// <summary>
    ///     Filtro de autenticação de api
    /// </summary>
    public class AdminAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IAuthenticationBusiness _authenticationBusiness;

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="authenticationBusiness">DI de autenticação</param>
        public AdminAuthorizeFilter(IAuthenticationBusiness authenticationBusiness)
        {
            _authenticationBusiness = authenticationBusiness;
        }

        /// <summary>
        ///     Regras para autenticação
        /// </summary>
        /// <param name="context">Contexto da autenticação</param>
        /// <exception cref="AuthenticationException"></exception>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var appAuthorization = context.HttpContext.Request.Headers["App-Authorization"];

            var task = _authenticationBusiness.AuthenticateApi(appAuthorization);
            task.Wait();
            var apiClient = task.Result;
            if (apiClient == null || apiClient.Id <= 0)
                throw new AuthenticationException((int)EError.API_UNAUTHORIZED, Errors.ApiUnauthorized);

            context.HttpContext.Items.Add("apiClient", apiClient);
            context.HttpContext.Items.Add("apiClientId", apiClient.Id);
            context.HttpContext.Items.Add("clientId", apiClient.ClientId);
        }
    }
}