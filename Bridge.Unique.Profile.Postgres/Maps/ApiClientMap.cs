using Bridge.Commons.System.EntityFramework.Bases.Audits;
using Bridge.Unique.Profile.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bridge.Unique.Profile.Postgres.Maps
{
    public class ApiClientMap : BaseAuditMap<ApiClientEntity, int>
    {
        public override void Configure(EntityTypeBuilder<ApiClientEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("ApiClient");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                .UseIdentityAlwaysColumn();

            builder.Property(f => f.Token)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasIndex(f => f.Token)
                .IsUnique();

            builder.Property(f => f.Active)
                .HasDefaultValue(true);

            builder.Property(f => f.ApiId)
                .IsRequired();

            builder.Property(f => f.ClientId)
                .IsRequired();

            builder.Property(f => f.IsAdmin)
                .IsRequired();

            builder.Property(f => f.Sender)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(f => f.Code)
                .HasMaxLength(7)
                .IsRequired();

            builder.HasIndex(f => f.Code)
                .IsUnique();

            builder.Property(f => f.ApiKeyId)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(f => new { f.ApiId, f.ClientId })
                .IsUnique();

            builder.Property(f => f.NeedsExternalApproval)
                .HasDefaultValue(false);

            builder.Property(f => f.ExternalApproversEmail)
                .HasMaxLength(254);

            builder.HasOne(f => f.Api)
                .WithMany(f => f.Clients)
                .HasForeignKey(f => f.ApiId);

            builder.HasOne(f => f.Client)
                .WithMany(f => f.Apis)
                .HasForeignKey(f => f.ClientId);
        }
    }
}