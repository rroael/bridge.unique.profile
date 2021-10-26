using System;
using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Commons.System.EntityFramework.Bases.Audits;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.System.Enums;

namespace Bridge.Unique.Profile.Postgres.Entities
{
    public class UserApiClientEntity : BaseAuditEntity<long>, IObjectMapper<UserApiClient, UserApiClientEntity>
    {
        public int UserId { get; set; }
        public int ApiClientId { get; set; }
        public int ProfileId { get; set; }
        public DateTime? TermsAcceptanceDate { get; set; }
        public bool? Active { get; set; }
        public bool? IsExternalApproved { get; set; }
        public string AccessValidationToken { get; set; }

        public UserEntity User { get; set; }
        public ApiClientEntity ApiClient { get; set; }

        public UserApiClientEntity MapFrom(UserApiClient input)
        {
            Id = input.Id;
            ApiClientId = input.ApiClientId;
            UserId = input.UserId;
            ProfileId = input.ProfileId;
            TermsAcceptanceDate = input.TermsAcceptanceDate;
            Active = input.Active;

            return this;
        }

        public UserApiClient MapTo()
        {
            return new UserApiClient
            {
                Id = Id,
                Active = Active ?? true,
                ApiClientId = ApiClientId,
                ApiClient = ApiClient?.MapTo(),
                UserId = UserId,
                ProfileId = ProfileId,
                CreateDate = CreateDate,
                UpdateDate = UpdateDate,
                TermsAcceptanceDate = TermsAcceptanceDate,
                User = new User
                {
                    Document = User.Document,
                    Email = User.Email,
                    Name = User.Name,
                    Id = User.Id,
                    Gender = (EGender)User.Gender,
                    BirthDate = User.BirthDate,
                    PhoneNumber = User.PhoneNumber,
                    UserName = User.UserName
                }
            };
        }
    }
}