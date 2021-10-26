using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.System.Contracts.Filters;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.System.Models;

namespace Bridge.Unique.Profile.Domain.Business.Contracts
{
    public interface IUserBusiness
    {
        Task<bool> AcceptTerms(int id, int apiClientId);
        Task<User> Get(int id, int apiClientId);
        Task<User> Get(int id, string apiCode, int clientId);
        Task<User> GetAdmin(int id);
        Task<User> GetByApiClientAdmin(int id, string apiClientCode);
        Task<User> LoginByApi(Identity identity);
        Task<User> Login(Identity identity);
        Task<User> LoginSocial(IdentitySocial identity, ApiClient apiClient);
        AuthUser Authenticate(Token token);
        Task<bool> Logout(Token token);
        Task<bool> UpdatePassword(UpdatePassword updatePassword);
        Task<bool> RecoverPassword(Identity identity, ApiClient apiClient);
        Task<Token> RefreshToken(Token token);
        Task<PaginatedList<User>> ListByApplication(int apiClientId, Pagination pagination);
        Task<PaginatedList<User>> ListByApplicationProfile(IFilterPagination request);
        Task<PaginatedList<User>> ListById(int[] ids, Pagination pagination);
        Task<PaginatedList<Address>> ListAddresses(int userId);
        Task<User> Save(User user, int apiClientId, ApiClient apiClientContext = null);
        Task<User> Save(User user, string apiCode, int clientId, ApiClient apiClientContext);
        Task<bool> AdminActiveUserUpdate(UserAction user);
        Task<bool> AdminUserApproval(UserAction user);
        Task<User> SaveSocial(User user, int apiClientId);
        Task<bool> Delete(int id, int apiClientId);
        Task<bool> Delete(int id, string apiCode, int clientId);
        Task<bool> Delete(string email, string clientCode, List<string> apiCodes);
        Task<bool> ValidateEmail(string token);

        Task ResendEmailValidationLink(string email, ApiClient apiClient, bool resending = true,
            string token = null, string accessToken = null, User user = null, bool isEmailChanging = true,
            bool resendExternal = false);

        Task<bool> ResendTelephoneValidationCode(int id, string phoneNumber, int apiClientId, ApiClient apiClient,
            bool resending = true, string code = null);

        Task<bool> ValidateTelephone(string phoneNumber, string code);
        Task<bool> ValidateExternalAccess(string token);
        Task<bool> ExistsInAnotherPortal(string email);
    }
}