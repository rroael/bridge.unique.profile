using System.Threading.Tasks;
using Bridge.Commons.System.Models;
using Bridge.Unique.Profile.Communication.Models.Out;
using RestSharp;

namespace Bridge.Unique.Profile.Wrapper.Contracts
{
    /// <summary>
    ///     Interface de autenticação de aplicação e usuário
    /// </summary>
    public interface IBupAuthenticationService
    {
        /// <summary>
        ///     Autentica usuário
        /// </summary>
        /// <param name="headers">Headers de acesso</param>
        /// <returns>Objeto AuthUserOut com dados do usuário autenticado</returns>
        Task<IRestResponse<AuthUserOut>> AdminUserAuthenticate(BaseHeader headers);

        /// <summary>
        ///     Autentica usuário
        /// </summary>
        /// <param name="authorization">Access Token do usuário</param>
        /// <returns>Objeto AuthUserOut com dados do usuário autenticado</returns>
        Task<IRestResponse<AuthUserOut>> AdminUserAuthenticate(string authorization);

        /// <summary>
        ///     Autentica a api
        /// </summary>
        /// <param name="headers">Headers de acesso</param>
        /// <returns>Objeto ApplicationOut com a aplicação autenticada</returns>
        Task<IRestResponse<ApiClientOut>> AdminApiAuthenticate(BaseHeader headers);

        /// <summary>
        ///     Autentica a api
        /// </summary>
        /// <returns>Objeto ApplicationOut com a aplicação autenticada</returns>
        Task<IRestResponse<ApiClientOut>> AdminApiAuthenticate();

        /// <summary>
        ///     Autentica o cliente
        /// </summary>
        /// <param name="headers">Headers de acesso</param>
        /// <returns>Objeto ApiClientOut com api e cliente autenticados</returns>
        Task<IRestResponse<ApiClientOut>> AdminClientAuthenticate(BaseHeader headers);

        /// <summary>
        ///     Autentica o cliente
        /// </summary>
        /// <returns>Objeto ApiClientOut com api e cliente autenticados</returns>
        Task<IRestResponse<ApiClientOut>> AdminClientAuthenticate();
    }
}