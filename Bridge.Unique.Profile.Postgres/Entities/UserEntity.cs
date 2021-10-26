using System;
using System.Collections.Generic;
using System.Linq;
using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Commons.System.EntityFramework.Bases.Audits;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.System.Enums;

namespace Bridge.Unique.Profile.Postgres.Entities
{
    public class UserEntity : BaseAuditEntity<int>, IObjectMapper<User, UserEntity>
    {
        public UserEntity()
        {
        }

        public UserEntity(User input)
        {
            MapFrom(input);
        }

        public string Document { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte Gender { get; set; }
        public bool IsEmailValidated { get; set; }
        public bool IsPhoneNumberValidated { get; set; }
        public string EmailValidationToken { get; set; }
        public string SmsValidationCode { get; set; }
        public bool? Active { get; set; }
        public ICollection<UserAddressEntity> Addresses { get; set; } = new List<UserAddressEntity>();
        public ICollection<UserLoginEntity> UserLogins { get; set; } = new List<UserLoginEntity>();
        public ICollection<UserApiClientEntity> ApiClients { get; set; } = new List<UserApiClientEntity>();

        public User MapTo()
        {
            return new User
            {
                Id = Id,
                Active = Active ?? true,
                Name = Name,
                PhoneNumber = PhoneNumber,
                Email = Email,
                CreateDate = CreateDate,
                Document = Document,
                Gender = (EGender)Gender,
                Password = Password,
                BirthDate = BirthDate,
                ImageUrl = ImageUrl,
                ProfileId = ApiClients.FirstOrDefault()?.ProfileId ?? 0,
                UserName = UserName,
                IsEmailValidated = IsEmailValidated,
                IsPhoneNumberValidated = IsPhoneNumberValidated,
                EmailValidationToken = EmailValidationToken,
                SmsValidationCode = SmsValidationCode,
                TermsAcceptanceDate = ApiClients.FirstOrDefault()?.TermsAcceptanceDate,
                ApiClients = ApiClients?.Select(s => s.MapTo()).ToList(),
                UserLogins = UserLogins?.Select(s => s.MapTo()).ToList(),
                ApiClientId = ApiClients.FirstOrDefault()?.ApiClientId ?? 0,
                IsExternalApproved = ApiClients.FirstOrDefault()?.IsExternalApproved ?? false,
                Addresses = Addresses?.Select(a => a.MapToOnlyAddress()).ToList()
            };
        }

        public UserEntity MapFrom(User input)
        {
            Id = input.Id;
            Active = input.Active;
            Name = input.Name;
            PhoneNumber = input.PhoneNumber;
            Email = input.Email;
            CreateDate = input.CreateDate;
            Document = input.Document;
            Gender = (byte)input.Gender;
            Password = input.Password;
            BirthDate = input.BirthDate;
            ImageUrl = input.ImageUrl;
            UserName = input.UserName;
            IsEmailValidated = input.IsEmailValidated;
            IsPhoneNumberValidated = input.IsPhoneNumberValidated;
            EmailValidationToken = input.EmailValidationToken;
            SmsValidationCode = input.SmsValidationCode;

            ApiClients.Add(new UserApiClientEntity
            {
                ProfileId = input.ProfileId,
                ApiClientId = input.ApiClientId,
                TermsAcceptanceDate = input.TermsAcceptanceDate,
                IsExternalApproved = input.IsExternalApproved,
                AccessValidationToken = input.AccessValidationToken
            });

            UserLogins.Add(new UserLoginEntity
            {
                Provider = (byte)input.Provider,
                UserId = input.Id,
                ProviderUserId = input.ProviderUserId ?? "0"
            });

            return this;
        }
    }
}