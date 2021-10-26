using System;
using System.Collections.Generic;
using Bridge.Unique.Profile.System.Enums;

namespace Bridge.Unique.Profile.Domain.Models
{
    public class User : Identity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? BirthDate { get; set; }
        public EGender Gender { get; set; }
        public bool HasUserName { get; set; }
        public bool IsEmailValidated { get; set; }
        public bool IsPhoneNumberValidated { get; set; }
        public string EmailValidationToken { get; set; }
        public string SmsValidationCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public int ProfileId { get; set; }
        public bool Active { get; set; }
        public Token Token { get; set; }
        public DateTime? TermsAcceptanceDate { get; set; }
        public bool IsExternalApproved { get; set; }
        public string AccessValidationToken { get; set; }

        //Aux
        public bool EmailChanging { get; set; }
        public bool PhoneChanging { get; set; }

        //public List<UserApplication> Applications { get; set; } = new List<UserApplication>();
        public List<UserAddress> Addresses { get; set; } = new List<UserAddress>();
        public List<UserApiClient> ApiClients { get; set; } = new List<UserApiClient>();
        public List<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
    }
}