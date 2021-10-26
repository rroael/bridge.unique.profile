using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Postgres.Entities
{
    public class UserLoginEntity : IObjectMapper<UserLogin, UserLoginEntity>
    {
        public UserLoginEntity()
        {
        }

        public UserLoginEntity(UserLogin input)
        {
            MapFrom(input);
        }

        public byte Provider { get; set; }
        public string ProviderUserId { get; set; }
        public int UserId { get; set; }

        public UserEntity User { get; set; }

        public UserLoginEntity MapFrom(UserLogin input)
        {
            Provider = (byte)input.Provider;
            ProviderUserId = input.ProviderUserId;
            UserId = input.UserId;

            return this;
        }

        public UserLogin MapTo()
        {
            return new UserLogin
            {
                Provider = (EAuthProvider)Provider,
                ProviderUserId = ProviderUserId,
                UserId = UserId
            };
        }
    }
}