using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Domain.Repositories.Contracts
{
    public interface IAddressRepository
    {
        Task<Address> Get(IIdentifiable<long> identifiable);
        Task<Address> Get(IIdentifiable<long> identifiable, int userId);
        Task<PaginatedList<Address>> List(IEnumerable<long> ids, Pagination pagination);
        Task<PaginatedList<Address>> ListByUser(IIdentifiable<long> userId, Pagination pagination);
        Task<Address> Save(Address request);
        Task<bool> Delete(IIdentifiable<long> identifiable, int userId);
    }
}