using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Models;
using Bridge.Unique.Profile.System.Enums;

namespace Bridge.Unique.Profile.Domain.Models
{
    public class Contact : BaseModel<long>, IBaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string PhoneNumber { get; set; }
        public int ClientId { get; set; }
        public EContactType ContactType { get; set; }

        public Client Client { get; set; }
    }
}