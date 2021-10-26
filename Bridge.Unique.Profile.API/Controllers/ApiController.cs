using System.Net;
using System.Threading.Tasks;
using Bridge.Commons.System.Models.Validations;
using Bridge.Unique.Profile.API.Attributes;
using Bridge.Unique.Profile.API.Models.Results;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.API.Controllers
{
    /// <summary>
    ///     Aplicações
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [AdminAuthorize]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.Forbidden)]
    public class ApiController : BaseController
    {
        private readonly IApiBusiness _apiBusiness;

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="apiBusiness">DI de aplicação</param>
        /// <param name="authenticationBusiness">DI de autenticação</param>
        public ApiController(IApiBusiness apiBusiness, IAuthenticationBusiness authenticationBusiness) : base(
            authenticationBusiness)
        {
            _apiBusiness = apiBusiness;
        }

        /// <summary>
        ///     Autenticar
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///     Realiza uma autenticação.
        ///     **Observação:** É possivel acessa-lo sem possuir uma chave administrativa.
        /// </remarks>
        [ProducesResponseType(typeof(ApiClientResult), (int)HttpStatusCode.OK)]
        [HttpGet("authenticate")]
        public async Task<ApiClientResult> Authenticate()
        {
            var result = await _apiBusiness.Authenticate(AppAuthorization);
            return new ApiClientResult(result);
        }
    }
}