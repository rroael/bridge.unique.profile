using System.Net;
using System.Threading.Tasks;
using Bridge.Commons.System.Models;
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
    ///     Endereços
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
    public class AddressController : BaseController
    {
        private readonly IAddressBusiness _addressBusiness;

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="addressBusiness">DI de endereço</param>
        /// <param name="authenticationBusiness">DI de autenticação</param>
        public AddressController(IAddressBusiness addressBusiness, IAuthenticationBusiness authenticationBusiness) :
            base(authenticationBusiness)
        {
            _addressBusiness = addressBusiness;
        }

        /// <summary>
        ///     Listar os endereços
        /// </summary>
        /// <remarks>
        ///     Retorna uma listagem de endereços através de uma lista paginada do objeto `AddressResult`.
        /// </remarks>
        /// <param name="page">Número da página</param>
        /// <returns>Lista paginada</returns>
        [ProducesResponseType(typeof(PaginatedList<AddressResult>), (int)HttpStatusCode.OK)]
        [HttpGet("list/{page}")]
        public async Task<PaginatedList<AddressResult>> ListAddresses(int page)
        {
            var result = await _addressBusiness.ListByUser(GetUserIdFromContext, new Pagination { Page = page });

            return result.ConvertTo<AddressResult>();
        }

        /// <summary>
        ///     Buscar endereço
        /// </summary>
        /// <param name="id">Identificador do endereço</param>
        /// <returns></returns>
        /// <remarks>
        ///     Busca um endereço a partir de um identificador, retornando um objeto `AdressResult` contendo o endereço encontrado.
        /// </remarks>
        [ProducesResponseType(typeof(AddressResult), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public async Task<AddressResult> Get(long id)
        {
            var result = await _addressBusiness.Get(id, GetUserIdFromContext);
            return new AddressResult(result);
        }

        /// <summary>
        ///     Criar endereço
        /// </summary>
        /// <param name="request">Objeto AddressRequest</param>
        /// <returns></returns>
        /// <remarks>
        ///     Cria um endereço a partir das informações do objeto `AdressRequest`, retornando um objeto `AddressResult` com o
        ///     endereço criado.
        /// </remarks>
        [ProducesResponseType(typeof(AddressResult), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<AddressResult> Create([FromBody] AddressRequest request)
        {
            request.UserId = GetUserIdFromContext;
            var result = await _addressBusiness.Save(request.MapTo());
            return new AddressResult(result);
        }

        /// <summary>
        ///     Atualizar endereço
        /// </summary>
        /// <param name="request">Objeto AddressUpdateRequest</param>
        /// <returns></returns>
        /// <remarks>
        ///     Atualiza o endereço a partir de um objeto `AddressUpdateRequest`, retornando um objeto "AdressResult" com as
        ///     informações atualizadas.
        /// </remarks>
        [ProducesResponseType(typeof(AddressResult), (int)HttpStatusCode.OK)]
        [HttpPut]
        public async Task<AddressResult> Update([FromBody] AddressUpdateRequest request)
        {
            request.UserId = GetUserIdFromContext;
            var result = await _addressBusiness.Save(request.MapTo());
            return new AddressResult(result);
        }

        /// <summary>
        ///     Remover endereço
        /// </summary>
        /// <param name="id">Identificador do endereço</param>
        /// <returns></returns>
        /// <remarks>
        ///     Remove um endereço a partir do `id` informado.
        /// </remarks>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpDelete("{id}")]
        public async Task<bool> Delete(long id)
        {
            return await _addressBusiness.Delete(id, GetUserIdFromContext);
        }
    }
}