using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.Out;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Results
{
    /// <summary>
    ///     Resultado de token
    /// </summary>
    public class TokenResult : TokenOut, IFromObjectMapper<Token, TokenResult>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public TokenResult()
        {
        }

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="model">Modelo</param>
        public TokenResult(Token model)
        {
            MapFrom(model);
        }

        /// <summary>
        ///     Mapeia modelo para resultado
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TokenResult MapFrom(Token input)
        {
            if (input == null) return this;

            AccessToken = input.AccessToken;
            RefreshToken = input.RefreshToken;

            return this;
        }
    }
}