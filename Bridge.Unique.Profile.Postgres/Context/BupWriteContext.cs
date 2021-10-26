using Bridge.Commons.System.EntityFramework.Bases.Contexts;
using Bridge.Unique.Profile.Domain.Contexts.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bridge.Unique.Profile.Postgres.Context
{
    public class BupWriteContext : BaseWriteContext, IBupWriteContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public BupWriteContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Logging
            optionsBuilder.UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ContextConfiguration.OnModelCreatingConfigure(modelBuilder);
        }
    }
}