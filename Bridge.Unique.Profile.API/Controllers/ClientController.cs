using System.Net;
using System.Threading.Tasks;
using Bridge.Commons.System.Models.Requests;
using Bridge.Commons.System.Models.Results;
using Bridge.Commons.System.Models.Validations;
using Bridge.Unique.Profile.API.Attributes;
using Bridge.Unique.Profile.API.Models.Requests;
using Bridge.Unique.Profile.API.Models.Results;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.API.Controllers
{
    /// <summary>
    ///     Clientes
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [UserAuthorize]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.Forbidden)]
    public class ClientController : BaseController
    {
        private readonly IClientBusiness _clientBusiness;

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="clientBusiness">DI de ClienteBusiness</param>
        /// <param name="authenticationBusiness">DI de autenticação</param>
        public ClientController(IClientBusiness clientBusiness, IAuthenticationBusiness authenticationBusiness) : base(
            authenticationBusiness)
        {
            _clientBusiness = clientBusiness;
        }

        /// <summary>
        ///     Buscar cliente
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///     Busca um cliente, retornando um objeto `ClientResult` contendo o cliente encontrado.
        /// </remarks>
        [ProducesResponseType(typeof(ClientResult), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ClientResult> Get()
        {
            var result = await _clientBusiness.Get(GetClientIdFromContext);
            return new ClientResult(result);
        }

        /// <summary>
        ///     Buscar cliente
        /// </summary>
        /// <remarks>
        ///     Busca um cliente pelo seu identificador, retornando um objeto `ClientResult` com o cliente encontrado.
        /// </remarks>
        /// <param name="id">Identificação do cliente</param>
        /// <returns>Objeto ClientResult criado</returns>
        [ProducesResponseType(typeof(ClientResult), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public async Task<ClientResult> Get(int id)
        {
            var result = await _clientBusiness.Get(id);
            return new ClientResult(result);
        }

        /// <summary>
        ///     Listar os clientes
        /// </summary>
        /// <param name="request">Objeto de paginação com ordenação</param>
        /// <returns>Lista paginada de clientes da Api atual</returns>
        /// <remarks>
        ///     Realiza uma listagem dos clientes da API atual, retornando uma lista paginada do objeto `ClientResult`.
        /// </remarks>
        [ProducesResponseType(typeof(PaginatedList<ClientResult>), (int)HttpStatusCode.OK)]
        [HttpGet("list")]
        public async Task<PaginatedList<ClientResult>> List([FromQuery] PaginationRequest request)
        {
            var result = await _clientBusiness.List(GetApiClientFromContext.ApiId, request.MapTo());

            return result.ConvertTo<ClientResult>();
        }

        /// <summary>
        ///     Atualização
        /// </summary>
        /// <remarks>
        ///     Atualiza um novo cliente a partir das informações recebidas do objeto `ClientUpdateRequest`, retornando
        ///     um objeto `ClientResult` com o cliente atualizado.
        /// </remarks>
        /// <param name="request">Objeto ClientUpdateRequest</param>
        /// <returns>Objeto ClientResult</returns>
        [ProducesResponseType(typeof(ClientResult), (int)HttpStatusCode.OK)]
        [HttpPut]
        public async Task<ClientResult> Update([FromBody] ClientEssentialUpdateRequest request)
        {
            request.Id = GetClientIdFromContext;
            var result = await _clientBusiness.ClientUpdate(request.MapTo());
            return new ClientResult(result);
        }
    }
}