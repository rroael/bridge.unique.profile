using System.Collections.Generic;
using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.In.Addresses;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Requests
{
    /// <summary>
    ///     Requisição de endereço
    /// </summary>
    public class AddressRequest : AddressIn, IToObjectMapper<Address>
    {
        /// <summary>
        ///     Mapeia requisição para modelo
        /// </summary>
        /// <returns></returns>
        public Address MapTo()
        {
            return new Address
            {
                City = City,
                State = State,
                Active = Active,
                Street = Street,
                Country = Country,
                Nickname = Nickname,
                Neighborhood = Neighborhood,
                StreetNumber = StreetNumber,
                Complement = Complement,
                AddressTypes = AddressTypes,
                ZipCode = ZipCode,
                Location = Location,
                Name = Name,
                Email = Email,
                PhoneNumber = PhoneNumber,
                UserAddresses = new List<UserAddress>
                {
                    new()
                    {
                        UserId = UserId
                    }
                }
            };
        }
    }
}