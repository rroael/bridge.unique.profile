using Bridge.Commons.System.Contracts;
using Bridge.Unique.Profile.Communication.Enums;

namespace Bridge.Unique.Profile.Domain.Models
{
    public class UserLogin : IBaseModel
    {
        public EAuthProvider Provider { get; set; }
        public string ProviderUserId { get; set; }
        public int UserId { get; set; }
    }
}