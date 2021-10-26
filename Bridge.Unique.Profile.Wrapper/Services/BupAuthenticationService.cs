using System.Threading.Tasks;
using Bridge.Commons.System.Models;
using Bridge.Unique.Profile.Communication.Models.Out;
using Bridge.Unique.Profile.Wrapper.Configurations;
using Bridge.Unique.Profile.Wrapper.Contracts;
using RestSharp;
using IRestClient = Bridge.Commons.Rest.IRestClient;
using RestClient = Bridge.Commons.Rest.RestClient;

namespace Bridge.Unique.Profile.Wrapper.Services
{
    /// <summary>
    ///     Serviço de autenticação BUP
    /// </summary>
    public class BupAuthenticationService : IBupAuthenticationService
    {
        private readonly string _appAuthorization;

        /// <summary>
        ///     RestClient
        /// </summary>
        protected readonly IRestClient _restClient;

        #region Constructors

        /// <summary>
        ///     Serviço de autenticação BUP
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="appAuthorization"></param>
        /// <param name="restClient"></param>
        public BupAuthenticationService(string baseUrl, string appAuthorization, IRestClient restClient = null)
        {
            _restClient = restClient ?? new RestClient(baseUrl);
            _appAuthorization = appAuthorization;
        }

        /// <summary>
        ///     Serviço de autenticação BUP
        /// </summary>
        /// <param name="configuration"></param>
        public BupAuthenticationService(BupConfiguration configuration)
        {
            _restClient = new RestClient(configuration.ServiceBaseUrl);
            _appAuthorization = configuration.AppAuthorization;
        }

        /// <summary>
        ///     Serviço de autenticação BUP
        /// </summary>
        /// <param name="baseUrl"></param>
        public BupAuthenticationService(string baseUrl)
        {
            _restClient = new RestClient(baseUrl);
        }

        /// <summary>
        ///     Serviço de autenticação BUP
        /// </summary>
        /// <param name="restClient"></param>
        public BupAuthenticationService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        /// <summary>
        ///     Header
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public BaseHeader GetDefaultHeader(string authorization = null)
        {
            return new BaseHeader(authorization, _appAuthorization, _appAuthorization, "application/json");
        }

        #endregion

        #region Authentication

        /// <summary>
        ///     Autenticar usuário admin
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<AuthUserOut>> AdminUserAuthenticate(BaseHeader headers)
        {
            const string route = "user-admin/authenticate";
            return await _restClient.GetAsync<AuthUserOut>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Autenticar usuário admin
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<AuthUserOut>> AdminUserAuthenticate(string authorization)
        {
            const string route = "user-admin/authenticate";
            return await _restClient.GetAsync<AuthUserOut>(route,
                headers: GetDefaultHeader(authorization).GetHeaders());
        }

        /// <summary>
        ///     Autenticar API admin
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<ApiClientOut>> AdminApiAuthenticate(BaseHeader headers)
        {
            const string route = "api-admin/authenticate";
            return await _restClient.GetAsync<ApiClientOut>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Autenticar API admin
        /// </summary>
        /// <returns></returns>
        public async Task<IRestResponse<ApiClientOut>> AdminApiAuthenticate()
        {
            const string route = "api-admin/authenticate";
            return await _restClient.GetAsync<ApiClientOut>(route, headers: GetDefaultHeader().GetHeaders());
        }

        /// <summary>
        ///     Autenticar cliente admin
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<ApiClientOut>> AdminClientAuthenticate(BaseHeader headers)
        {
            const string route = "client-admin/authenticate";
            return await _restClient.GetAsync<ApiClientOut>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Autenticar cliente admin
        /// </summary>
        /// <returns></returns>
        public async Task<IRestResponse<ApiClientOut>> AdminClientAuthenticate()
        {
            const string route = "client-admin/authenticate";
            return await _restClient.GetAsync<ApiClientOut>(route, headers: GetDefaultHeader().GetHeaders());
        }

        #endregion
    }
}