using Bridge.Commons.System.AspNet.Settings;
using Bridge.Unique.Profile.Postgres.Context;
using Bridge.Unique.Profile.System.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging.Abstractions;

namespace Bridge.Unique.Profile.Postgres.Factories
{
    /// <summary>
    ///     Factory para criação de contexto de banco de dados
    /// </summary>
    public class BupContextFactory : IDesignTimeDbContextFactory<BupWriteContext>
    {
        /// <summary>
        ///     Cria o contexto do banco de dados
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public BupWriteContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BupWriteContext>();
            var appSettings = new DotNetSettings().GetAppSettings<AppSettings>();

            optionsBuilder.UseNpgsql(appSettings.ConnectionStrings.BUPWriteContext);

            return new BupWriteContext(optionsBuilder.Options, new NullLoggerFactory());
        }
    }
}