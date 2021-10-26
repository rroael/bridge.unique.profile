using Bridge.Commons.System.EntityFramework.Bases.Audits;
using Bridge.Unique.Profile.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bridge.Unique.Profile.Postgres.Maps
{
    public class UserApiClientMap : BaseAuditMap<UserApiClientEntity, long>
    {
        public override void Configure(EntityTypeBuilder<UserApiClientEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("UserApiClient");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                .UseIdentityAlwaysColumn();

            builder.Property(f => f.UserId)
                .IsRequired();

            builder.Property(f => f.ApiClientId)
                .IsRequired();

            builder.Property(f => f.ProfileId)
                .IsRequired();

            builder.Property(f => f.TermsAcceptanceDate)
                .IsRequired(false);

            builder.Property(f => f.Active)
                .HasDefaultValue(true);

            builder.Property(f => f.IsExternalApproved)
                .HasDefaultValue(true);

            builder.Property(f => f.AccessValidationToken);

            builder.HasOne(f => f.User)
                .WithMany(f => f.ApiClients)
                .HasForeignKey(f => f.UserId);

            builder.HasOne(f => f.ApiClient)
                .WithMany(f => f.Users)
                .HasForeignKey(f => f.ApiClientId);
        }
    }
}