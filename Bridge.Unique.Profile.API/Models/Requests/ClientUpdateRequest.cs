using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.In;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Requests
{
    /// <summary>
    ///     Requisição de atualização de cliente
    /// </summary>
    public class ClientUpdateRequest : ClientUpdateIn, IToObjectMapper<Client>
    {
        /// <summary>
        ///     Mapeia requisição para modelo
        /// </summary>
        /// <returns></returns>
        public Client MapTo()
        {
            return new Client
            {
                Id = Id,
                Active = Active,
                Name = Name,
                Description = Description,
                Code = Code,
                Sender = Sender,
                ApplicationToken = ApplicationToken,
                Document = Document,
                Segment = Segment
            };
        }
    }
}