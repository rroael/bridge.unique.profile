using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Models;
using Bridge.Unique.Profile.Communication.Enums;

namespace Bridge.Unique.Profile.Domain.Models
{
    public class Identity : BaseModel<int>, IBaseModel
    {
        public string ApiCode { get; set; }
        public int ApiClientId { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public EAuthProvider Provider { get; set; } = EAuthProvider.BUP;
        public string ProviderUserId { get; set; } = "0";
        public string ProviderToken { get; set; }
    }
}