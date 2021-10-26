using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.In;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Requests
{
    /// <summary>
    ///     Requisição de cliente e usuário
    /// </summary>
    public class ClientUserRequest : ClientUserIn, IToObjectMapper<ClientUser>
    {
        /// <summary>
        ///     Objeto de cliente
        /// </summary>
        public new ClientRequest Client { get; set; }

        /// <summary>
        ///     Objeto de usuário
        /// </summary>
        public new UserRequest User { get; set; }

        /// <summary>
        ///     Mapeia requisição para objeto de entrada
        /// </summary>
        /// <returns></returns>
        public ClientUser MapTo()
        {
            return new ClientUser
            {
                Client = Client.MapTo(),
                User = User.MapTo(),
                UserApiCodeToAssociate = UserApiCodeToAssociate
            };
        }
    }
}