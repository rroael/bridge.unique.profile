using Bridge.Unique.Profile.Domain.Business.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.API.Controllers
{
    /// <summary>
    ///     Controller base
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        ///     Instância de autenticação
        /// </summary>
        protected readonly IAuthenticationBusiness _authenticationBusiness;

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="authenticationBusiness">DI de autenticação</param>
        public BaseController(IAuthenticationBusiness authenticationBusiness)
        {
            _authenticationBusiness = authenticationBusiness;
        }

        /// <summary>
        ///     Header Authorization
        /// </summary>
        [FromHeader(Name = "Authorization")]
        public string Authorization { get; set; }

        /// <summary>
        ///     Header App-Authorization
        /// </summary>
        [FromHeader(Name = "App-Authorization")]
        public string AppAuthorization { get; set; }

        /// <summary>
        ///     Header Content-Type
        /// </summary>
        [FromHeader(Name = "Content-Type")]
        public string ContentType { get; set; }

        /// <summary>
        ///     Busca no contexto a aplicação validada
        /// </summary>
        protected ApiClient GetApiClientFromContext
        {
            get
            {
                var apiClient = (ApiClient)HttpContext.Items["apiClient"];
                if (apiClient != null) return apiClient;

                var task = _authenticationBusiness.AuthenticateApi(AppAuthorization);
                task.Wait();
                return task.Result;
            }
        }

        /// <summary>
        ///     Busca no contexto o identificador do usuário validado
        /// </summary>
        protected int GetUserIdFromContext => (int)HttpContext.Items["userId"]!;

        /// <summary>
        ///     Busca no contexto o identificador do perfil (role)
        /// </summary>
        protected int GetProfileIdFromContext => (int)HttpContext.Items["profileId"]!;

        /// <summary>
        ///     Busca no contexto o identificador da aplicação validada
        /// </summary>
        protected int GetApiClientIdFromContext => (int)HttpContext.Items["apiClientId"]!;

        /// <summary>
        ///     Busca no contexto o identificador do cliente validado
        /// </summary>
        protected int GetClientIdFromContext => (int)HttpContext.Items["clientId"]!;
    }
}