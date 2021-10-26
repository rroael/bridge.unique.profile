using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Commons.System.Models;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Postgres.Entities
{
    public class UserAddressEntity : IdentifiableLong, IObjectMapper<UserAddress, UserAddressEntity>
    {
        public int UserId { get; set; }
        public long AddressId { get; set; }

        public UserEntity User { get; set; }
        public AddressEntity Address { get; set; }

        public UserAddress MapTo()
        {
            return new UserAddress
            {
                User = User.MapTo(),
                Id = Id,
                Address = Address.MapTo(),
                UserId = UserId,
                AddressId = AddressId
            };
        }

        public UserAddressEntity MapFrom(UserAddress input)
        {
            User = new UserEntity(input.User);
            Id = input.Id;
            Address = new AddressEntity(input.Address);
            UserId = input.UserId;
            AddressId = input.AddressId;

            return this;
        }

        public UserAddress MapToOnlyAddress()
        {
            return new UserAddress
            {
                Id = Id,
                Address = Address.MapTo(),
                UserId = UserId,
                AddressId = AddressId
            };
        }
    }
}