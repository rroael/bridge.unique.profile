using System.Linq;
using System.Threading.Tasks;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.EntityFramework.Bases.Repositories;
using Bridge.Commons.System.EntityFramework.Extensions;
using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Exceptions;
using Bridge.Commons.System.Resources;
using Bridge.Unique.Profile.Domain.Contexts.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;
using Bridge.Unique.Profile.Postgres.Context;
using Bridge.Unique.Profile.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bridge.Unique.Profile.Postgres.Repositories
{
    public class ApiClientRepository :
        BaseRepository<BupReadContext, BupWriteContext, ApiClientEntity, IIdentifiable<int>, int>, IApiClientRepository
    {
        public ApiClientRepository(IBupReadContext bupReadContext, IBupWriteContext bupWriteContext)
        {
            Init((BupReadContext)bupReadContext, (BupWriteContext)bupWriteContext);
        }

        public async Task<ApiClient> Get(string auth)
        {
            var entity = await GetQueryable()
                .Include(i => i.Api)
                .Include(i => i.Client)
                .SingleOrDefaultAsync(x => x.Token.Equals(auth));

            return entity?.MapTo();
        }

        public async Task<ApiClient> GetByUser(string apiCode, string email)
        {
            var entity = await (from ac in GetQueryable()
                    join a in GetQueryable<ApiEntity>() on ac.ApiId equals a.Id
                    join ua in GetQueryable<UserApiClientEntity>() on ac.Id equals ua.ApiClientId
                    join u in GetQueryable<UserEntity>() on ua.UserId equals u.Id
                    where a.Code.Equals(apiCode) &&
                          u.Email == email
                    select ac)
                .Include(i => i.Api)
                .Include(i => i.Client)
                .SingleOrDefaultAsync();

            ValidateEntity(entity);

            return entity.MapTo();
        }

        public async Task<ApiClient> Get(string apiCode, int clientId)
        {
            var entity = await (from ac in GetQueryable()
                    join a in GetQueryable<ApiEntity>() on ac.ApiId equals a.Id
                    where a.Code.Equals(apiCode) &&
                          ac.ClientId == clientId
                    select ac)
                .Include(i => i.Api)
                .Include(i => i.Client)
                .SingleOrDefaultAsync();

            ValidateEntity(entity);

            return entity.MapTo();
        }

        public async Task<int> GetApiClientId(string apiCode, int clientId)
        {
            var entityId = await (from ac in GetQueryable()
                    join a in GetQueryable<ApiEntity>() on ac.ApiId equals a.Id
                    where a.Code.Equals(apiCode) &&
                          ac.ClientId == clientId
                    select ac.Id)
                .SingleOrDefaultAsync();

            if (entityId == 0)
                throw new RepositoryException((int)EBaseError.ENTITY_NOT_FOUND, BaseErrors.EntityNotFound);

            return entityId;
        }

        public async Task<ApiClient> Save(ApiClient request)
        {
            var entity = new ApiClientEntity(request);

            GetWritable().CreateOrUpdate(entity);

            await SaveChangesAsync();

            ValidateEntity(entity);

            return entity.MapTo();
        }
    }
}