using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Requests
{
    /// <summary>
    ///     Requisição de atualização administrativa de usuário
    /// </summary>
    public class UserActionRequest : UserActionIn, IToObjectMapper<UserAction>
    {
        /// <summary>
        ///     Mapeia requisição para modelo
        /// </summary>
        /// <returns></returns>
        public UserAction MapTo()
        {
            return new UserAction
            {
                UserId = UserId,
                Action = Action.HasValue && Action.Value,
                ApiCode = ApiCode,
                ClientId = ClientId,
                ApiClientId = ApiClientId
            };
        }
    }
}