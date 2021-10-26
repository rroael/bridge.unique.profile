using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bridge.Commons.System.Models.Requests;
using Bridge.Commons.System.Models.Results;
using Bridge.Commons.System.Models.Validations;
using Bridge.Unique.Profile.API.Attributes;
using Bridge.Unique.Profile.API.Models.Requests;
using Bridge.Unique.Profile.API.Models.Results;
using Bridge.Unique.Profile.Communication.Models.In.Filters;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.API.Controllers
{
    /// <summary>
    ///     Clientes - Administração
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
    public class ClientAdminController : BaseController
    {
        private readonly IClientBusiness _clientBusiness;

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="clientBusiness">DI de aplicação</param>
        /// <param name="authenticationBusiness">DI de autenticação</param>
        public ClientAdminController(IClientBusiness clientBusiness, IAuthenticationBusiness authenticationBusiness) :
            base(authenticationBusiness)
        {
            _clientBusiness = clientBusiness;
        }

        /// <summary>
        ///     Autenticar
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///     Realiza uma autenticação de cliente.
        ///     **Observação:** Só é possivel acessa-lo possuindo uma chave administrativa.
        /// </remarks>
        [ProducesResponseType(typeof(ApiClientResult), (int)HttpStatusCode.OK)]
        [HttpGet("authenticate")]
        public async Task<ApiClient> Authenticate()
        {
            return await _clientBusiness.Authenticate(AppAuthorization);
        }

        /// <summary>
        ///     Criar novo cliente
        /// </summary>
        /// <remarks>
        ///     Adiciona um novo cliente a partir das informações recebidas do objeto `ClientRequest`, retornando
        ///     um objeto `ClientResult` com o cliente criado.
        /// </remarks>
        /// <param name="request">Objeto ClientRequest a criar</param>
        /// <returns>Objeto ClientResult criado</returns>
        [ProducesResponseType(typeof(ClientResult), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<ClientResult> Create([FromBody] ClientRequest request)
        {
            var result = await _clientBusiness.Save(request.MapTo(), request.ApiCodes);
            return new ClientResult(result);
        }

        /// <summary>
        ///     Filtro de clientes
        /// </summary>
        /// <param name="request">Objeto de paginação com ordenação</param>
        /// <returns>Lista paginada de clientes da Api e Profile escolhidos</returns>
        [HttpPost("filter")]
        public async Task<PaginatedList<ClientResult>> Filter(
            [FromBody] FilterPaginationRequest<ClientFilterIn> request)
        {
            var result = await _clientBusiness.Filter(request.MapTo());
            return result.ConvertTo<ClientResult>();
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
        ///     Remover cliente
        /// </summary>
        /// <remarks>
        ///     Remove um cliente a partir de seu identificador.
        /// </remarks>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>True se o cliente foi removido com sucesso</returns>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _clientBusiness.Delete(id);
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
        [HttpGet("list-by-api")]
        public async Task<PaginatedList<ClientResult>> ListByApi([FromQuery] PaginationRequest request)
        {
            var result = await _clientBusiness.List(GetApiClientFromContext.ApiId, request.MapTo());

            return result.ConvertTo<ClientResult>();
        }

        /// <summary>
        ///     Listar todos os clientes
        /// </summary>
        /// <remarks>
        ///     Realiza a listagem de todos os clientes no repositório, retornando uma lista paginada do objeto `ClientResult`.
        /// </remarks>
        /// <param name="request">Objeto de paginação com ordenação</param>
        /// <returns>Lista paginada de clientes da Api atual</returns>
        [ProducesResponseType(typeof(PaginatedList<ClientResult>), (int)HttpStatusCode.OK)]
        [HttpGet("list")]
        public async Task<PaginatedList<ClientResult>> List([FromQuery] PaginationRequest request)
        {
            var result = await _clientBusiness.List(request.MapTo());

            return result.ConvertTo<ClientResult>();
        }

        /// <summary>
        ///     Listar todos os clientes associados à API
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<ClientResult>), (int)HttpStatusCode.OK)]
        [HttpGet("list-all")]
        public async Task<List<ClientResult>> ListAll()
        {
            var list = await _clientBusiness.ListAll(GetApiClientFromContext.ApiId);
            return list?.Select(s => new ClientResult(s)).ToList();
        }

        /// <summary>
        ///     Busca id do cliente
        /// </summary>
        /// <param name="code">Código da aplicação cliente</param>
        /// <returns>Id do cliente</returns>
        [HttpGet("getIdByCode/{code}")]
        public async Task<int> GetIdByCode(string code)
        {
            return await _clientBusiness.GetIdByCode(code);
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
        public async Task<ClientResult> Update([FromBody] ClientUpdateRequest request)
        {
            var result = await _clientBusiness.Update(request.MapTo());
            return new ClientResult(result);
        }
    }
}