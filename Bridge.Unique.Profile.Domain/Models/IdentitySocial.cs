using System;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Models;
using Bridge.Unique.Profile.Domain.Models.Providers;
using Bridge.Unique.Profile.System.Enums;

namespace Bridge.Unique.Profile.Domain.Models
{
    public class IdentitySocial : BaseModel<int>, IBaseModel
    {
        public int ApiClientId { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public int ProfileId { get; set; }
        public bool HasUserName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? BirthDate { get; set; }
        public EGender Gender { get; set; }

        public Provider Provider { get; set; }
    }
}