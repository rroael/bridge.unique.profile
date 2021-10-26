using System;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Models;

namespace Bridge.Unique.Profile.Domain.Models
{
    public class UserApiClient : BaseModel<long>, IBaseModel
    {
        public int UserId { get; set; }
        public int ApiClientId { get; set; }
        public int ProfileId { get; set; }
        public DateTime? TermsAcceptanceDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Active { get; set; }
        public User User { get; set; }
        public ApiClient ApiClient { get; set; }
    }
}