using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;

namespace Bridge.Unique.Profile.Domain.Business
{
    public class ContactBusiness : BaseBusiness, IContactBusiness
    {
        private readonly IContactRepository _contactRepository;

        public ContactBusiness(IContactRepository contactRepository, IBaseValidator<Contact> contactValidator) : base(
            contactValidator)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Contact> Get(long id)
        {
            return await Execute(_contactRepository.Get, new IdentifiableLong(id));
        }

        public async Task<PaginatedList<Contact>> List(IEnumerable<long> ids, int page)
        {
            var pagination = new Pagination
            {
                Page = page,
                Order = ESortType.ASCENDING,
                PageSize = 10,
                SortField = ""
            };

            return await Execute(_contactRepository.List, ids, pagination);
        }

        public async Task<PaginatedList<Contact>> ListByClient(int clientId, Pagination pagination)
        {
            return await Execute(_contactRepository.ListByClient, new IdentifiableLong(clientId), pagination);
        }

        public async Task<Contact> Save(Contact contact)
        {
            return await ExecuteValidate(_contactRepository.Save, contact);
        }

        public async Task<bool> Delete(long id, int clientId)
        {
            return await Execute(_contactRepository.Delete, new IdentifiableLong(id), clientId);
        }
    }
}