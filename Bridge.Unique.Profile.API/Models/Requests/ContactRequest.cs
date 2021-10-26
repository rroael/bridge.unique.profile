using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.In;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.System.Enums;

namespace Bridge.Unique.Profile.API.Models.Requests
{
    /// <summary>
    ///     Requisição de contato
    /// </summary>
    public class ContactRequest : ContactIn, IToObjectMapper<Contact>
    {
        /// <summary>
        ///     Mapeia requisição para modelo
        /// </summary>
        /// <returns></returns>
        public Contact MapTo()
        {
            return new Contact
            {
                Id = Id,
                Document = Document,
                Email = Email,
                Name = Name,
                ApplicationToken = ApplicationToken,
                ClientId = ClientId,
                ContactType = (EContactType)ContactType,
                PhoneNumber = PhoneNumber
            };
        }
    }
}