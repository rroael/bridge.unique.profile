using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.Extension;
using Bridge.Commons.System.Contracts.Filters;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;
using Bridge.Unique.Profile.System.Helpers;
using Bridge.Unique.Profile.System.Settings;

namespace Bridge.Unique.Profile.Domain.Business
{
    public class ClientBusiness : BaseBusiness, IClientBusiness
    {
        private readonly IApiBusiness _apiBusiness;
        private readonly IApiClientRepository _apiClientRepository;
        private readonly IApiGatewayRepository _apiGatewayRepository;
        private readonly AppSettings _appSettings;
        private readonly IAuthenticationBusiness _authenticationBusiness;
        private readonly IClientRepository _clientRepository;

        public ClientBusiness(AppSettings appSettings,
            IClientRepository clientRepository,
            IAuthenticationBusiness authenticationBusiness,
            IApiGatewayRepository apiGatewayRepository,
            IApiBusiness apiBusiness,
            IApiClientRepository apiClientRepository,
            IBaseValidator<Client> clientValidator) : base(clientValidator)
        {
            _appSettings = appSettings;
            _clientRepository = clientRepository;
            _authenticationBusiness = authenticationBusiness;
            _apiGatewayRepository = apiGatewayRepository;
            _apiBusiness = apiBusiness;
            _apiClientRepository = apiClientRepository;
        }

        public async Task<ApiClient> Authenticate(string token)
        {
            var result = await _authenticationBusiness.AuthenticateClient(token);
            return result;
        }

        public async Task<PaginatedList<Client>> Filter(IFilterPagination request)
        {
            return await _clientRepository.Filter(request);
        }

        public async Task<Client> Get(int id)
        {
            var result = await Execute(_clientRepository.Get, new IdentifiableInt(id));

            foreach (var apiClient in result.Apis)
            {
                var apiKey = await _apiGatewayRepository.Get(apiClient.ApiKeyId);

                apiClient.Token = apiKey;
            }

            return result;
        }

        public async Task<PaginatedList<Client>> List(int apiId, Pagination pagination)
        {
            return await Execute(_clientRepository.List, apiId, pagination);
        }

        public async Task<PaginatedList<Client>> List(Pagination pagination)
        {
            return await Execute(_clientRepository.List, pagination);
        }

        public async Task<List<Client>> ListAll(int apiId)
        {
            return await Execute(_clientRepository.ListAll, apiId);
        }

        public async Task<Client> Update(Client request)
        {
            return await ExecuteValidate(_clientRepository.Save, request);
        }

        public async Task<Client> ClientUpdate(Client client)
        {
            return await ExecuteValidate(_clientRepository.ClientUpdate, client);
        }

        public async Task<Client> Save(Client client, List<string> apiCodes)
        {
            var clientResult = await ExecuteValidate(_clientRepository.Save, client);

            if (client.Id == 0)
            {
                var listApis = await _apiBusiness.GetByCodes(apiCodes);
                foreach (var api in listApis)
                {
                    var tokenHash = TokenHelper.GenerateBupToken().ToHash(HashType);

                    var apiClient = await _apiClientRepository.Save(new ApiClient
                    {
                        Active = true,
                        Token = tokenHash,
                        ApiId = api.Id,
                        ClientId = clientResult.Id,
                        IsAdmin = true,
                        Code = api.Code + "_" + client.Code,
                        Sender = client.Sender.Trim(),
                        ApiKeyId = string.Empty
                    });

                    clientResult.Apis.Add(apiClient);
                }
            }

            return clientResult;
        }

        public async Task<bool> DeleteClientApi(Client clientRequest, List<string> apiCodes)
        {
            var client = await _clientRepository.GetByCode(clientRequest.Code);
            foreach (var apiClient in client.Apis)
                if (!apiClient.Api.Code.Equals("PAP"))
                    if (apiCodes.Contains(apiClient.Api.Code))
                        await _apiGatewayRepository.Delete(apiClient.ApiKeyId);

            var clientResult = await Execute(_clientRepository.Delete, new IdentifiableInt(client.Id));

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var client = await Execute(_clientRepository.Delete, new IdentifiableInt(id));

            foreach (var apiClient in client.Apis) await _apiGatewayRepository.Delete(apiClient.ApiKeyId);

            return true;
        }

        public async Task<int> GetIdByCode(string code)
        {
            var result = await Execute(_clientRepository.GetIdByCode, code);

            return result;
        }
    }
}