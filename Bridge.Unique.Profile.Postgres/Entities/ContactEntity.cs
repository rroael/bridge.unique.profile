using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Commons.System.EntityFramework.Bases.Audits;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.System.Enums;

namespace Bridge.Unique.Profile.Postgres.Entities
{
    public class ContactEntity : BaseAuditEntity<long>, IObjectMapper<Contact, ContactEntity>
    {
        public ContactEntity()
        {
        }

        public ContactEntity(Contact input)
        {
            MapFrom(input);
        }

        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Document { get; set; }
        public byte ContactType { get; set; }
        public bool? Active { get; set; }

        public ClientEntity Client { get; set; }

        public Contact MapTo()
        {
            return new Contact
            {
                Id = Id,
                ClientId = ClientId,
                Name = Name,
                PhoneNumber = PhoneNumber,
                Email = Email,
                ContactType = (EContactType)ContactType,
                Document = Document
            };
        }

        public ContactEntity MapFrom(Contact input)
        {
            Id = input.Id;
            ClientId = input.ClientId;
            Name = input.Name;
            PhoneNumber = input.PhoneNumber;
            Email = input.Email;
            ContactType = (byte)input.ContactType;
            Document = input.Document;

            return this;
        }
    }
}