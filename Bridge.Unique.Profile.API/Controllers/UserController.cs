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
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.API.Controllers
{
    /// <summary>
    ///     Usuários
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
    public class UserController : BaseController
    {
        private readonly IUserBusiness _userBusiness;

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="userBusiness">DI de usuário</param>
        /// <param name="authenticationBusiness">DI de autenticação</param>
        public UserController(IUserBusiness userBusiness, IAuthenticationBusiness authenticationBusiness) : base(
            authenticationBusiness)
        {
            _userBusiness = userBusiness;
        }

        /// <summary>
        ///     Aprovar/Reprova o acesso do usuário para os ApiClients que possuem a flag NeedsExternalApproval
        /// </summary>
        /// <remarks>
        ///     Aprova ou reprova um usuário do ApiClient usando o `UserActiveAdminRequest`.
        /// </remarks>
        /// <param name="request">Objeto UserAdminActionRequest</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpPatch("approve")]
        public async Task<NoContentResult> Approve([FromBody] UserActionRequest request)
        {
            request.ApiClientId = GetApiClientIdFromContext;
            await _userBusiness.AdminUserApproval(request.MapTo());
            return NoContent();
        }

        /// <summary>
        ///     Aprovar/Reprova o acesso do usuário para os ApiClients que possuem a flag NeedsExternalApproval
        /// </summary>
        /// <remarks>
        ///     Aprova ou reprova um usuário do ApiClient usando o `UserActiveAdminRequest`.
        /// </remarks>
        /// <param name="apiCode">Código da API</param>
        /// <param name="request">Objeto UserAdminActionRequest</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpPatch("approve-in-app/{apiCode}")]
        public async Task<NoContentResult> ApproveInApp(string apiCode, [FromBody] UserActionRequest request)
        {
            request.ApiCode = apiCode;
            request.ClientId = GetClientIdFromContext;
            await _userBusiness.AdminUserApproval(request.MapTo());
            return NoContent();
        }

        /// <summary>
        ///     Aceitar os termos de uso e política de privacidade
        /// </summary>
        /// <remarks>
        ///     Aceita os Termos de Uso e Política de privacidade.
        /// </remarks>
        /// <returns></returns>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpPatch("terms/accept")]
        public async Task<bool> AcceptTerms()
        {
            var result = await _userBusiness.AcceptTerms(GetUserIdFromContext, GetApiClientIdFromContext);

            return result;
        }

        /// <summary>
        ///     Buscar usuário
        /// </summary>
        /// <remarks>
        ///     Busca um usuário retornando um objeto `UserResult` contendo o usuário encontrado.
        /// </remarks>
        /// <returns>Objeto de UserResult encontrado</returns>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<UserResult> Get()
        {
            var result = await _userBusiness.Get(GetUserIdFromContext, GetApiClientIdFromContext);

            return new UserResult(result);
        }

        /// <summary>
        ///     Buscar usuário
        /// </summary>
        /// <remarks>
        ///     Busca um usuário retornando um objeto `UserResult` contendo o usuário encontrado.
        /// </remarks>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Objeto de UserResult encontrado</returns>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpGet("get-by-id/{id}")]
        public async Task<UserResult> GetById(int id)
        {
            var result = await _userBusiness.Get(id, GetApiClientIdFromContext);

            return new UserResult(result);
        }

        /// <summary>
        ///     Buscar usuário
        /// </summary>
        /// <remarks>
        ///     Busca um usuário retornando um objeto `UserResult` contendo o usuário encontrado.
        /// </remarks>
        /// <param name="apiCode">Código da API</param>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Objeto de UserResult encontrado</returns>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpGet("get-in-app/{apiCode}/{id}")]
        public async Task<UserResult> GetInApp(string apiCode, int id)
        {
            var result = await _userBusiness.Get(id, apiCode, GetClientIdFromContext);

            return new UserResult(result);
        }

        /// <summary>
        ///     Criar novo usuário
        /// </summary>
        /// <param name="request">Objeto UserRequest a criar</param>
        /// <returns>Objeto UserResult criado</returns>
        /// <remarks>
        ///     Cria um novo usuário a partir das informações do objeto `UserRequest`, retornando um objeto
        ///     `UserResult`.
        /// </remarks>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<UserResult> Create([FromBody] UserRequest request)
        {
            var apiClientId = GetApiClientIdFromContext;
            request.ApiClientId = apiClientId;
            var result = await _userBusiness.Save(request.MapTo(), apiClientId);
            return new UserResult(result);
        }

        /// <summary>
        ///     Criar novo usuário
        /// </summary>
        /// <param name="apiCode"></param>
        /// <param name="request">Objeto UserRequest a criar</param>
        /// <returns>Objeto UserResult criado</returns>
        /// <remarks>
        ///     Cria um novo usuário a partir das informações do objeto `UserRequest`, retornando um objeto
        ///     `UserResult`.
        /// </remarks>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpPost("create-in-app/{apiCode}")]
        public async Task<UserResult> CreateInApp(string apiCode, [FromBody] UserRequest request)
        {
            var result = await _userBusiness.Save(request.MapTo(), apiCode, GetClientIdFromContext,
                GetApiClientFromContext);
            return new UserResult(result);
        }

        /// <summary>
        ///     Atualizar senha
        /// </summary>
        /// <remarks>
        ///     Atualiza a senha do usuário a partir das informações do objeto `UpdatePasswordRequest`.
        /// </remarks>
        /// <param name="request">Objeto UpdatePasswordRequest</param>
        /// <returns>True se realizou a operação com sucesso</returns>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpPatch("password/update")]
        public async Task<bool> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            request.Id = GetUserIdFromContext;
            request.ApplicationId = GetApiClientIdFromContext;
            return await _userBusiness.UpdatePassword(request.MapTo());
        }

        /// <summary>
        ///     Lista paginada de usuários por aplicação e profile
        /// </summary>
        /// <param name="request">Objeto de paginação com ordenação</param>
        /// <returns>Lista paginada de usuários da Api e Profile escolhidos</returns>
        [HttpPost("list")]
        public async Task<PaginatedList<UserResult>> List([FromBody] FilterPaginationRequest<UserFilterIn> request)
        {
            request.Filters.ApiClientId = GetApiClientIdFromContext;
            request.Filters.UserId = GetUserIdFromContext;
            var result =
                await _userBusiness.ListByApplicationProfile(request.MapTo());
            return result.ConvertTo<UserResult>();
        }

        /// <summary>
        ///     Lista paginada de usuários por aplicação e profile
        /// </summary>
        /// <param name="apiCode">Código da API</param>
        /// <param name="request">Objeto de paginação com ordenação</param>
        /// <returns>Lista paginada de usuários da Api e Profile escolhidos</returns>
        [HttpPost("list-in-app/{apiCode}")]
        public async Task<PaginatedList<UserResult>> ListByApplicationProfile(string apiCode,
            [FromBody] FilterPaginationRequest<UserFilterIn> request)
        {
            request.Filters.ApiCode = apiCode;
            request.Filters.ClientId = GetClientIdFromContext;
            var result =
                await _userBusiness.ListByApplicationProfile(request.MapTo());
            return result.ConvertTo<UserResult>();
        }

        /// <summary>
        ///     Atualizar usuário
        /// </summary>
        /// <remarks>
        ///     Atualiza as informações de um usuário através de um objeto `UserUpdateRequest`, retornando um objeto
        ///     `UserResult` contendo o usuário ja atualizado.
        /// </remarks>
        /// <param name="request">Objeto UserUpdateRequest a atualizar</param>
        /// <returns>Objeto UserResult atualizado</returns>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.NoContent)]
        [HttpPut]
        public async Task<UserResult> Update([FromBody] UserUpdateRequest request)
        {
            var apiClientId = GetApiClientIdFromContext;
            request.ApiClientId = apiClientId;
            request.Id = GetUserIdFromContext;
            var result = await _userBusiness.Save(request.MapTo(), apiClientId);
            return new UserResult(result);
        }

        /// <summary>
        ///     Atualizar usuário
        /// </summary>
        /// <remarks>
        ///     Atualiza as informações de um usuário através de um objeto `UserUpdateRequest`, retornando um objeto
        ///     `UserResult` contendo o usuário ja atualizado.
        /// </remarks>
        /// <param name="request">Objeto UserUpdateRequest a atualizar</param>
        /// <returns>Objeto UserResult atualizado</returns>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.NoContent)]
        [HttpPut("update-by-id")]
        public async Task<UserResult> UpdateById([FromBody] UserUpdateAdminRequest request)
        {
            var result = await _userBusiness.Save(request.MapTo(), GetApiClientIdFromContext);
            return new UserResult(result);
        }

        /// <summary>
        ///     Atualizar usuário
        /// </summary>
        /// <remarks>
        ///     Atualiza as informações de um usuário através de um objeto `UserUpdateRequest`, retornando um objeto
        ///     `UserResult` contendo o usuário ja atualizado.
        /// </remarks>
        /// <param name="apiCode">Código da API</param>
        /// <param name="request">Objeto UserUpdateRequest a atualizar</param>
        /// <returns>Objeto UserResult atualizado</returns>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.NoContent)]
        [HttpPut("update-in-app/{apiCode}")]
        public async Task<UserResult> UpdateInApp(string apiCode, [FromBody] UserUpdateAdminRequest request)
        {
            var result = await _userBusiness.Save(request.MapTo(), apiCode, GetClientIdFromContext,
                GetApiClientFromContext);
            return new UserResult(result);
        }

        /// <summary>
        ///     Remover usuário
        /// </summary>
        /// <remarks>
        ///     Remove um usuário cadastrado
        /// </remarks>
        /// <returns>True se removido com sucesso</returns>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpDelete]
        public async Task<bool> Delete()
        {
            return await _userBusiness.Delete(GetUserIdFromContext, GetApiClientIdFromContext);
        }

        /// <summary>
        ///     Remover usuário
        /// </summary>
        /// <remarks>
        ///     Remove um usuário cadastrado
        /// </remarks>
        /// <returns>True se removido com sucesso</returns>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpDelete("delete-by-id/{id}")]
        public async Task<bool> DeleteById(int id)
        {
            return await _userBusiness.Delete(id, GetApiClientIdFromContext);
        }

        /// <summary>
        ///     Remover usuário
        /// </summary>
        /// <remarks>
        ///     Remove um usuário cadastrado
        /// </remarks>
        /// <returns>True se removido com sucesso</returns>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpDelete("delete-in-app/{apiCode}/{id}")]
        public async Task<bool> DeleteInApp(string apiCode, int id)
        {
            return await _userBusiness.Delete(id, apiCode, GetClientIdFromContext);
        }

        /// <summary>
        ///     Reenviar código de validação de telefone
        /// </summary>
        /// <remarks>
        ///     Reenvia o código de validação de telefone a partir das informações do objeto `TelephoneValidationResendRequest`.
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpPatch("telephone/resend")]
        public async Task<bool> ResendTelephoneValidationCode([FromBody] TelephoneValidationResendRequest request)
        {
            return await _userBusiness.ResendTelephoneValidationCode(GetUserIdFromContext, request.PhoneNumber,
                GetApiClientIdFromContext, null);
        }
    }
}