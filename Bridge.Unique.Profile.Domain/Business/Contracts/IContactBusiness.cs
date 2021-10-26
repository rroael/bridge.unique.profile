using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Domain.Business.Contracts
{
    public interface IContactBusiness
    {
        Task<Contact> Get(long id);
        Task<PaginatedList<Contact>> List(IEnumerable<long> ids, int page);
        Task<PaginatedList<Contact>> ListByClient(int clientId, Pagination pagination);
        Task<Contact> Save(Contact contact);
        Task<bool> Delete(long id, int clientId);
    }
}