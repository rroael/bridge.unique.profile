using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Models;

namespace Bridge.Unique.Profile.Domain.Models
{
    public class UserAddress : BaseModel<long>, IBaseModel
    {
        public int UserId { get; set; }
        public long AddressId { get; set; }

        public User User { get; set; }
        public Address Address { get; set; }
    }
}