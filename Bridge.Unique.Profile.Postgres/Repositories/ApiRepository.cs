using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.EntityFramework.Bases.Repositories;
using Bridge.Commons.System.EntityFramework.Extensions;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Unique.Profile.Domain.Contexts.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;
using Bridge.Unique.Profile.Postgres.Context;
using Bridge.Unique.Profile.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bridge.Unique.Profile.Postgres.Repositories
{
    public class ApiRepository : BaseRepository<BupReadContext, BupWriteContext, ApiEntity, IIdentifiable<int>, int>,
        IApiRepository
    {
        public ApiRepository(IBupReadContext bupReadContext, IBupWriteContext bupWriteContext)
        {
            Init((BupReadContext)bupReadContext, (BupWriteContext)bupWriteContext);
        }

        public async Task<Api> Authenticate(string auth)
        {
            var entity = await GetQueryable<ApiClientEntity>()
                .Include(i => i.Api)
                .FirstOrDefaultAsync(x => x.Token.Equals(auth));

            ValidateEntity(entity);

            return entity.Api.MapTo();
        }

        public async Task<PaginatedList<Api>> List(Pagination pagination)
        {
            return await GetQueryable().GetPaginatedListAsync<ApiEntity, Api>(pagination);
        }

        public async Task<Api> Get(IIdentifiable<int> identifiable)
        {
            ValidateIdentifiable(identifiable);

            var entity = await GetByIdentifiableAsync(identifiable);

            return entity.MapTo();
        }

        public async Task<List<Api>> GetByCodes(List<string> codes)
        {
            var entity = await GetQueryable().Where(w => codes.Contains(w.Code)).ToListAsync();

            return entity?.Select(s => s.MapTo()).ToList();
        }

        public async Task<Api> Get(string auth)
        {
            var entity = await GetQueryable<ApiClientEntity>()
                .Include(i => i.Api)
                .SingleOrDefaultAsync(x => x.Token.Equals(auth));

            ValidateEntity(entity);

            return entity.Api.MapTo();
        }

        public async Task<Api> Save(Api request)
        {
            var entity = new ApiEntity(request);

            GetWritable().CreateOrUpdate(entity);

            await SaveChangesAsync();

            ValidateEntity(entity);

            return entity.MapTo();
        }

        public async Task<Api> Delete(IIdentifiable<int> identifiable)
        {
            ValidateIdentifiable(identifiable);

            var entity = await GetByIdentifiableAsync(identifiable);

            GetWritable().Remove(entity);

            await SaveChangesAsync();

            return entity.MapTo();
        }
    }
}