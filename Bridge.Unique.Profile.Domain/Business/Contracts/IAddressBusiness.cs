using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Domain.Business.Contracts
{
    public interface IAddressBusiness
    {
        Task<Address> Get(long id);
        Task<Address> Get(long id, int userId);
        Task<PaginatedList<Address>> List(IEnumerable<long> ids, Pagination pagination);
        Task<PaginatedList<Address>> ListByUser(int userId, Pagination pagination);
        Task<Address> Save(Address address);
        Task<bool> Delete(long id, int userId);
        Task<Address> GetByZipCode(string zipCode, ApiClient apiClient);
    }
}