using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.Out;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Results
{
    /// <summary>
    ///     Resultado de contato
    /// </summary>
    public class ContactResult : ContactOut, IFromObjectMapper<Contact, ContactResult>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public ContactResult()
        {
        }

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="model"></param>
        public ContactResult(Contact model)
        {
            MapFrom(model);
        }

        /// <summary>
        ///     Mapeia modelo para resultado
        /// </summary>
        /// <param name="input">Modelo</param>
        /// <returns></returns>
        public ContactResult MapFrom(Contact input)
        {
            Id = input.Id;
            Name = input.Name;
            Email = input.Email;
            PhoneNumber = input.PhoneNumber;
            ContactType = (int)input.ContactType;
            Document = input.Document;
            ClientId = input.ClientId;


            return this;
        }
    }
}