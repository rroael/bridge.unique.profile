using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Bridge.Commons.System.Exceptions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Resources;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Domain.Business
{
    public class ClientUserBusiness : IClientUserBusiness
    {
        private readonly IClientBusiness _clientBusiness;
        private readonly IUserBusiness _userBusiness;

        public ClientUserBusiness(IClientBusiness clientBusiness, IUserBusiness userBusiness)
        {
            _clientBusiness = clientBusiness;
            _userBusiness = userBusiness;
        }

        public async Task Create(ClientUser request, List<string> apiCodes)
        {
            var step = 0;

            async Task Rollback(Exception ex)
            {
                if (step == 1) await _clientBusiness.DeleteClientApi(request.Client, apiCodes);
                if (step >= 2)
                {
                    await _userBusiness.Delete(request.User.Email, request.Client.Code, apiCodes);
                    await _clientBusiness.DeleteClientApi(request.Client, apiCodes);
                }

                throw new BusinessException((int)EError.CLIENT_COULD_NOT_BE_REGISTERED,
                    Errors.ClientCouldNotBeRegistered);
            }

            if (await _userBusiness.ExistsInAnotherPortal(request.User.Email))
                throw new BusinessException((int)EError.USER_ALREADY_EXISTS_IN_PORTAL,
                    Errors.UserAlreadyExistsInPortal);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    step = 1;
                    var client = await _clientBusiness.Save(request.Client, apiCodes);

                    step = 2;
                    var papApiClient = client.Apis.First(f =>
                        f.Code.Equals(request.UserApiCodeToAssociate + "_" + request.Client.Code));
                    request.User.ApiClientId = papApiClient.Id;

                    await _userBusiness.Save(request.User, papApiClient.Id);

                    step = 3;
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    await Rollback(ex);
                }
            }
        }
    }
}