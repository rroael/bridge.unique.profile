using System.Net;
using System.Threading.Tasks;
using Bridge.Commons.System.Models.Validations;
using Bridge.Unique.Profile.API.Attributes;
using Bridge.Unique.Profile.API.Models.Requests;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.API.Controllers
{
    /// <summary>
    ///     Usuários - Métodos de administração
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
    public class ClientUserAdminController : Controller
    {
        private readonly IClientUserBusiness _clientUserBusiness;

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="clientUserBusiness">DI de Cliente User</param>
        public ClientUserAdminController(IClientUserBusiness clientUserBusiness)
        {
            _clientUserBusiness = clientUserBusiness;
        }

        /// <summary>
        ///     Cria um novo cliente e usuário admin desse cliente
        /// </summary>
        /// <param name="request">Objeto ClientUserRequest a criar</param>
        [HttpPost]
        public async Task<NoContentResult> Create([FromBody] ClientUserRequest request)
        {
            await _clientUserBusiness.Create(request.MapTo(), request.Client.ApiCodes);
            return NoContent();
        }
    }
}