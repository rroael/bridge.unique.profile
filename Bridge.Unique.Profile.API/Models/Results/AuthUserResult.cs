using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.Out;
using Bridge.Unique.Profile.System.Models;

namespace Bridge.Unique.Profile.API.Models.Results
{
    /// <summary>
    ///     Resultado de autenticação básica de usuário
    /// </summary>
    public class AuthUserResult : AuthUserOut, IFromObjectMapper<AuthUser, AuthUserResult>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="model"></param>
        public AuthUserResult(AuthUser model)
        {
            MapFrom(model);
        }

        /// <summary>
        ///     Mapeia modelo para resultado
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public AuthUserResult MapFrom(AuthUser input)
        {
            Id = input.Id;
            ApiClientId = input.ApplicationId;
            ClientId = input.ClientId;
            ProfileId = input.ProfileId;

            return this;
        }
    }
}