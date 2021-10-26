using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Domain.Repositories.Contracts
{
    public interface IContactRepository
    {
        Task<Contact> Get(IIdentifiable<long> identifiable);
        Task<PaginatedList<Contact>> List(IEnumerable<long> ids, Pagination pagination);
        Task<PaginatedList<Contact>> ListByClient(IIdentifiable<long> clientId, Pagination pagination);
        Task<Contact> Save(Contact request);
        Task<bool> Delete(IIdentifiable<long> identifiable, int clientId);
    }
}