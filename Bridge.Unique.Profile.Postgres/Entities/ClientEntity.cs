using System.Collections.Generic;
using System.Linq;
using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Commons.System.EntityFramework.Bases.Audits;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Postgres.Entities
{
    public class ClientEntity : BaseAuditEntity<int>, IObjectMapper<Client, ClientEntity>
    {
        public ClientEntity()
        {
        }

        public ClientEntity(Client input)
        {
            MapFrom(input);
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Document { get; set; }
        public string Segment { get; set; }
        public bool? Active { get; set; }

        public ICollection<ContactEntity> Contacts { get; set; }
        public ICollection<ApiClientEntity> Apis { get; set; }

        public Client MapTo()
        {
            var result = new Client
            {
                Id = Id,
                Active = Active ?? true,
                Code = Code,
                Name = Name,
                Description = Description,
                Document = Document,
                Segment = Segment,
                Contacts = Contacts?.Select(s => s.MapTo()).ToList(),
                Apis = Apis?.Select(s => s.MapTo()).ToList()
            };

            result.Contacts ??= new List<Contact>();
            result.Apis ??= new List<ApiClient>();

            return result;
        }

        public ClientEntity MapFrom(Client input)
        {
            Id = input.Id;
            Active = input.Active;
            Code = input.Code;
            Name = input.Name;
            Description = input.Description;
            Document = input.Document;
            Segment = input.Segment;

            return this;
        }
    }
}