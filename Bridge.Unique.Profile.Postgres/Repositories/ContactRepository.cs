using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.EntityFramework.Bases.Repositories;
using Bridge.Commons.System.EntityFramework.Extensions;
using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Exceptions;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Commons.System.Resources;
using Bridge.Unique.Profile.Domain.Contexts.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;
using Bridge.Unique.Profile.Postgres.Context;
using Bridge.Unique.Profile.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bridge.Unique.Profile.Postgres.Repositories
{
    public class ContactRepository :
        BaseRepository<BupReadContext, BupWriteContext, ContactEntity, IIdentifiable<long>, long>, IContactRepository
    {
        public ContactRepository(IBupReadContext bupReadContext, IBupWriteContext bupWriteContext)
        {
            Init((BupReadContext)bupReadContext, (BupWriteContext)bupWriteContext);
        }

        public async Task<Contact> Get(IIdentifiable<long> identifiable)
        {
            ValidateIdentifiable(identifiable);

            var entity = await GetByIdentifiableAsync(identifiable);

            return entity.MapTo();
        }

        public async Task<PaginatedList<Contact>> List(IEnumerable<long> ids, Pagination pagination)
        {
            var query = GetQueryable();

            if (ids != null)
                query = query.Where(x => ids.Contains(x.Id));

            return await query.GetPaginatedListAsync<ContactEntity, Contact>(pagination);
        }

        public async Task<PaginatedList<Contact>> ListByClient(IIdentifiable<long> clientId, Pagination pagination)
        {
            ValidateIdentifiable(clientId);

            var query = GetQueryable().Where(c => c.ClientId == clientId.Id);

            return await query.GetPaginatedListAsync<ContactEntity, Contact>(pagination);
        }

        public async Task<Contact> Save(Contact request)
        {
            var entity = new ContactEntity(request);

            GetWritable().CreateOrUpdateLong(entity);

            await SaveChangesAsync();

            ValidateEntity(entity);

            return entity.MapTo();
        }

        public async Task<bool> Delete(IIdentifiable<long> identifiable, int clientId)
        {
            ValidateIdentifiable(identifiable);

            var entity = await GetQueryable().FirstOrDefaultAsync(c =>
                c.Id == identifiable.Id && c.ClientId == clientId);

            if (entity == null)
                throw new RepositoryException((int)EBaseError.ENTITY_NOT_FOUND, BaseErrors.EntityNotFound);

            GetWritable().Remove(entity);

            return await SaveChangesAsync() > 0;
        }
    }
}