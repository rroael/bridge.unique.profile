using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Bridge.Commons.System.Exceptions;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Requests;
using Bridge.Commons.System.Models.Results;
using Bridge.Commons.System.Models.Validations;
using Bridge.Unique.Profile.Communication.Models.In;
using Bridge.Unique.Profile.Communication.Models.In.Addresses;
using Bridge.Unique.Profile.Communication.Models.In.Filters;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using Bridge.Unique.Profile.Communication.Models.Out;
using Bridge.Unique.Profile.Wrapper.Configurations;
using Bridge.Unique.Profile.Wrapper.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using IRestClient = Bridge.Commons.Rest.IRestClient;

namespace Bridge.Unique.Profile.Wrapper.Services
{
    /// <summary>
    ///     Serviços BUP
    /// </summary>
    public class BupService : BupAuthenticationService, IBupService
    {
        #region Admin ClientUser

        /// <summary>
        ///     Cria cliente e usuário inicial
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<NoContentResult>> AdminClientUserCreate(ClientUserIn @in,
            BaseHeader headers = null)
        {
            const string route = "client-user-admin";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<NoContentResult>(route, @in, headers: headers.GetHeaders());
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Contrutor serviços BUP
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="authorization"></param>
        /// <param name="restClient"></param>
        public BupService(string baseUrl, string authorization = null, IRestClient restClient = null) : base(baseUrl,
            authorization, restClient)
        {
        }

        /// <summary>
        ///     Contrutor serviços BUP
        /// </summary>
        /// <param name="configuration"></param>
        public BupService(BupConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        ///     Contrutor serviços BUP
        /// </summary>
        /// <param name="baseUrl"></param>
        public BupService(string baseUrl) : base(baseUrl)
        {
        }

        /// <summary>
        ///     Contrutor serviços BUP
        /// </summary>
        /// <param name="restClient"></param>
        public BupService(IRestClient restClient) : base(restClient)
        {
        }

        #endregion

        #region Address

        /// <summary>
        ///     Criar endereço
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<AddressOut>> AddressCreate(AddressIn @in, BaseHeader headers = null)
        {
            const string route = "address/";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<AddressIn, AddressOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Criar endereço
        /// </summary>
        /// <param name="in"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<AddressOut>> AddressCreate(AddressIn @in, string authorization)
        {
            return await AddressCreate(@in, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Buscar endereço
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<AddressOut>> AddressGet(long id, BaseHeader headers = null)
        {
            var route = $"address/{id}";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<AddressOut>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Buscar endereço
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<AddressOut>> AddressGet(long id, string authorization)
        {
            return await AddressGet(id, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Listar endereços
        /// </summary>
        /// <param name="page"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<PaginatedList<AddressOut>>> AddressList(int page, BaseHeader headers = null)
        {
            var route = $"address/list/{page}";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<PaginatedList<AddressOut>>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Listar endereços
        /// </summary>
        /// <param name="page"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<PaginatedList<AddressOut>>> AddressList(int page, string authorization)
        {
            return await AddressList(page, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Atualizar endereço
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<AddressOut>> AddressUpdate(AddressUpdateIn @in, BaseHeader headers = null)
        {
            const string route = "address/";
            headers ??= GetDefaultHeader();
            return await _restClient.PutAsync<AddressUpdateIn, AddressOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Atualizar endereço
        /// </summary>
        /// <param name="in"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<AddressOut>> AddressUpdate(AddressUpdateIn @in, string authorization)
        {
            return await AddressUpdate(@in, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Deletar endereço
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> AddressDelete(long id, BaseHeader headers = null)
        {
            var route = $"address/{id}";
            headers ??= GetDefaultHeader();
            return await _restClient.DeleteAsync<bool>(route, null, headers.GetHeaders());
        }

        /// <summary>
        ///     Deletar endereço
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> AddressDelete(long id, string authorization)
        {
            return await AddressDelete(id, GetDefaultHeader(authorization));
        }

        #endregion

        #region Admin - App Authentication

        /// <summary>
        ///     Criar usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> AdminUserCreate(UserIn @in, BaseHeader headers = null)
        {
            const string route = "user-admin";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<UserIn, UserOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Criar usuário admin essentials
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> AdminUserEssentialCreate(UserEssentialIn @in,
            BaseHeader headers = null)
        {
            const string route = "user-admin/create/essential";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<UserEssentialIn, UserOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Deletar usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> AdminUserDelete(int id, BaseHeader headers = null)
        {
            const string route = "user-admin";
            headers ??= GetDefaultHeader();
            return await _restClient.DeleteAsync<UserOut>(route, id, headers.GetHeaders());
        }

        /// <summary>
        ///     Buscar usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> AdminUserGet(int id, BaseHeader headers = null)
        {
            var route = $"user-admin/{id}";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<UserOut>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Buscar usuário como administrador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> AdminUserGetAsAdmin(int id, BaseHeader headers = null)
        {
            var route = $"user-admin/admin/{id}";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<UserOut>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Consulta usuário
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="apiClientCode"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> AdminUserGetByApiClientAsAdmin(int userId, string apiClientCode,
            BaseHeader headers = null)
        {
            const string route = "user-admin/admin/by-api-client";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<UserOut>(route, new { userId, apiClientCode },
                headers.GetHeaders());
        }

        /// <summary>
        ///     Listar usuários
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<PaginatedList<UserOut>>> AdminUserList(Pagination pagination,
            BaseHeader headers = null)
        {
            const string route = "user-admin/list";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<PaginatedList<UserOut>>(route, pagination, headers.GetHeaders());
        }

        /// <summary>
        ///     Listar usuários pelo Id
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns>Uma lista de usuários com o endereço padrão preenchido</returns>
        public async Task<IRestResponse<List<UserOut>>> AdminUserListById(UserListByIdIn @in,
            BaseHeader headers = null)
        {
            const string route = "user-admin/listbyid";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<List<UserOut>>(route, @in, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Listar usuário admin por perfil da aplicação
        /// </summary>
        /// <param name="request"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<PaginatedList<UserOut>>> AdminUserListByApplicationProfile(
            FilterPaginationRequest<UserFilterIn> request,
            BaseHeader headers = null)
        {
            const string route = "user-admin/list-app-profile";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<PaginatedList<UserOut>>(route, request, headers.GetHeaders());
        }

        /// <summary>
        ///     Logar usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> AdminUserLogin(LoginIn @in, BaseHeader headers = null)
        {
            const string route = "user-admin/login";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<LoginIn, UserOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Logar usuário por apiCode, ignorando o ApiClient da chave de autenticação
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> AdminUserLoginByApiCode(LoginIn @in, BaseHeader headers = null)
        {
            var route = $"user-admin/{@in.ApiCode}/login";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<LoginIn, UserOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Logar usuário por rede social
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> AdminUserLoginSocial(UserSocialIn @in, BaseHeader headers = null)
        {
            const string route = "user-admin/login/social";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<UserSocialIn, UserOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Encerrar sessão do usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> AdminUserLogout(TokenIn @in, BaseHeader headers = null)
        {
            const string route = "user-admin/logout";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<TokenIn, bool>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Encerrar sessão do usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> AdminUserLogout(TokenIn @in, string authorization)
        {
            return await AdminUserLogout(@in, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Recuperar senha do usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> AdminUserRecoverPassword(PasswordRecoverIn @in,
            BaseHeader headers = null)
        {
            const string route = "user-admin/password/recover";
            headers ??= GetDefaultHeader();
            return await _restClient.PatchAsync<PasswordRecoverIn, bool>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Recuperar senha do usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> AdminUserRecoverPasswordByApiCode(PasswordRecoverIn @in,
            BaseHeader headers = null)
        {
            var route = $"user-admin/{@in.ApiCode}/password/recover";
            headers ??= GetDefaultHeader();
            return await _restClient.PatchAsync<PasswordRecoverIn, bool>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Atualizar token do usuãrio
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<TokenOut>> AdminUserRefreshToken(RefreshTokenIn @in, BaseHeader headers = null)
        {
            const string route = "user-admin/token/refresh";
            headers ??= GetDefaultHeader();
            return await _restClient.PatchAsync<RefreshTokenIn, TokenOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Reenviar e-mail com link para validação
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> AdminUserResendEmailValidationLink(EmailValidationResendIn @in,
            BaseHeader headers = null)
        {
            const string route = "user-admin/email/resend";
            headers ??= GetDefaultHeader();
            return await _restClient.PatchAsync<EmailValidationResendIn, bool>(route, @in,
                headers.GetHeaders());
        }

        /// <summary>
        ///     Reenviar código de validação
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> AdminUserResendTelephoneValidationCode(
            TelephoneValidationResendIn @in,
            BaseHeader headers = null)
        {
            const string route = "user-admin/telephone/resend";
            headers ??= GetDefaultHeader();
            return await _restClient.PatchAsync<TelephoneValidationResendIn, bool>(route, @in,
                headers.GetHeaders());
        }

        /// <summary>
        ///     Atualizar usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> AdminUserUpdate(UserUpdateAdminIn @in, BaseHeader headers = null)
        {
            const string route = "user-admin";
            headers ??= GetDefaultHeader();
            return await _restClient.PutAsync<UserUpdateAdminIn, UserOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Validar telefone
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> AdminUserValidateTelephone(TelephoneValidateIn @in,
            BaseHeader headers = null)
        {
            const string route = "user-admin/telephone/validate";
            headers ??= GetDefaultHeader();
            return await _restClient.PatchAsync<TelephoneValidateIn, bool>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Ativar/desativar usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<NoContentResult>> AdminActiveUser(UserActionIn @in,
            BaseHeader headers = null)
        {
            const string route = "user-admin/active";
            headers ??= GetDefaultHeader();
            return await _restClient.PatchAsync<NoContentResult>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Aprovar/Reprovar acesso do usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<NoContentResult>> AdminUserApproval(UserActionIn @in,
            BaseHeader headers = null)
        {
            const string route = "user-admin/approve";
            headers ??= GetDefaultHeader();
            return await _restClient.PatchAsync<NoContentResult>(route, @in, headers.GetHeaders());
        }

        #endregion

        #region Admin - Address

        /// <summary>
        ///     Criar endereço
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<AddressOut>> AdminAddressCreate(AddressIn @in, BaseHeader headers = null)
        {
            const string route = "address-admin/";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<AddressIn, AddressOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Buscar endereço
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<AddressOut>> AdminAddressGet(long id, BaseHeader headers = null)
        {
            var route = $"address-admin/{id}";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<AddressOut>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Listar endereços por id
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<List<AddressOut>>> AdminAddressListById(AddressListByIdIn @in,
            BaseHeader headers = null)
        {
            const string route = "address-admin/listbyid/";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<List<AddressOut>>(route, @in, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Atualizar endereço
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<AddressOut>> AdminAddressUpdate(AddressUpdateIn @in, BaseHeader headers = null)
        {
            const string route = "address-admin/";
            headers ??= GetDefaultHeader();
            return await _restClient.PutAsync<AddressUpdateIn, AddressOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Buscar endereço por cep
        /// </summary>
        /// <param name="zipCode"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<AddressOut>> AdminAddressGetByZipCode(string zipCode, BaseHeader headers = null)
        {
            var route = $"address-admin/zipcode/{zipCode}";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<AddressOut>(route, headers: headers.GetHeaders());
        }

        #endregion

        #region Client

        /// <summary>
        ///     Buscar cliente
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<ClientOut>> ClientGet(BaseHeader headers = null)
        {
            const string route = "client";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<ClientOut>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Atualiza cliente
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<ClientOut>> ClientUpdate(ClientEssentialUpdateIn @in, BaseHeader headers = null)
        {
            const string route = "client";
            headers ??= GetDefaultHeader();
            return await _restClient.PutAsync<ClientEssentialUpdateIn, ClientOut>(route, @in, headers.GetHeaders());
        }

        #endregion

        #region Contact

        /// <summary>
        ///     Criar contato
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<ContactOut>> ContactCreate(ContactIn @in, BaseHeader headers = null)
        {
            const string route = "contact/";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<ContactIn, ContactOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Buscar contato
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<ContactOut>> ContactGet(long id, BaseHeader headers = null)
        {
            var route = $"contact/{id}";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<ContactOut>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Listar contatos
        /// </summary>
        /// <param name="page"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<PaginatedList<ContactOut>>> ContactList(int page, BaseHeader headers = null)
        {
            var route = $"contact/list/{page}";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<PaginatedList<ContactOut>>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Atualizar contato
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<ContactOut>> ContactUpdate(ContactIn @in, BaseHeader headers = null)
        {
            const string route = "contact/";
            headers ??= GetDefaultHeader();
            return await _restClient.PutAsync<ContactIn, ContactOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Deletar contato
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> ContactDelete(long id, BaseHeader headers = null)
        {
            var route = $"contact/{id}";
            headers ??= GetDefaultHeader();
            return await _restClient.DeleteAsync<bool>(route, null, headers.GetHeaders());
        }

        #endregion

        #region Admin Client

        /// <summary>
        ///     Cria cliente
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<ClientOut>> AdminClientCreate(ClientIn @in, BaseHeader headers = null)
        {
            const string route = "client-admin";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<ClientOut>(route, @in, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Filtra cliente
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<PaginatedList<ClientOut>>> AdminClientFilter(
            FilterPaginationRequest<ClientFilterIn> @in, BaseHeader headers = null)

        {
            const string route = "client-admin/filter";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<PaginatedList<ClientOut>>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Busca cliente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<ClientOut>> AdminClientGet(long id, BaseHeader headers = null)
        {
            var route = $"client-admin/{id}";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<ClientOut>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Deleta cliente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> AdminClientDelete(long id, BaseHeader headers = null)
        {
            var route = $"client-admin/{id}";
            headers ??= GetDefaultHeader();
            return await _restClient.DeleteAsync<bool>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Lista todos os clientes
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<PaginatedList<ClientOut>>> AdminClientList(PaginationRequest pagination,
            BaseHeader headers = null)
        {
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<PaginatedList<ClientOut>>("client-admin/list", pagination,
                headers.GetHeaders());
        }

        /// <summary>
        ///     Lista todos os clientes
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<PaginatedList<ClientOut>>> AdminClientListByApi(PaginationRequest pagination,
            BaseHeader headers = null)
        {
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<PaginatedList<ClientOut>>("client-admin/list-by-api", pagination,
                headers.GetHeaders());
        }

        /// <summary>
        ///     Listar todos os clientes associados à API
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<List<ClientOut>>> AdminClientListAllByApi(BaseHeader headers = null)
        {
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<List<ClientOut>>("client-admin/list-all",
                headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Consulta Id do cliente pelo codígo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<int>> AdminClientGetIdByCode(string code, BaseHeader headers = null)
        {
            var route = $"client-admin/getIdByCode/{code}";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<int>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Atualiza cliente
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<ClientOut>> AdminClientUpdate(ClientUpdateIn @in, BaseHeader headers = null)
        {
            const string route = "client-admin";
            headers ??= GetDefaultHeader();
            return await _restClient.PutAsync<ClientUpdateIn, ClientOut>(route, @in, headers.GetHeaders());
        }

        #endregion

        #region User Authentication

        /// <summary>
        ///     Aprovação/Reprovação de acesso de usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<NoContentResult>> UserApprove(UserActionIn @in, BaseHeader headers)
        {
            const string route = "user/approve";
            headers ??= GetDefaultHeader();
            return await _restClient.PatchAsync<NoContentResult>(route, null, headers.GetHeaders());
        }

        /// <summary>
        ///     Aprovação/Reprovação de acesso de usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<NoContentResult>> UserApprove(UserActionIn @in, string authorization)
        {
            return await UserApprove(@in, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Aprovação/Reprovação de acesso de usuário
        /// </summary>
        /// <param name="apiCode">Código da API</param>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<NoContentResult>> UserApproveInApp(string apiCode, UserActionIn @in,
            BaseHeader headers)
        {
            var route = $"user/approve-in-app/{apiCode}";
            headers ??= GetDefaultHeader();
            return await _restClient.PatchAsync<NoContentResult>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Aprovação/Reprovação de acesso de usuário
        /// </summary>
        /// <param name="apiCode">Código da API</param>
        /// <param name="in"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<NoContentResult>> UserApproveInApp(string apiCode, UserActionIn @in,
            string authorization)
        {
            return await UserApproveInApp(apiCode, @in, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Usuário aceitou os termos
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> UserAcceptTerms(BaseHeader headers)
        {
            const string route = "user/terms/accept";
            headers ??= GetDefaultHeader();
            return await _restClient.PatchAsync<bool>(route, headers.GetHeaders());
        }

        /// <summary>
        ///     Usuário aceitou os termos
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> UserAcceptTerms(string authorization)
        {
            return await UserAcceptTerms(GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Deletar usuário
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> UserDelete(BaseHeader headers)
        {
            const string route = "user";
            headers ??= GetDefaultHeader();
            return await _restClient.DeleteAsync<bool>(route, null, headers.GetHeaders());
        }

        /// <summary>
        ///     Deletar usuário
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> UserDelete(string authorization)
        {
            return await UserDelete(GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Deletar usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> UserDeleteById(int id, BaseHeader headers)
        {
            var route = $"user/delete-by-id/{id}";
            headers ??= GetDefaultHeader();
            return await _restClient.DeleteAsync<bool>(route, null, headers.GetHeaders());
        }

        /// <summary>
        ///     Deletar usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> UserDeleteById(int id, string authorization)
        {
            return await UserDeleteById(id, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Deletar usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <param name="apiCode"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> UserDeleteInApp(string apiCode, int id, BaseHeader headers)
        {
            var route = $"user/delete-in-app/{apiCode}/{id}";
            headers ??= GetDefaultHeader();
            return await _restClient.DeleteAsync<bool>(route, null, headers.GetHeaders());
        }

        /// <summary>
        ///     Deletar usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorization"></param>
        /// <param name="apiCode"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> UserDeleteInApp(string apiCode, int id, string authorization)
        {
            return await UserDeleteInApp(apiCode, id, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Buscar usuário
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserGet(BaseHeader headers)
        {
            const string route = "user";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<UserOut>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Buscar usuário
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserGet(string authorization)
        {
            return await UserGet(GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Buscar usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserGetById(int id, BaseHeader headers)
        {
            var route = $"user/get-by-id/{id}";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<UserOut>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Buscar usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserGetById(int id, string authorization)
        {
            return await UserGetById(id, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Buscar usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <param name="apiCode"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserGetInApp(string apiCode, int id, BaseHeader headers)
        {
            var route = $"user/get-in-app/{apiCode}/{id}";
            headers ??= GetDefaultHeader();
            return await _restClient.GetAsync<UserOut>(route, headers: headers.GetHeaders());
        }

        /// <summary>
        ///     Buscar usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorization"></param>
        /// <param name="apiCode"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserGetInApp(string apiCode, int id, string authorization)
        {
            return await UserGetInApp(apiCode, id, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Listar usuário por perfil da aplicação
        /// </summary>
        /// <param name="request"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<PaginatedList<UserOut>>> UserListByApplication(
            FilterPaginationRequest<UserFilterIn> request,
            BaseHeader headers = null)
        {
            const string route = "user/list";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<PaginatedList<UserOut>>(route, request, headers.GetHeaders());
        }

        /// <summary>
        ///     Listar usuário por perfil da aplicação
        /// </summary>
        /// <param name="request"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<PaginatedList<UserOut>>> UserListByApplication(
            FilterPaginationRequest<UserFilterIn> request,
            string authorization)
        {
            return await UserListByApplication(request, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Listar usuário por perfil da aplicação
        /// </summary>
        /// <param name="request"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<PaginatedList<UserOut>>> UserListByApplicationProfile(
            FilterPaginationRequest<UserFilterIn> request,
            BaseHeader headers = null)
        {
            var route = $"user/list-in-app/{request.Filters.ApiCode}";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<PaginatedList<UserOut>>(route, request, headers.GetHeaders());
        }

        /// <summary>
        ///     Listar usuário por perfil da aplicação
        /// </summary>
        /// <param name="request"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<PaginatedList<UserOut>>> UserListByApplicationProfile(
            FilterPaginationRequest<UserFilterIn> request,
            string authorization)
        {
            return await UserListByApplicationProfile(request, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Reenviar código de validação do telefone
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> UserResendTelephoneValidationCode(
            TelephoneValidationResendIn @in,
            BaseHeader headers = null)
        {
            const string route = "user/telephone/resend";
            headers ??= GetDefaultHeader();
            return await _restClient.PatchAsync<TelephoneValidationResendIn, bool>(route, @in,
                headers.GetHeaders());
        }

        /// <summary>
        ///     Reenviar código de validação do telefone
        /// </summary>
        /// <param name="in"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> UserResendTelephoneValidationCode(
            TelephoneValidationResendIn @in, string authorization)
        {
            return await UserResendTelephoneValidationCode(@in, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Atualizar senha
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> UserUpdatePassword(UpdatePasswordIn @in, BaseHeader headers)
        {
            const string route = "user/password/update";
            headers ??= GetDefaultHeader();
            return await _restClient.PatchAsync<UpdatePasswordIn, bool>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Atualizar senha
        /// </summary>
        /// <param name="in"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<bool>> UserUpdatePassword(UpdatePasswordIn @in, string authorization)
        {
            return await UserUpdatePassword(@in, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Criar usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserCreate(UserIn @in, BaseHeader headers = null)
        {
            const string route = "user";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<UserIn, UserOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Criar usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserCreate(UserIn @in, string authorization)
        {
            return await UserCreate(@in, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Criar usuário
        /// </summary>
        /// <param name="apiCode"></param>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserCreateInApp(string apiCode, UserIn @in, BaseHeader headers = null)
        {
            var route = $"user/create-in-app/{apiCode}";
            headers ??= GetDefaultHeader();
            return await _restClient.PostAsync<UserIn, UserOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Criar usuário
        /// </summary>
        /// <param name="apiCode"></param>
        /// <param name="in"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserCreateInApp(string apiCode, UserIn @in, string authorization)
        {
            return await UserCreateInApp(apiCode, @in, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Atualizar usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserUpdate(UserUpdateIn @in, BaseHeader headers)
        {
            const string route = "user";
            headers ??= GetDefaultHeader();
            return await _restClient.PutAsync<UserUpdateIn, UserOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Atualizar usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserUpdate(UserUpdateIn @in, string authorization)
        {
            return await UserUpdate(@in, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Atualizar usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserUpdateById(UserUpdateAdminIn @in, BaseHeader headers)
        {
            const string route = "user/update-by-id";
            headers ??= GetDefaultHeader();
            return await _restClient.PutAsync<UserUpdateIn, UserOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Atualizar usuário
        /// </summary>
        /// <param name="in"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserUpdateById(UserUpdateAdminIn @in, string authorization)
        {
            return await UserUpdateById(@in, GetDefaultHeader(authorization));
        }

        /// <summary>
        ///     Atualizar usuário
        /// </summary>
        /// <param name="apiCode"></param>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserUpdateInApp(string apiCode, UserUpdateAdminIn @in,
            BaseHeader headers)
        {
            var route = $"user/update-in-app/{apiCode}";
            headers ??= GetDefaultHeader();
            return await _restClient.PutAsync<UserUpdateIn, UserOut>(route, @in, headers.GetHeaders());
        }

        /// <summary>
        ///     Atualizar usuário
        /// </summary>
        /// <param name="apiCode"></param>
        /// <param name="in"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<IRestResponse<UserOut>> UserUpdateInApp(string apiCode, UserUpdateAdminIn @in,
            string authorization)
        {
            return await UserUpdateInApp(apiCode, @in, GetDefaultHeader(authorization));
        }

        #endregion

        #region General

        /// <summary>
        ///     Buscar erros
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public ErrorResult GetErrors(IRestResponse response)
        {
            return response.StatusCode == HttpStatusCode.OK
                ? null
                : JsonConvert.DeserializeObject<ErrorResult>(response.Content);
        }

        /// <summary>
        ///     Get or Throw
        /// </summary>
        /// <param name="response"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="BaseException"></exception>
        public T GetOrThrow<T>(IRestResponse<T> response) where T : class
        {
            if (response == null)
                throw new NullReferenceException();

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                return response.Data;

            throw GetThrowable(response);
        }

        /// <summary>
        ///     Get or Throw
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="BaseException"></exception>
        public IConvertible GetOrThrow(IRestResponse<IConvertible> response)
        {
            if (response == null)
                throw new NullReferenceException();

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                return response.Data;

            throw GetThrowable(response);
        }

        /// <summary>
        ///     Get or Throw
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="BaseException"></exception>
        public bool GetOrThrow(IRestResponse<bool> response)
        {
            if (response == null)
                throw new NullReferenceException();

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                return response.Data;

            throw GetThrowable(response);
        }

        /// <summary>
        ///     Get Throwable
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public BaseException GetThrowable(IRestResponse response)
        {
            var errorResult = GetErrors(response);
            BaseException exception = null;

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    exception = new NotFoundException(errorResult.ErrorList, response.ErrorException);
                    break;
                case HttpStatusCode.BadRequest:
                    exception = new RequestException(errorResult.ErrorList, response.ErrorException);
                    break;
                case HttpStatusCode.Unauthorized:
                    exception = new AuthenticationException(errorResult.ErrorList, response.ErrorException);
                    break;
                case HttpStatusCode.InternalServerError:
                    exception = new RepositoryException(errorResult.ErrorList, response.ErrorException);
                    break;
                case HttpStatusCode.Conflict:
                    exception = new ConflictException(errorResult.ErrorList, response.ErrorException);
                    break;
                default:
                    exception = new BusinessException(errorResult.ErrorList, response.ErrorException);
                    break;
            }

            return exception;
        }

        /// <summary>
        ///     Throw errors
        /// </summary>
        /// <param name="response"></param>
        /// <exception cref="BaseException"></exception>
        public void ThrowErrors(IRestResponse response)
        {
            throw GetThrowable(response);
        }

        #endregion
    }
}