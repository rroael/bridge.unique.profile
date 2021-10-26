using System.Collections.Generic;
using System.Linq;
using Bridge.Commons.Location.Models;
using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Commons.System.EntityFramework.Bases.Audits;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Postgres.Entities
{
    public class AddressEntity : BaseAuditEntity<long>, IObjectMapper<Address, AddressEntity>
    {
        public AddressEntity()
        {
        }

        public AddressEntity(Address input)
        {
            MapFrom(input);
        }

        public string Nickname { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Complement { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int AddressTypes { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool? Active { get; set; }

        public ICollection<UserAddressEntity> UserAddresses { get; set; } = new List<UserAddressEntity>();

        public Address MapTo()
        {
            return new Address
            {
                Id = Id,
                Active = Active ?? true,
                City = City,
                Complement = Complement,
                Country = Country,
                Neighborhood = Neighborhood,
                Nickname = Nickname,
                State = State,
                Street = Street,
                AddressTypes = AddressTypes,
                StreetNumber = StreetNumber,
                ZipCode = ZipCode,
                Location = new LocationPoint(Latitude, Longitude),
                Email = Email,
                Name = Name,
                PhoneNumber = PhoneNumber
            };
        }

        public AddressEntity MapFrom(Address input)
        {
            Id = input.Id;
            Active = input.Active;
            City = input.City;
            Complement = input.Complement;
            Country = input.Country;
            Neighborhood = input.Neighborhood;
            Nickname = input.Nickname;
            State = input.State;
            Street = input.Street;
            AddressTypes = input.AddressTypes;
            StreetNumber = input.StreetNumber;
            ZipCode = input.ZipCode;
            Latitude = input.Location?.Latitude ?? 0;
            Longitude = input.Location?.Longitude ?? 0;
            Email = input.Email;
            Name = input.Name;
            PhoneNumber = input.PhoneNumber;

            if (input.UserAddresses == null) return this;

            if (Id != 0) return this;

            foreach (var ua in input.UserAddresses.Where(ua => ua.UserId != 0))
                UserAddresses.Add(new UserAddressEntity { UserId = ua.UserId });

            return this;
        }
    }
}