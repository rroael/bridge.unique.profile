using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.EntityFramework.Bases.Repositories;
using Bridge.Commons.System.EntityFramework.Extensions;
using Bridge.Commons.System.Exceptions;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Resources;
using Bridge.Unique.Profile.Domain.Contexts.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;
using Bridge.Unique.Profile.Postgres.Context;
using Bridge.Unique.Profile.Postgres.Entities;
using Bridge.Unique.Profile.System.Enums;
using Microsoft.EntityFrameworkCore;

namespace Bridge.Unique.Profile.Postgres.Repositories
{
    public class AddressRepository :
        BaseRepository<BupReadContext, BupWriteContext, AddressEntity, IIdentifiable<long>, long>, IAddressRepository
    {
        public AddressRepository(IBupReadContext bupReadContext, IBupWriteContext bupWriteContext)
        {
            Init((BupReadContext)bupReadContext, (BupWriteContext)bupWriteContext);
        }

        public async Task<Address> Get(IIdentifiable<long> identifiable)
        {
            var entity = await GetAndValidate(identifiable.Id, 0);
            return entity.MapTo();
        }

        public async Task<Address> Get(IIdentifiable<long> identifiable, int userId)
        {
            var entity = await GetAndValidate(identifiable.Id, userId);
            return entity.MapTo();
        }

        public async Task<PaginatedList<Address>> List(IEnumerable<long> ids, Pagination pagination)
        {
            var query = GetQueryable();

            if (ids != null)
                query = query.Where(x => ids.Contains(x.Id));

            return await query.OrderBy(o => o.Nickname).ThenByDescending(od => od.CreateDate)
                .GetPaginatedListAsync<AddressEntity, Address>(pagination);
        }

        public async Task<PaginatedList<Address>> ListByUser(IIdentifiable<long> userId, Pagination pagination)
        {
            ValidateIdentifiable(userId);

            var query =
                from a in GetQueryable()
                join ua in GetQueryable<UserAddressEntity>() on a.Id equals ua.AddressId
                orderby a.Neighborhood, a.CreateDate descending
                where ua.UserId == userId.Id
                select a;

            return await query.GetPaginatedListAsync<AddressEntity, Address>(pagination);
        }

        public async Task<Address> Save(Address request)
        {
            var userId = request.UserAddresses?.First().UserId;
            var addresses = GetWritable();

            if ((request.AddressTypes & (int)EAddressType.DEFAULT) > 0 && userId != null)
            {
                var list = await (from a in addresses
                        join ua in GetWritable<UserAddressEntity>() on a.Id equals ua.AddressId
                        where (a.AddressTypes & (int)EAddressType.DEFAULT) > 0
                              && a.Id != request.Id
                              && ua.UserId == userId.Value
                        select a
                    ).ToListAsync();

                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                        item.AddressTypes -= (int)EAddressType.DEFAULT;

                    addresses.UpdateRange(list);
                }
            }

            if (request.Id > 0 && userId != null)
                await GetAndValidate(request.Id, userId.Value);

            var entity = new AddressEntity(request);

            addresses.CreateOrUpdateLong(entity);

            await SaveChangesAsync();

            ValidateEntity(entity);

            return entity.MapTo();
        }

        public async Task<bool> Delete(IIdentifiable<long> identifiable, int userId)
        {
            var entity = await GetAndValidate(identifiable.Id, userId);

            GetWritable().Remove(entity);

            return await SaveChangesAsync() > 0;
        }

        private async Task<AddressEntity> GetAndValidate(long id, int userId)
        {
            var identifiable = new IdentifiableLong(id);
            ValidateIdentifiable(identifiable);

            IQueryable<AddressEntity> query;

            if (userId <= 0)
                query = from a in GetQueryable()
                    where a.Id == id
                    select a;
            else
                query = from a in GetQueryable()
                    join ua in GetQueryable<UserAddressEntity>() on a.Id equals ua.AddressId
                    where a.Id == id && ua.UserId == userId
                    select a;

            var entity = await query.SingleOrDefaultAsync();

            if (entity == null)
                throw new NotFoundException((int)EError.ADDRESS_NOT_FOUND, Errors.AddressNotFound);

            return entity;
        }
    }
}