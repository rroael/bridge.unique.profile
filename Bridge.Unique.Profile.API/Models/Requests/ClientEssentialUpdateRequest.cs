using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.In;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Requests
{
    /// <summary>
    ///     Requisição de atualização de cliente
    /// </summary>
    public class ClientEssentialUpdateRequest : ClientEssentialUpdateIn, IToObjectMapper<Client>
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
                Name = Name,
                Description = Description,
                ApplicationToken = ApplicationToken,
                Document = Document,
                Segment = Segment
            };
        }
    }
}