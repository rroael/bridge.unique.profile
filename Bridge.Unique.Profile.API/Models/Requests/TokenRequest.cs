using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Requests
{
    /// <summary>
    ///     Requisição de token
    /// </summary>
    public class TokenRequest : TokenIn, IToObjectMapper<Token>
    {
        /// <summary>
        ///     Mapeia requisição para modelo
        /// </summary>
        /// <returns></returns>
        public Token MapTo()
        {
            return new Token
            {
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
                ApplicationToken = ApplicationToken
            };
        }
    }
}