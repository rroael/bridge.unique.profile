using Bridge.Unique.Profile.API.Attributes;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.API.Controllers
{
    /// <summary>
    ///     Usuários dos clientes
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [AdminAuthorize]
    public class ClientUserController : BaseController
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="authenticationBusiness"></param>
        public ClientUserController(IAuthenticationBusiness authenticationBusiness) : base(authenticationBusiness)
        {
        }
    }
}