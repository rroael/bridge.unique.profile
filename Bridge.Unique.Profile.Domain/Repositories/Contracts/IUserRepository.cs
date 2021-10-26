using System.Threading.Tasks;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Contracts.Filters;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Domain.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<bool> AcceptTerms(IIdentifiable<int> identifiable, int apiClientId);
        Task<bool> Delete(IIdentifiable<int> identifiable, int apiClientId);
        Task<bool> DeleteByEmail(string email, int apiClientId);
        Task<User> GetAdmin(IIdentifiable<int> identifiable);
        Task<User> GetByApiClientAdmin(IIdentifiable<int> identifiable, string apiClientCode);
        Task<User> Get(IIdentifiable<int> identifiable, int apiClientId);
        Task<PaginatedList<User>> ListByApplication(IIdentifiable<int> identifiable, Pagination pagination);
        Task<PaginatedList<User>> ListByApplicationProfile(IFilterPagination request);
        Task<PaginatedList<User>> ListById(int[] ids, Pagination pagination);
        Task<PaginatedList<Address>> ListAddresses(IIdentifiable<int> identifiable, Pagination pagination);
        Task<User> Login(Identity identity, int clientId = 0);
        Task<User> Login(IdentitySocial identity);
        Task<(bool, User, ApiClient)> Save(User request);
        Task<bool> AdminActiveUserUpdate(UserAction update);
        Task<bool> AdminUserApproval(UserAction update);
        Task<bool> UpdatePassword(UpdatePassword updatePassword);
        Task<User> ValidateEmail(string auth);
        Task<bool> ValidateTelephone(string phoneNumber, string codeAuth);
        Task<User> SetNewValidationEmailToken(string email, string auth);

        Task<(User, ApiClient)> SetNewValidationTelephoneCode(int apiClientId, int id, string phoneNumber,
            string codeAuth);

        Task<User> SetNewPassword(Identity identity, string passwordAuth, int clientId = 0);
        Task<UserApiClient> ValidateExternalAccess(string auth);
        Task<(User, bool)> SetNewExternalAccessValidationToken(string email, int apiClientId, string auth);
        Task<bool> ExistsInAnotherPortal(string email);
    }
}