using Bridge.Commons.System.EntityFramework.Bases.Contexts;
using Bridge.Unique.Profile.Domain.Contexts.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bridge.Unique.Profile.Postgres.Context
{
    public class BupReadContext : BaseReadOnlyContext, IBupReadContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public BupReadContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ContextConfiguration.OnModelCreatingConfigure(modelBuilder);
        }
    }
}