using Bridge.Commons.System.Models;
using Bridge.Unique.Profile.Communication.Models.Out;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.Wrapper.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     Controller Base do Bup
    /// </summary>
    public class BupControllerBase : ControllerBase
    {
        /// <summary>
        ///     Header Authorization
        /// </summary>
        [FromHeader(Name = "authorization")]
        public string Authorization { get; set; }

        /// <summary>
        ///     Header App-Authorization
        /// </summary>
        [FromHeader(Name = "app-authorization")]
        public string AppAuthorization { get; set; }

        /// <summary>
        ///     Header Content-Type
        /// </summary>
        [FromHeader(Name = "content-type")]
        public string ContentType { get; set; }

        /// <summary>
        ///     Busca no contexto a aplicação validada
        /// </summary>
        protected ApiClientOut GetApiClientFromContext => (ApiClientOut)HttpContext.Items["apiClient"];

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

        /// <summary>
        ///     Busca os headers básicos para o uso do BUP
        /// </summary>
        /// <param name="hasNewAppAuthorization">Se tem uma nova autorização de app (api x client) no header do request</param>
        /// <returns></returns>
        protected BaseHeader GetBaseHeaders(bool hasNewAppAuthorization = true)
        {
            var headers = new BaseHeader(Authorization);

            if (!hasNewAppAuthorization) return headers;

            headers.AppAuthorization = AppAuthorization;
            headers.ApiKey = headers.AppAuthorization;

            return headers;
        }
    }
}