using System.Collections.Generic;
using Bridge.Commons.Location.Models;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Models;

namespace Bridge.Unique.Profile.Domain.Models
{
    public class Address : BaseModel<long>, IBaseModel
    {
        public string Nickname { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Neighborhood { get; set; } //District
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Complement { get; set; }
        public int AddressTypes { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public LocationPoint Location { get; set; }
        public bool Active { get; set; }

        public List<UserAddress> UserAddresses { get; set; }
    }
}