using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Requests
{
    /// <summary>
    ///     Requisição de login
    /// </summary>
    public class LoginRequest : LoginIn, IToObjectMapper<Identity>
    {
        /// <summary>
        ///     Mapeia requisição para modelo
        /// </summary>
        /// <returns></returns>
        public Identity MapTo()
        {
            return new Identity
            {
                Email = Email.ToLower(),
                UserName = UserName,
                Password = Password,
                ApiClientId = ApiClientId,
                ApplicationToken = ApplicationToken,
                ApiCode = ApiCode
            };
        }
    }
}