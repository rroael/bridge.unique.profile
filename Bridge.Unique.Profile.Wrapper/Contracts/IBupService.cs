using System;
using System.Collections.Generic;
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
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Bridge.Unique.Profile.Wrapper.Contracts
{
    /// <inheritdoc />
    /// <summary>
    ///     Interface de serviço do BUP
    /// </summary>
    public interface IBupService : IBupAuthenticationService
    {
        #region Admin ClientUser

        /// <summary>
        ///     Cria Client e Usuário inicial para uma API específica
        /// </summary>
        /// <param name="in">Objeto ClienteUser</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        Task<IRestResponse<NoContentResult>> AdminClientUserCreate(ClientUserIn @in, BaseHeader headers = null);

        #endregion

        #region Address

        /// <summary>
        ///     Criação de endereço
        /// </summary>
        /// <param name="in">Objeto Endereço</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto AddressOut com o endereço criado</returns>
        Task<IRestResponse<AddressOut>> AddressCreate(AddressIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Criação de endereço
        /// </summary>
        /// <param name="in">Objeto Endereço</param>
        /// <param name="authorization">Access Token do usuário</param>
        /// <returns>Objeto AddressOut com o endereço criado</returns>
        Task<IRestResponse<AddressOut>> AddressCreate(AddressIn @in, string authorization);

        /// <summary>
        ///     Busca endereço
        /// </summary>
        /// <param name="id">Identificador do endereço</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto AddressOut com o endereço encontrado</returns>
        Task<IRestResponse<AddressOut>> AddressGet(long id, BaseHeader headers = null);

        /// <summary>
        ///     Busca endereço
        /// </summary>
        /// <param name="id">Identificador do endereço</param>
        /// <param name="authorization">Access Token do usuário</param>
        /// <returns>Objeto AddressOut com o endereço encontrado</returns>
        Task<IRestResponse<AddressOut>> AddressGet(long id, string authorization);

        /// <summary>
        ///     Lista endereços
        /// </summary>
        /// <param name="page">Página da lista</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Lista paginada de objetos AddressOut encontrados</returns>
        Task<IRestResponse<PaginatedList<AddressOut>>> AddressList(int page, BaseHeader headers = null);

        /// <summary>
        ///     Lista endereços
        /// </summary>
        /// <param name="page">Página da lista</param>
        /// <param name="authorization">Access Token do usuário</param>
        /// <returns>Lista paginada de objetos AddressOut encontrados</returns>
        Task<IRestResponse<PaginatedList<AddressOut>>> AddressList(int page, string authorization);

        /// <summary>
        ///     Atualização de endereço
        /// </summary>
        /// <param name="in">Objeto Endereço</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto AddressOut com o endereço atualizado</returns>
        Task<IRestResponse<AddressOut>> AddressUpdate(AddressUpdateIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Atualização de endereço
        /// </summary>
        /// <param name="in">Objeto Endereço</param>
        /// <param name="authorization">Access Token do usuário</param>
        /// <returns>Objeto AddressOut com o endereço atualizado</returns>
        Task<IRestResponse<AddressOut>> AddressUpdate(AddressUpdateIn @in, string authorization);

        /// <summary>
        ///     Remoção de endereço
        /// </summary>
        /// <param name="id">Identificador do endereço</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto AddressOut com o endereço removido</returns>
        Task<IRestResponse<bool>> AddressDelete(long id, BaseHeader headers = null);

        /// <summary>
        ///     Remoção de endereço
        /// </summary>
        /// <param name="id">Identificador do endereço</param>
        /// <param name="authorization">Access Token do usuário</param>
        /// <returns>Objeto AddressOut com o endereço removido</returns>
        Task<IRestResponse<bool>> AddressDelete(long id, string authorization);

        #endregion

        #region Admin - App Authentication

        /// <summary>
        ///     Criação de usuário
        /// </summary>
        /// <param name="in">Objeto Usuário</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto UserOut com o usuário criado</returns>
        Task<IRestResponse<UserOut>> AdminUserCreate(UserIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Criação de usuário com dados essenciais
        /// </summary>
        /// <param name="in">Objeto Usuário</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto UserOut com o usuário criado</returns>
        Task<IRestResponse<UserOut>> AdminUserEssentialCreate(UserEssentialIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Remoção de usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto UserOut com o usuário removido</returns>
        Task<IRestResponse<UserOut>> AdminUserDelete(int id, BaseHeader headers = null);

        /// <summary>
        ///     Busca de usuário vinculado ao administrador
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto UserOut com o usuário encontrado</returns>
        Task<IRestResponse<UserOut>> AdminUserGet(int id, BaseHeader headers = null);

        /// <summary>
        ///     Busca de usuário como administrador (pode estar vinculado a qualquer api)
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="headers">[Opcional] headers de autenticação administrativos</param>
        /// <returns>Objeto UserOut com o usuário encontrado</returns>
        Task<IRestResponse<UserOut>> AdminUserGetAsAdmin(int id, BaseHeader headers = null);

        /// <summary>
        ///     Busca de usuário como administrador, vinculado a api do código informado
        /// </summary>
        /// <param name="userId">Identificador do usuário</param>
        /// <param name="apiClientCode">Código da aplicação</param>
        /// <param name="headers">[Opcional] headers de autenticação administrativos</param>
        /// <returns>Objeto UserOut com o usuário encontrado</returns>
        Task<IRestResponse<UserOut>> AdminUserGetByApiClientAsAdmin(int userId, string apiClientCode,
            BaseHeader headers = null);

        /// <summary>
        ///     Lista de usuários por aplicação
        /// </summary>
        /// <param name="pagination">Objeto de paginação</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Lista paginada de objetos UserOut</returns>
        Task<IRestResponse<PaginatedList<UserOut>>> AdminUserList(Pagination pagination, BaseHeader headers = null);

        /// <summary>
        ///     Listar usuários pelo Id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="headers"></param>
        /// <returns>Uma lista de usuários com o endereço padrão preenchido</returns>
        Task<IRestResponse<List<UserOut>>> AdminUserListById(UserListByIdIn request,
            BaseHeader headers = null);

        /// <summary>
        ///     Lista paginada de usuários por aplicação e profile
        /// </summary>
        /// <param name="request">Objeto de paginação e filtro</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Lista paginada de objetos UserOut</returns>
        Task<IRestResponse<PaginatedList<UserOut>>> AdminUserListByApplicationProfile(
            FilterPaginationRequest<UserFilterIn> request,
            BaseHeader headers = null);

        /// <summary>
        ///     Login de usuário
        /// </summary>
        /// <param name="in">Objeto de login</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto UserOut logado</returns>
        Task<IRestResponse<UserOut>> AdminUserLogin(LoginIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Logar usuário por apiCode, ignorando o ApiClient da chave de autenticação
        /// </summary>
        /// <param name="in">Objeto de login</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto UserOut logado</returns>
        Task<IRestResponse<UserOut>> AdminUserLoginByApiCode(LoginIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Login de usuário com Facebook ou Google
        /// </summary>
        /// <param name="in">Objeto de login</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto UserOut logado</returns>
        Task<IRestResponse<UserOut>> AdminUserLoginSocial(UserSocialIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Logout de usuário
        /// </summary>
        /// <param name="in">Objeto com tokens para efetuar logout</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto ServiceResult com código da operação (200 Ok, 500 Erro, 404 NotFound, 400 BadRequest)</returns>
        Task<IRestResponse<bool>> AdminUserLogout(TokenIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Logout de usuário
        /// </summary>
        /// <param name="in">Objeto com tokens para efetuar logout</param>
        /// <param name="authorization">Access Token do usuário</param>
        /// <returns>Objeto ServiceResult com código da operação (200 Ok, 500 Erro, 404 NotFound, 400 BadRequest)</returns>
        Task<IRestResponse<bool>> AdminUserLogout(TokenIn @in, string authorization);

        /// <summary>
        ///     Recuperação de senha do usuário
        /// </summary>
        /// <param name="in">Objeto de login</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto ServiceResult com código da operação (200 Ok, 500 Erro, 404 NotFound, 400 BadRequest)</returns>
        Task<IRestResponse<bool>> AdminUserRecoverPassword(PasswordRecoverIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Recuperação de senha do usuário
        /// </summary>
        /// <param name="in">Objeto de login</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto ServiceResult com código da operação (200 Ok, 500 Erro, 404 NotFound, 400 BadRequest)</returns>
        Task<IRestResponse<bool>> AdminUserRecoverPasswordByApiCode(PasswordRecoverIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Renovação de token de acesso de usuário
        /// </summary>
        /// <param name="in">Objeto RefreshToken</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto TokenOut com o access token renovado</returns>
        Task<IRestResponse<TokenOut>> AdminUserRefreshToken(RefreshTokenIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Reenvio de link de validação de e-mail
        /// </summary>
        /// <param name="in">Objeto EmailValidationResend</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto ServiceResult com código da operação (200 Ok, 500 Erro, 404 NotFound, 400 BadRequest)</returns>
        Task<IRestResponse<bool>> AdminUserResendEmailValidationLink(EmailValidationResendIn @in,
            BaseHeader headers = null);

        /// <summary>
        ///     Reenvia código de validação de telefone
        /// </summary>
        /// <param name="in">Objeto TelephoneValidationResend</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto ServiceResult com código da operação (200 Ok, 500 Erro, 404 NotFound, 400 BadRequest)</returns>
        Task<IRestResponse<bool>> AdminUserResendTelephoneValidationCode(TelephoneValidationResendIn @in,
            BaseHeader headers = null);

        /// <summary>
        ///     Atualiza um usuário
        /// </summary>
        /// <param name="in">Objeto User</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto UserResult atualizado</returns>
        Task<IRestResponse<UserOut>> AdminUserUpdate(UserUpdateAdminIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Valida telefone do usuário
        /// </summary>
        /// <param name="in">Objeto TelephoneValidate</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto ServiceResult com código da operação (200 Ok, 500 Erro, 404 NotFound, 400 BadRequest)</returns>
        Task<IRestResponse<bool>> AdminUserValidateTelephone(TelephoneValidateIn @in,
            BaseHeader headers = null);

        /// <summary>
        ///     Ativa ou inativa o usuário de uma aplicação
        /// </summary>
        /// <param name="in">Objeto UserAction</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto ServiceResult com código da operação (200 Ok, 500 Erro, 404 NotFound, 400 BadRequest)</returns>
        Task<IRestResponse<NoContentResult>> AdminActiveUser(UserActionIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Aprova ou reprova o acesso de um usuário
        /// </summary>
        /// <param name="in">Objeto UserActionIn</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns></returns>
        Task<IRestResponse<NoContentResult>> AdminUserApproval(UserActionIn @in, BaseHeader headers = null);

        #endregion

        #region Admin - Address

        /// <summary>
        ///     Criação de endereço
        /// </summary>
        /// <param name="in">Objeto Endereço</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto AddressOut com o endereço criado</returns>
        Task<IRestResponse<AddressOut>> AdminAddressCreate(AddressIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Busca endereço
        /// </summary>
        /// <param name="id">Identificador do endereço</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto AddressOut com o endereço encontrado</returns>
        Task<IRestResponse<AddressOut>> AdminAddressGet(long id, BaseHeader headers = null);

        /// <summary>
        ///     Lista de endereços por id
        /// </summary>
        /// <param name="in">Lista de identificadores</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Lista de endereços AddressOut</returns>
        Task<IRestResponse<List<AddressOut>>> AdminAddressListById(AddressListByIdIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Atualização de endereço
        /// </summary>
        /// <param name="in">Objeto Endereço</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto AddressOut com o endereço atualizado</returns>
        Task<IRestResponse<AddressOut>> AdminAddressUpdate(AddressUpdateIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Busca endereço por cep
        /// </summary>
        /// <param name="zipCode">Identificador do cep</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto AddressOut com o endereço encontrado</returns>
        Task<IRestResponse<AddressOut>> AdminAddressGetByZipCode(string zipCode, BaseHeader headers = null);

        #endregion

        #region Client

        /// <summary>
        ///     Retorna o Cliente do contexto atual
        /// </summary>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto ClientOut com os dados do cliente</returns>
        Task<IRestResponse<ClientOut>> ClientGet(BaseHeader headers = null);

        /// <summary>
        ///     Atualiza o cliente do contexto atual
        /// </summary>
        /// <param name="in">Objeto ClientUpdateIn</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto ClientOut com os dados do cliente</returns>
        Task<IRestResponse<ClientOut>> ClientUpdate(ClientEssentialUpdateIn @in, BaseHeader headers = null);

        #endregion

        #region Contact

        /// <summary>
        ///     Criar contato
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IRestResponse<ContactOut>> ContactCreate(ContactIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Buscar contato
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IRestResponse<ContactOut>> ContactGet(long id, BaseHeader headers = null);

        /// <summary>
        ///     Listar contatos
        /// </summary>
        /// <param name="page"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IRestResponse<PaginatedList<ContactOut>>> ContactList(int page, BaseHeader headers = null);

        /// <summary>
        ///     Atualizar contato
        /// </summary>
        /// <param name="in"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IRestResponse<ContactOut>> ContactUpdate(ContactIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Deletar contato
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IRestResponse<bool>> ContactDelete(long id, BaseHeader headers = null);

        #endregion

        #region Admin Client

        /// <summary>
        ///     Retorna o Cliente do contexto atual
        /// </summary>
        /// <param name="in">Objeto Cliente</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto ClientOut com os dados do cliente</returns>
        Task<IRestResponse<ClientOut>> AdminClientCreate(ClientIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Filtro de clientes
        /// </summary>
        /// <param name="request">Objeto FilterPaginationRequest->ClientFilterIn</param>
        /// <param name="headers">[Obrigatório] Header com Authorization da aplicação</param>
        /// <returns></returns>
        Task<IRestResponse<PaginatedList<ClientOut>>> AdminClientFilter(
            FilterPaginationRequest<ClientFilterIn> request, BaseHeader headers = null);

        /// <summary>
        ///     Retorna uma lista de clientes por API
        /// </summary>
        /// <param name="pagination">Objeto de paginação</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Lista paginada de objetos AddressOut encontrados</returns>
        Task<IRestResponse<PaginatedList<ClientOut>>> AdminClientListByApi(PaginationRequest pagination,
            BaseHeader headers = null);

        /// <summary>
        ///     Retorna o Cliente do contexto atual
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto ClientOut com os dados do cliente</returns>
        Task<IRestResponse<ClientOut>> AdminClientGet(long id, BaseHeader headers = null);

        /// <summary>
        ///     Retorna o Cliente do contexto atual
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto ClientOut com os dados do cliente</returns>
        Task<IRestResponse<bool>> AdminClientDelete(long id, BaseHeader headers = null);

        /// <summary>
        ///     Retorna o Cliente do contexto atual
        /// </summary>
        /// <param name="pagination">Objeto de paginação</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto ClientOut com os dados do cliente</returns>
        Task<IRestResponse<PaginatedList<ClientOut>>> AdminClientList(PaginationRequest pagination,
            BaseHeader headers = null);

        /// <summary>
        ///     Listar todos os clientes associados à API
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IRestResponse<List<ClientOut>>> AdminClientListAllByApi(BaseHeader headers = null);

        /// <summary>
        ///     Retorna o id do Cliente
        /// </summary>
        /// <param name="code">Código aplicação cliente</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Id do cliente</returns>
        Task<IRestResponse<int>> AdminClientGetIdByCode(string code, BaseHeader headers = null);

        /// <summary>
        ///     Atualiza o cliente do contexto atual
        /// </summary>
        /// <param name="in">Objeto ClientUpdateIn</param>
        /// <param name="headers">[Opcional] headers de autenticação</param>
        /// <returns>Objeto ClientOut com os dados do cliente</returns>
        Task<IRestResponse<ClientOut>> AdminClientUpdate(ClientUpdateIn @in, BaseHeader headers = null);

        #endregion

        #region User Authentication

        /// <summary>
        ///     Aprovação de usuário
        /// </summary>
        /// <param name="in">Objeto UserActionIn</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        Task<IRestResponse<NoContentResult>> UserApprove(UserActionIn @in, BaseHeader headers);

        /// <summary>
        ///     Aprovação de usuário
        /// </summary>
        /// <param name="in">Objeto UserActionIn</param>
        /// <param name="authorization">Authorization do usuário</param>
        Task<IRestResponse<NoContentResult>> UserApprove(UserActionIn @in, string authorization);

        /// <summary>
        ///     Aprovação de usuário
        /// </summary>
        /// <param name="apiCode">Código da API</param>
        /// <param name="in">Objeto UserActionIn</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        Task<IRestResponse<NoContentResult>> UserApproveInApp(string apiCode, UserActionIn @in, BaseHeader headers);

        /// <summary>
        ///     Aprovação de usuário
        /// </summary>
        /// <param name="apiCode">Código da API</param>
        /// <param name="in">Objeto UserActionIn</param>
        /// <param name="authorization">Authorization do usuário</param>
        Task<IRestResponse<NoContentResult>> UserApproveInApp(string apiCode, UserActionIn @in, string authorization);

        /// <summary>
        ///     Aceita os termos de uso e política de privacidade
        /// </summary>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns>True se obteve sucesso ao persistir o aceite dos termos de uso e política de privacidade</returns>
        Task<IRestResponse<bool>> UserAcceptTerms(BaseHeader headers);

        /// <summary>
        ///     Aceita os termos de uso e política de privacidade
        /// </summary>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns>True se obteve sucesso ao persistir o aceite dos termos de uso e política de privacidade</returns>
        Task<IRestResponse<bool>> UserAcceptTerms(string authorization);

        /// <summary>
        ///     Remove um usuário
        /// </summary>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns>Objeto UserOut removido</returns>
        Task<IRestResponse<bool>> UserDelete(BaseHeader headers);

        /// <summary>
        ///     Remove um usuário
        /// </summary>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns>Objeto UserOut removido</returns>
        Task<IRestResponse<bool>> UserDelete(string authorization);

        /// <summary>
        ///     Remove um usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns>Objeto UserOut removido</returns>
        Task<IRestResponse<bool>> UserDeleteById(int id, BaseHeader headers);

        /// <summary>
        ///     Remove um usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns>Objeto UserOut removido</returns>
        Task<IRestResponse<bool>> UserDeleteById(int id, string authorization);

        /// <summary>
        ///     Remove um usuário
        /// </summary>
        /// <param name="apiCode">Código da API</param>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns>Objeto UserOut removido</returns>
        Task<IRestResponse<bool>> UserDeleteInApp(string apiCode, int id, BaseHeader headers);

        /// <summary>
        ///     Remove um usuário
        /// </summary>
        /// <param name="apiCode">Código da API</param>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns>Objeto UserOut removido</returns>
        Task<IRestResponse<bool>> UserDeleteInApp(string apiCode, int id, string authorization);

        /// <summary>
        ///     Busca o usuário
        /// </summary>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns>Objeto UserOut encontrado</returns>
        Task<IRestResponse<UserOut>> UserGet(BaseHeader headers);

        /// <summary>
        ///     Busca o usuário
        /// </summary>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns>Objeto UserOut encontrado</returns>
        Task<IRestResponse<UserOut>> UserGet(string authorization);

        /// <summary>
        ///     Busca o usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns>Objeto UserOut encontrado</returns>
        Task<IRestResponse<UserOut>> UserGetById(int id, BaseHeader headers);

        /// <summary>
        ///     Busca o usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns>Objeto UserOut encontrado</returns>
        Task<IRestResponse<UserOut>> UserGetById(int id, string authorization);

        /// <summary>
        ///     Busca o usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <param name="apiCode">Código da API</param>
        /// <returns>Objeto UserOut encontrado</returns>
        Task<IRestResponse<UserOut>> UserGetInApp(string apiCode, int id, BaseHeader headers);

        /// <summary>
        ///     Busca o usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="authorization">Authorization do usuário</param>
        /// <param name="apiCode">Código da API</param>
        /// <returns>Objeto UserOut encontrado</returns>
        Task<IRestResponse<UserOut>> UserGetInApp(string apiCode, int id, string authorization);

        /// <summary>
        ///     Lista de usuário por aplicação
        /// </summary>
        /// <param name="request">Objeto FilterPaginationRequest->UserFilterIn</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns></returns>
        Task<IRestResponse<PaginatedList<UserOut>>> UserListByApplication(
            FilterPaginationRequest<UserFilterIn> request, BaseHeader headers = null);

        /// <summary>
        ///     Lista de usuário por aplicação
        /// </summary>
        /// <param name="request">Objeto FilterPaginationRequest->UserFilterIn</param>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns></returns>
        Task<IRestResponse<PaginatedList<UserOut>>> UserListByApplication(
            FilterPaginationRequest<UserFilterIn> request, string authorization);

        /// <summary>
        ///     Lista de usuário por aplicação
        /// </summary>
        /// <param name="request">Objeto FilterPaginationRequest->UserFilterIn</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns></returns>
        Task<IRestResponse<PaginatedList<UserOut>>> UserListByApplicationProfile(
            FilterPaginationRequest<UserFilterIn> request, BaseHeader headers = null);

        /// <summary>
        ///     Lista de usuário por aplicação
        /// </summary>
        /// <param name="request">Objeto FilterPaginationRequest->UserFilterIn</param>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns></returns>
        Task<IRestResponse<PaginatedList<UserOut>>> UserListByApplicationProfile(
            FilterPaginationRequest<UserFilterIn> request, string authorization);

        /// <summary>
        ///     Reenvia código de validação de telefone
        /// </summary>
        /// <param name="in">Objeto TelephoneValidationResend</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns>Objeto ServiceResult</returns>
        Task<IRestResponse<bool>> UserResendTelephoneValidationCode(TelephoneValidationResendIn @in,
            BaseHeader headers);

        /// <summary>
        ///     Reenvia código de validação de telefone
        /// </summary>
        /// <param name="in">Objeto TelephoneValidationResend</param>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns>Objeto ServiceResult</returns>
        Task<IRestResponse<bool>> UserResendTelephoneValidationCode(TelephoneValidationResendIn @in,
            string authorization);

        /// <summary>
        ///     Atualiza senha do usuário
        /// </summary>
        /// <param name="in">Objeto UpdatePassword</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns>Objeto ServiceResult</returns>
        Task<IRestResponse<bool>> UserUpdatePassword(UpdatePasswordIn @in, BaseHeader headers);

        /// <summary>
        ///     Atualiza senha do usuário
        /// </summary>
        /// <param name="in">Objeto UpdatePassword</param>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns>Objeto ServiceResult</returns>
        Task<IRestResponse<bool>> UserUpdatePassword(UpdatePasswordIn @in, string authorization);

        /// <summary>
        ///     Cria o usuário
        /// </summary>
        /// <param name="in">Objeto User</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns>Objeto UserOut atualizado</returns>
        Task<IRestResponse<UserOut>> UserCreate(UserIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Cria o usuário
        /// </summary>
        /// <param name="in">Objeto User</param>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns></returns>
        Task<IRestResponse<UserOut>> UserCreate(UserIn @in, string authorization);

        /// <summary>
        ///     Cria o usuário
        /// </summary>
        /// <param name="apiCode">Código da API</param>
        /// <param name="in">Objeto User</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns>Objeto UserOut atualizado</returns>
        Task<IRestResponse<UserOut>> UserCreateInApp(string apiCode, UserIn @in, BaseHeader headers = null);

        /// <summary>
        ///     Cria o usuário
        /// </summary>
        /// <param name="apiCode">Código da API</param>
        /// <param name="in">Objeto User</param>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns></returns>
        Task<IRestResponse<UserOut>> UserCreateInApp(string apiCode, UserIn @in, string authorization);

        /// <summary>
        ///     Atualiza o usuário
        /// </summary>
        /// <param name="in">Objeto User</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns>Objeto UserOut atualizado</returns>
        Task<IRestResponse<UserOut>> UserUpdate(UserUpdateIn @in, BaseHeader headers);

        /// <summary>
        ///     Atualiza o usuário
        /// </summary>
        /// <param name="in">Objeto User</param>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns></returns>
        Task<IRestResponse<UserOut>> UserUpdate(UserUpdateIn @in, string authorization);

        /// <summary>
        ///     Atualiza o usuário
        /// </summary>
        /// <param name="in">Objeto User</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns>Objeto UserOut atualizado</returns>
        Task<IRestResponse<UserOut>> UserUpdateById(UserUpdateAdminIn @in, BaseHeader headers);

        /// <summary>
        ///     Atualiza o usuário
        /// </summary>
        /// <param name="in">Objeto User</param>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns></returns>
        Task<IRestResponse<UserOut>> UserUpdateById(UserUpdateAdminIn @in, string authorization);

        /// <summary>
        ///     Atualiza o usuário
        /// </summary>
        /// <param name="apiCode">Código da API</param>
        /// <param name="in">Objeto User</param>
        /// <param name="headers">[Obrigatório] Header com Authorization do usuário e aplicação</param>
        /// <returns>Objeto UserOut atualizado</returns>
        Task<IRestResponse<UserOut>> UserUpdateInApp(string apiCode, UserUpdateAdminIn @in, BaseHeader headers);

        /// <summary>
        ///     Atualiza o usuário
        /// </summary>
        /// <param name="apiCode">Código da API</param>
        /// <param name="in">Objeto User</param>
        /// <param name="authorization">Authorization do usuário</param>
        /// <returns></returns>
        Task<IRestResponse<UserOut>> UserUpdateInApp(string apiCode, UserUpdateAdminIn @in, string authorization);

        #endregion

        #region General

        /// <summary>
        ///     Busca erros no response
        /// </summary>
        /// <param name="response">Response</param>
        /// <returns></returns>
        ErrorResult GetErrors(IRestResponse response);

        /// <summary>
        ///     Busca objeto throwable
        /// </summary>
        /// <param name="response">Response</param>
        /// <returns></returns>
        BaseException GetThrowable(IRestResponse response);

        /// <summary>
        ///     Retorna o objeto result ou dispara uma exceção
        /// </summary>
        /// <param name="response">Response</param>
        /// <typeparam name="T">Genérico do objeto de resultado</typeparam>
        /// <returns></returns>
        T GetOrThrow<T>(IRestResponse<T> response) where T : class;

        /// <summary>
        ///     Retorna o objeto result ou dispara uma exceção
        /// </summary>
        /// <param name="response">Response</param>
        /// <returns></returns>
        IConvertible GetOrThrow(IRestResponse<IConvertible> response);

        /// <summary>
        ///     Busca o objeto result ou dispara uma exceção
        /// </summary>
        /// <param name="response">Response</param>
        /// <returns></returns>
        bool GetOrThrow(IRestResponse<bool> response);

        /// <summary>
        ///     Dispara uma exceção com os erros do response
        /// </summary>
        /// <param name="response">Response</param>
        void ThrowErrors(IRestResponse response);

        #endregion
    }
}