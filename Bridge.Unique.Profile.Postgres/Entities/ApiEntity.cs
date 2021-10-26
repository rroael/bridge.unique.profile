using System;
using System.Collections.Generic;
using System.Linq;
using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Commons.System.EntityFramework.Bases.Audits;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Postgres.Entities
{
    public class ApiEntity : BaseAuditEntity<int>, IObjectMapper<Api, ApiEntity>
    {
        public ApiEntity()
        {
        }

        public ApiEntity(Api input)
        {
            MapFrom(input);
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan AccessTokenTtl { get; set; }
        public TimeSpan RefreshTokenTtl { get; set; }
        public bool? Active { get; set; }

        public ICollection<ApiClientEntity> Clients { get; set; }

        public Api MapTo()
        {
            return new Api
            {
                Id = Id,
                Active = Active ?? true,
                Name = Name,
                AccessTokenTtl = AccessTokenTtl,
                RefreshTokenTtl = RefreshTokenTtl,
                Description = Description,
                CreateDate = CreateDate,
                Clients = Clients?.Select(s => s.MapTo()).ToList(),
                Code = Code
            };
        }

        public ApiEntity MapFrom(Api input)
        {
            Id = input.Id;
            Active = input.Active;
            Name = input.Name;
            Description = input.Description;
            AccessTokenTtl = input.AccessTokenTtl;
            RefreshTokenTtl = input.RefreshTokenTtl;
            CreateDate = input.CreateDate;
            Code = input.Code;

            return this;
        }
    }
}