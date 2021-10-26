using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Contracts.Filters;
using Bridge.Commons.System.EntityFramework.Bases.Repositories;
using Bridge.Commons.System.EntityFramework.Extensions;
using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Exceptions;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Commons.System.Resources;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.In.Filters;
using Bridge.Unique.Profile.Communication.Resources;
using Bridge.Unique.Profile.Domain.Contexts.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;
using Bridge.Unique.Profile.Postgres.Context;
using Bridge.Unique.Profile.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bridge.Unique.Profile.Postgres.Repositories
{
    public class ClientRepository :
        BaseRepository<BupReadContext, BupWriteContext, ClientEntity, IIdentifiable<int>, int>, IClientRepository
    {
        public ClientRepository(IBupReadContext bupReadContext, IBupWriteContext bupWriteContext)
        {
            Init((BupReadContext)bupReadContext, (BupWriteContext)bupWriteContext);
        }

        public async Task<PaginatedList<Client>> Filter(IFilterPagination request)
        {
            var pagination = (FilterPagination<ClientFilterIn>)request;
            var filter = pagination.Filters;

            var query = GetQueryable();

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Code))
                    query = query.Where(x => x.Code.ToLower().StartsWith(filter.Code.ToLower()));

                if (!string.IsNullOrWhiteSpace(filter.Name))
                    query = query.Where(x => x.Name.ToLower().StartsWith(filter.Name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(pagination.SortField))
                query = pagination.Order == ESortType.ASCENDING
                    ? query.OrderBy(pagination.SortField)
                    : query.OrderByDescending(pagination.SortField);
            else
                query = query.OrderBy(o => o.Id);

            return await query.GetPaginatedListAsync<ClientEntity, Client>(pagination);
        }

        public async Task<Client> Get(IIdentifiable<int> identifiable)
        {
            ValidateIdentifiable(identifiable);

            var id = identifiable.Id;

            var entity = await (
                    from c in GetQueryable()
                    where c.Id == id
                    select c
                ).Include(i => i.Apis)
                .ThenInclude(t => t.Api)
                .SingleOrDefaultAsync();

            return entity?.MapTo();
        }

        public async Task<Client> GetByCode(string code)
        {
            var entity = await (
                    from c in GetQueryable()
                    where c.Code == code
                    select c
                ).Include(i => i.Apis)
                .ThenInclude(t => t.Api)
                .SingleOrDefaultAsync();

            return entity?.MapTo();
        }

        public async Task<int> GetIdByCode(string code)
        {
            var entity = await (
                from c in GetQueryable<ApiClientEntity>()
                where c.Code == code
                select c.ClientId
            ).SingleOrDefaultAsync();

            return entity;
        }

        public async Task<PaginatedList<Client>> List(int apiId, Pagination pagination)
        {
            var query = GetQueryable()
                .Join(GetQueryable<ApiClientEntity>(), c => c.Id, ac => ac.ClientId,
                    (client, apiClient) => new { client, apiClient })
                .Where(x => x.apiClient.ApiId == apiId)
                .Select(x => x.client)
                .OrderBy(x => x.Id);

            if (!string.IsNullOrWhiteSpace(pagination.SortField))
                query = pagination.Order == ESortType.ASCENDING
                    ? query.OrderBy(pagination.SortField)
                    : query.OrderByDescending(pagination.SortField);

            return await query.GetPaginatedListAsync<ClientEntity, Client>(pagination);
        }

        public async Task<PaginatedList<Client>> List(Pagination pagination)
        {
            var query = GetQueryable()
                .Include(i => i.Apis)
                .ThenInclude(t => t.Api)
                .OrderBy(o => o.Id);

            if (!string.IsNullOrWhiteSpace(pagination.SortField))
                query = pagination.Order == ESortType.ASCENDING
                    ? query.OrderBy(pagination.SortField)
                    : query.OrderByDescending(pagination.SortField);

            return await query.GetPaginatedListAsync<ClientEntity, Client>(pagination);
        }

        public async Task<List<Client>> ListAll(int apiId)
        {
            return await (from c in GetQueryable()
                join ap in GetQueryable<ApiClientEntity>() on c.Id equals ap.ClientId
                orderby c.Id
                where ap.ApiId == apiId
                select c.MapTo()).ToListAsync();
        }

        public async Task<Client> Save(Client client)
        {
            if (client.Id == 0)
            {
                var entityExisting = await GetQueryable().SingleOrDefaultAsync(s => s.Code == client.Code);
                if (entityExisting != null)
                    throw new RepositoryException((int)EError.CLIENT_ALREADY_EXISTS, Errors.ClientAlreadyExists);
            }

            var entity = new ClientEntity(client);

            GetWritable().CreateOrUpdate(entity);

            await SaveChangesAsync();

            ValidateEntity(entity);

            return entity.MapTo();
        }

        public async Task<Client> ClientUpdate(Client client)
        {
            var entity = await GetByIdentifiableAsync(new IdentifiableInt(client.Id));
            if (entity == null)
                throw new RepositoryException((int)EBaseError.ENTITY_NOT_FOUND, BaseErrors.EntityNotFound);

            entity.Description = client.Description;
            entity.Document = client.Document;
            entity.Segment = client.Segment;
            entity.Name = client.Name;

            GetWritable().CreateOrUpdate(entity);

            await SaveChangesAsync();

            ValidateEntity(entity);

            return entity.MapTo();
        }

        public async Task<Client> Delete(IIdentifiable<int> identifiable)
        {
            ValidateIdentifiable(identifiable);

            var entity = await GetByIdentifiableAsync(identifiable);
            GetWritable().Remove(entity);

            await SaveChangesAsync();

            return entity.MapTo();
        }
    }
}