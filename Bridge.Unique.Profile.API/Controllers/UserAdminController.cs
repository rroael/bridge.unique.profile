using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bridge.Commons.System.Models;
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
    public class UserAdminController : BaseController
    {
        private readonly IUserBusiness _userBusiness;

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="userBusiness">DI de usuário</param>
        /// <param name="authenticationBusiness">DI de autenticação</param>
        public UserAdminController(IUserBusiness userBusiness, IAuthenticationBusiness authenticationBusiness) :
            base(authenticationBusiness)
        {
            _userBusiness = userBusiness;
        }

        /// <summary>
        ///     Autenticar usuário
        /// </summary>
        /// <returns>Objeto AuthUserResult autenticado</returns>
        /// <remarks>
        ///     Realiza a autenticação do usuário através do *Header*, retornando um objeto `AuthUserResult`.
        /// </remarks>
        [ProducesResponseType(typeof(AuthUserResult), (int)HttpStatusCode.OK)]
        [HttpGet("authenticate")]
        public AuthUserResult Authenticate()
        {
            var request = new TokenRequest
            {
                AccessToken = Authorization,
                ApplicationToken = AppAuthorization
            };

            var result = _userBusiness.Authenticate(request.MapTo());
            return new AuthUserResult(result);
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
            var apiClientId = request.ApiClientId = GetApiClientIdFromContext;
            var result = await _userBusiness.Save(request.MapTo(), apiClientId);
            return new UserResult(result);
        }

        /// <summary>
        ///     Criar novo usuário com dados essenciais
        /// </summary>
        /// <param name="request">Objeto UserEssentialRequest a criar</param>
        /// <returns>Objeto UserResult criado</returns>
        /// <remarks>
        ///     Cria um novo usuário com dados essenciais a partir das informações do objeto `UserEssentialRequest`, retornando
        ///     um objeto `UserResult`.
        /// </remarks>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpPost("create/essential")]
        public async Task<UserResult> CreateEssential([FromBody] UserEssentialRequest request)
        {
            var apiClientId = request.ApiClientId = GetApiClientIdFromContext;
            var result = await _userBusiness.Save(request.MapTo(), apiClientId);
            return new UserResult(result);
        }

        /// <summary>
        ///     Remover usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>True se o usuário foi removido com sucesso</returns>
        /// <remarks>
        ///     Remove um usuário cadastrado.
        /// </remarks>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _userBusiness.Delete(id, GetApiClientIdFromContext);
        }

        /// <summary>
        ///     Buscar usuário por identificador
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Objeto UserResult encontrado</returns>
        /// <remarks>
        ///     Busca um usuário retornando um objeto `UserResult` contendo o usuário encontrado.
        /// </remarks>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public async Task<UserResult> Get(int id)
        {
            var result = await _userBusiness.Get(id, GetApiClientIdFromContext);

            return new UserResult(result);
        }

        /// <summary>
        ///     Buscar usuário (Admin) por identificador
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Objeto UserResult encontrado</returns>
        /// <remarks>
        ///     Busca um usuário retornando um objeto `UserResult` contendo o usuário encontrado.
        /// </remarks>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpGet("admin/{id}")]
        public async Task<UserResult> GetAdmin(int id)
        {
            var result = await _userBusiness.GetAdmin(id);

            return new UserResult(result);
        }

        /// <summary>
        ///     Busca usuário pelo ApiClientCode
        /// </summary>
        /// <param name="userId">Identificador do usuário</param>
        /// <param name="apiClientCode">Código da aplicação</param>
        /// <returns>Objeto UserResult encontrado</returns>
        [HttpGet("admin/by-api-client")]
        public async Task<UserResult> GetByApiClientAdmin(int userId, string apiClientCode)
        {
            var result = await _userBusiness.GetByApiClientAdmin(userId, apiClientCode);

            return new UserResult(result);
        }

        /// <summary>
        ///     Listar usuários por aplicação
        /// </summary>
        /// <param name="request">Objeto de paginação com ordenação</param>
        /// <returns>Lista paginada de usuários da Api atual</returns>
        /// <remarks>
        ///     Realiza a listagem de usuários da API atual, retornando uma lista paginada do objeto `UserResult`.
        /// </remarks>
        [ProducesResponseType(typeof(PaginatedList<UserResult>), (int)HttpStatusCode.OK)]
        [HttpGet("list")]
        public async Task<PaginatedList<UserResult>> ListByApplication([FromQuery] PaginationRequest request)
        {
            var result =
                await _userBusiness.ListByApplication(GetApiClientIdFromContext, request.MapTo());
            return result.ConvertTo<UserResult>();
        }

        /// <summary>
        ///     Lista paginada de usuários por aplicação e profile
        /// </summary>
        /// <param name="request">Objeto de paginação com ordenação</param>
        /// <returns>Lista paginada de usuários da Api e Profile escolhidos</returns>
        [HttpGet("list-app-profile")]
        public async Task<PaginatedList<UserResult>> ListByApplicationProfile(
            [FromBody] FilterPaginationRequest<UserFilterIn> request)
        {
            request.Filters.ClientId = GetApiClientFromContext.ClientId;
            var result =
                await _userBusiness.ListByApplicationProfile(request.MapTo());
            return result.ConvertTo<UserResult>();
        }

        /// <summary>
        ///     Lista de usuários por Ids
        /// </summary>
        /// <param name="request">Objeto contendo Ids dos usuários</param>
        /// <returns>Lista de usuários com endereço padrào preenchido</returns>
        [ProducesResponseType(typeof(List<UserResult>), (int)HttpStatusCode.OK)]
        [HttpGet("listbyid")]
        public async Task<List<UserResult>> ListById([FromQuery] UserListByIdRequest request)
        {
            var result = await _userBusiness.ListById(request.Ids, new Pagination
            {
                Page = 1,
                PageSize = request.Ids.Length
            });

            return result.ConvertTo<UserResult>().Data.ToList();
        }

        /// <summary>
        ///     Logar usuário
        /// </summary>
        /// <param name="request">Objeto LoginRequest</param>
        /// <returns>Objeto UserResult do usuário logado</returns>
        /// <remarks>
        ///     Realiza o login do usuário a partir das informações do objeto de `LoginRequest`, retornando um objeto de
        ///     `UserResult`.
        /// </remarks>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpPost("login")]
        public async Task<UserResult> Login([FromBody] LoginRequest request)
        {
            request.ApplicationToken = AppAuthorization;
            request.ApiClientId = GetApiClientIdFromContext;
            var result = await _userBusiness.Login(request.MapTo());
            return new UserResult(result);
        }

        /// <summary>
        ///     Logar usuário por apiCode, ignorando o ApiClient da chave de autenticação
        /// </summary>
        /// <param name="apiCode">Código da API na qual logar</param>
        /// <param name="request">Objeto LoginRequest</param>
        /// <returns>Objeto UserResult do usuário logado</returns>
        /// <remarks>
        ///     Realiza o login do usuário a partir das informações do objeto de `LoginRequest`, retornando um objeto de
        ///     `UserResult`.
        /// </remarks>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpPost("{apiCode}/login")]
        public async Task<UserResult> AdminLogin(string apiCode, [FromBody] LoginRequest request)
        {
            request.ApiCode = apiCode;
            request.ApplicationToken = AppAuthorization;
            var result = await _userBusiness.LoginByApi(request.MapTo());
            return new UserResult(result);
        }

        /// <summary>
        ///     Logar o usuário por rede social
        /// </summary>
        /// <param name="request">Objeto LoginRequest</param>
        /// <returns>Objeto UserResult do usuário logado</returns>
        /// <remarks>
        ///     Realiza o login do usuário por meio das plataformas "Facebook" e "Google" a partir das informações do objeto
        ///     `LoginSocialRequest`, retornando um objeto `UserResult`.
        /// </remarks>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpPost("login/social")]
        public async Task<UserResult> LoginSocial([FromBody] LoginSocialRequest request)
        {
            var apiClient = GetApiClientFromContext;
            request.ApplicationToken = AppAuthorization;
            request.ApiClientId = GetApiClientIdFromContext;
            var result = await _userBusiness.LoginSocial(request.MapTo(), apiClient);
            return new UserResult(result);
        }

        /// <summary>
        ///     Deslogar usuário
        /// </summary>
        /// <param name="request">Objeto TokenRequest a deslogar</param>
        /// <returns>True se deslogou com sucesso</returns>
        /// <remarks>
        ///     Encerra a sessão do usuário a partir das informações do objeto `TokenRequest`.
        /// </remarks>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpPost("logout")]
        public async Task<bool> Logout([FromBody] TokenRequest request)
        {
            //request.AccessToken ??= Authorization;
            request.ApplicationToken = AppAuthorization;

            return await _userBusiness.Logout(request.MapTo());
        }

        /// <summary>
        ///     Recuperar senha
        /// </summary>
        /// <param name="apiCode">Código da API</param>
        /// <param name="request">Objeto PasswordRecoverRequest</param>
        /// <returns>Objeto ServiceResult</returns>
        /// <remarks>
        ///     Recupera a senha do usuário a partir das informções do objeto `PasswordRecoverRequest`.
        /// </remarks>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpPatch("{apiCode}/password/recover")]
        public async Task<bool> AdminRecoverPassword(string apiCode, [FromBody] PasswordRecoverRequest request)
        {
            request.ApiCode = apiCode;
            return await _userBusiness.RecoverPassword(request.MapTo(), null);
        }

        /// <summary>
        ///     Recuperar senha
        /// </summary>
        /// <param name="request">Objeto LoginRequest</param>
        /// <returns>Objeto ServiceResult</returns>
        /// <remarks>
        ///     Recupera a senha do usuário a partir das informções do objeto `PasswordRecoverRequest`.
        /// </remarks>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpPatch("password/recover")]
        public async Task<bool> RecoverPassword([FromBody] PasswordRecoverRequest request)
        {
            request.ApplicationToken = AppAuthorization;
            return await _userBusiness.RecoverPassword(request.MapTo(), GetApiClientFromContext);
        }

        /// <summary>
        ///     Atualizar token
        /// </summary>
        /// <param name="request">Objeto RefreshTokenRequest</param>
        /// <returns>Objeto TokenResult atualizado</returns>
        /// <remarks>
        ///     Atualiza o token de acesso do usuário a partir das informações do objeto `RefreshTokenRequest`, retornando
        ///     um objeto `TokenResult`.
        /// </remarks>
        [ProducesResponseType(typeof(TokenResult), (int)HttpStatusCode.OK)]
        [HttpPatch("token/refresh")]
        public async Task<TokenResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var tokenRequest = new TokenRequest
            {
                RefreshToken = request.RefreshToken,
                ApplicationToken = AppAuthorization
            };

            var result = await _userBusiness.RefreshToken(tokenRequest.MapTo());
            return new TokenResult(result);
        }

        /// <summary>
        ///     Reenviar e-mail de validação
        /// </summary>
        /// <param name="request">Objeto EmailValidationResendRequest</param>
        /// <returns></returns>
        /// <remarks>
        ///     Reenvia o e-mail de validação a partir das informações do objeto `EmailValidationResendRequest`.
        /// </remarks>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpPatch("email/resend")]
        public async Task<bool> ResendEmailValidationLink([FromBody] EmailValidationResendRequest request)
        {
            await _userBusiness.ResendEmailValidationLink(request.Email, GetApiClientFromContext);
            return true;
        }

        /// <summary>
        ///     Reenviar código de validação de telefone
        /// </summary>
        /// <param name="request">Objeto TelephoneValidationResendRequest</param>
        /// <returns></returns>
        /// <remarks>
        ///     Reenvia o código de validação de telefone a partir das informações do objeto `TelephoneValidationResendRequest`.
        /// </remarks>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpPatch("telephone/resend")]
        public async Task<bool> ResendTelephoneValidationCode([FromBody] TelephoneValidationResendRequest request)
        {
            return await _userBusiness.ResendTelephoneValidationCode(request.UserId, request.PhoneNumber,
                GetApiClientIdFromContext, null);
        }

        /// <summary>
        ///     Atualizar usuário
        /// </summary>
        /// <param name="request">Objeto UserUpdateRequest</param>
        /// <returns>Objeto UserResult atualizado</returns>
        /// <remarks>
        ///     Atualiza as informações de um usuário através de um objeto `UserUpdateRequest`, retornando um objeto
        ///     `UserResult` contendo o usuário ja atualizado.
        /// </remarks>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpPut]
        public async Task<UserResult> Update([FromBody] UserUpdateAdminRequest request)
        {
            var apiClientId = request.ApiClientId = GetApiClientIdFromContext;
            var result = await _userBusiness.Save(request.MapTo(), apiClientId);
            return new UserResult(result);
        }

        /// <summary>
        ///     Validar número de telefone
        /// </summary>
        /// <param name="request">Objeto TelephoneValidateRequest</param>
        /// <returns></returns>
        /// <remarks>
        ///     Valida o número de telefone do usuário a partir das informações do objeto `TelephoneValidateRequest`.
        /// </remarks>
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [HttpPatch("telephone/validate")]
        public async Task<bool> ValidateTelephone([FromBody] TelephoneValidateRequest request)
        {
            return await _userBusiness.ValidateTelephone(request.PhoneNumber, request.Code);
        }

        /// <summary>
        ///     Ativar ou inativar um usuário de uma aplicação
        /// </summary>
        /// <remarks>
        ///     Ativa ou desativa um usuário de uma aplicação a partir das informações do objeto `UserActiveAdminRequest`.
        /// </remarks>
        /// <param name="request">Objeto UserAdminActionRequest</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(UserResult), (int)HttpStatusCode.OK)]
        [HttpPatch("active")]
        public async Task<NoContentResult> AdminActiveUser([FromBody] UserActionRequest request)
        {
            request.ApiClientId = GetApiClientIdFromContext;
            await _userBusiness.AdminActiveUserUpdate(request.MapTo());
            return NoContent();
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
        public async Task<NoContentResult> AdminUserApproval([FromBody] UserActionRequest request)
        {
            request.ApiClientId = GetApiClientIdFromContext;
            await _userBusiness.AdminUserApproval(request.MapTo());
            return NoContent();
        }
    }
}