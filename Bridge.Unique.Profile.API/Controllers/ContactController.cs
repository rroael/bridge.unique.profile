using System.Net;
using System.Threading.Tasks;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Unique.Profile.API.Attributes;
using Bridge.Unique.Profile.API.Models.Requests;
using Bridge.Unique.Profile.API.Models.Results;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.API.Controllers
{
    /// <summary>
    ///     Contatos
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [UserAuthorize]
    public class ContactController : BaseController
    {
        private readonly IContactBusiness _contactBusiness;

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="authenticationBusiness">DI de autenticação</param>
        /// <param name="contactBusiness">DI de contato</param>
        public ContactController(IContactBusiness contactBusiness, IAuthenticationBusiness authenticationBusiness) :
            base(authenticationBusiness)
        {
            _contactBusiness = contactBusiness;
        }

        /// <summary>
        ///     Listar os contatos
        /// </summary>
        /// <remarks>
        ///     Retorna uma listagem de contatos através de uma lista paginada do objeto `ContactResult`.
        /// </remarks>
        /// <param name="page">Número da página</param>
        /// <returns>Lista paginada</returns>
        [ProducesResponseType(typeof(PaginatedList<ContactResult>), (int)HttpStatusCode.OK)]
        [HttpGet("list/{page}")]
        public async Task<PaginatedList<ContactResult>> List(int page)
        {
            var result = await _contactBusiness.ListByClient(GetClientIdFromContext, new Pagination { Page = page });

            return result.ConvertTo<ContactResult>();
        }

        /// <summary>
        ///     Buscar contato
        /// </summary>
        /// <param name="id">Identificador do contato</param>
        /// <returns></returns>
        /// <remarks>
        ///     Busca um contato a partir de um identificador, retornando um objeto `ContactResult` contendo o contato encontrado.
        /// </remarks>
        [ProducesResponseType(typeof(ContactResult), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public async Task<ContactResult> Get(long id)
        {
            var result = await _contactBusiness.Get(id);
            return new ContactResult(result);
        }

        /// <summary>
        ///     Criar contato
        /// </summary>
        /// <param name="request">Objeto ContactRequest</param>
        /// <returns></returns>
        /// <remarks>
        ///     Cria um contato a partir das informações do objeto `ContactRequest`, retornando um objeto `ContactResult` com o
        ///     contato criado.
        /// </remarks>
        [ProducesResponseType(typeof(ContactResult), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<ContactResult> Create([FromBody] ContactRequest request)
        {
            request.ClientId = GetClientIdFromContext;
            var result = await _contactBusiness.Save(request.MapTo());
            return new ContactResult(result);
        }

        /// <summary>
        ///     Atualizar contato
        /// </summary>
        /// <param name="request">Objeto ContactRequest</param>
        /// <returns></returns>
        /// <remarks>
        ///     Atualiza o contato a partir de um objeto `ContactRequest`, retornando um objeto "ContactResult" com as
        ///     informações atualizadas.
        /// </remarks>
        [ProducesResponseType(typeof(ContactResult), (int)HttpStatusCode.OK)]
        [HttpPut]
        public async Task<ContactResult> Update([FromBody] ContactRequest request)
        {
            request.ClientId = GetClientIdFromContext;
            var result = await _contactBusiness.Save(request.MapTo());
            return new ContactResult(result);
        }

        /// <summary>
        ///     Remover contato
        /// </summary>
        /// <param name="id">Identificador do contato</param>
        /// <returns></returns>
        /// <remarks>
        ///     Remove um contato a partir do identificador informado.
        /// </remarks>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpDelete("{id}")]
        public async Task<bool> Delete(long id)
        {
            return await _contactBusiness.Delete(id, GetClientIdFromContext);
        }
    }
}