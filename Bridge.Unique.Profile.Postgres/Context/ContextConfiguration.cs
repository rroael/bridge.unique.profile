using Bridge.Unique.Profile.Postgres.Maps;
using Microsoft.EntityFrameworkCore;

namespace Bridge.Unique.Profile.Postgres.Context
{
    public static class ContextConfiguration
    {
        public static void OnModelCreatingConfigure(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new ContactMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserAddressMap());
            modelBuilder.ApplyConfiguration(new UserLoginMap());
            modelBuilder.ApplyConfiguration(new ApiMap());
            modelBuilder.ApplyConfiguration(new ApiClientMap());
            modelBuilder.ApplyConfiguration(new UserApiClientMap());
        }
    }
}