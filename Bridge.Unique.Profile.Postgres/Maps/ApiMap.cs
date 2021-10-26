using Bridge.Commons.System.EntityFramework.Bases.Audits;
using Bridge.Unique.Profile.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bridge.Unique.Profile.Postgres.Maps
{
    public class ApiMap : BaseAuditMap<ApiEntity, int>
    {
        public override void Configure(EntityTypeBuilder<ApiEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Api");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                .UseIdentityAlwaysColumn();

            builder.Property(f => f.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(f => f.Description)
                .HasMaxLength(254)
                .IsRequired();

            builder.Property(f => f.AccessTokenTtl)
                .IsRequired();

            builder.Property(f => f.RefreshTokenTtl)
                .IsRequired();

            builder.Property(f => f.Active)
                .HasDefaultValue(true);

            builder.Property(f => f.Code)
                .HasMaxLength(3)
                .IsRequired();

            builder.HasIndex(f => f.Code)
                .IsUnique();

            builder.HasMany(f => f.Clients)
                .WithOne(f => f.Api);
        }
    }
}