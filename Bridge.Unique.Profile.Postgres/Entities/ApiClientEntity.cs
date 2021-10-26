using System;
using System.Collections.Generic;
using System.Linq;
using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Commons.System.EntityFramework.Bases.Audits;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Postgres.Entities
{
    public class ApiClientEntity : BaseAuditEntity<int>, IObjectMapper<ApiClient, ApiClientEntity>
    {
        public ApiClientEntity()
        {
        }

        public ApiClientEntity(ApiClient input)
        {
            MapFrom(input);
        }

        public string Code { get; set; }
        public string Token { get; set; }
        public int ApiId { get; set; }
        public int ClientId { get; set; }
        public bool IsAdmin { get; set; }
        public string Sender { get; set; }
        public bool? Active { get; set; }
        public string ApiKeyId { get; set; }
        public bool NeedsExternalApproval { get; set; }
        public string ExternalApproversEmail { get; set; }

        public ApiEntity Api { get; set; }
        public ClientEntity Client { get; set; }
        public ICollection<UserApiClientEntity> Users { get; set; }

        public ApiClientEntity MapFrom(ApiClient input)
        {
            Id = input.Id;
            Token = input.Token;
            ApiId = input.ApiId;
            ClientId = input.ClientId;
            IsAdmin = input.IsAdmin;
            Active = input.Active;
            Sender = input.Sender;
            Code = input.Code;
            ApiKeyId = input.ApiKeyId;
            NeedsExternalApproval = input.NeedsExternalApproval;
            ExternalApproversEmail = input.ExternalApproversEmail;

            return this;
        }

        public ApiClient MapTo()
        {
            return new ApiClient
            {
                Id = Id,
                Api = Api != null
                    ? new Api
                    {
                        Id = Api.Id,
                        Active = Api.Active ?? true,
                        Name = Api.Name,
                        AccessTokenTtl = Api.AccessTokenTtl,
                        RefreshTokenTtl = Api.RefreshTokenTtl,
                        Description = Api.Description,
                        CreateDate = Api.CreateDate,
                        Code = Api.Code
                    }
                    : null,
                Token = string.Empty,
                Active = Active ?? true,
                Client = Client != null
                    ? new Client
                    {
                        Id = Client.Id,
                        Active = Client.Active ?? true,
                        Code = Client.Code,
                        Name = Client.Name,
                        Description = Client.Description,
                        Contacts = Client.Contacts?.Select(s => s.MapTo()).ToList()
                    }
                    : null,
                ApiId = ApiId,
                ClientId = ClientId,
                IsAdmin = IsAdmin,
                Sender = Sender,
                CreateDate = CreateDate ?? new DateTime(),
                UpdateDate = UpdateDate,
                Code = Code,
                ApiKeyId = ApiKeyId,
                NeedsExternalApproval = NeedsExternalApproval,
                ExternalApproversEmail = ExternalApproversEmail,
                ApplicationToken = Token
            };
        }
    }
}