using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.Extension;
using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;

namespace Bridge.Unique.Profile.Domain.Business
{
    public class ApiBusiness : BaseBusiness, IApiBusiness
    {
        private readonly IApiRepository _apiRepository;
        private readonly IAuthenticationBusiness _authenticationBusiness;

        public ApiBusiness(IApiRepository apiRepository,
            IAuthenticationBusiness authenticationBusiness, IBaseValidator<Api> applicationValidator) : base(
            applicationValidator)
        {
            _apiRepository = apiRepository;
            _authenticationBusiness = authenticationBusiness;
        }

        public async Task<ApiClient> Authenticate(string token)
        {
            return await Execute(_authenticationBusiness.AuthenticateApi, token);
        }

        public async Task<PaginatedList<Api>> List(int page)
        {
            var pagination = new Pagination
            {
                Page = page,
                Order = ESortType.ASCENDING,
                PageSize = 10,
                SortField = ""
            };

            return await Execute(_apiRepository.List, pagination);
        }

        public async Task<Api> Get(int id)
        {
            return await Execute(_apiRepository.Get, new IdentifiableInt(id));
        }

        public async Task<List<Api>> GetByCodes(List<string> codes)
        {
            return await Execute(_apiRepository.GetByCodes, codes);
        }

        public async Task<Api> Get(string applicationToken)
        {
            return await Execute(_apiRepository.Get, applicationToken.ToHash(HashType));
        }

        public async Task<Api> Save(Api api)
        {
            //var token = await _authenticationBusiness.GenerateApplicationToken(api);
            //api.Token = token.ToHash(HashType);

            return await ExecuteValidate(_apiRepository.Save, api);
        }

        public async Task<Api> Delete(int id)
        {
            return await Execute(_apiRepository.Delete, new IdentifiableInt(id));
        }
    }
}