using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.Out;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Results
{
    /// <summary>
    ///     Resultado de usuário
    /// </summary>
    public class UserApiClientResult : UserApiClientOut, IFromObjectMapper<UserApiClient, UserApiClientResult>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public UserApiClientResult()
        {
        }

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="model">Modelo</param>
        public UserApiClientResult(UserApiClient model)
        {
            MapFrom(model);
        }

        /// <summary>
        ///     Mapeia modelo para resultado
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public UserApiClientResult MapFrom(UserApiClient input)
        {
            ApiClientId = input.ApiClientId;
            Active = input.Active;
            ProfileId = input.ProfileId;
            CreateDate = input.CreateDate;
            UpdateDate = input.UpdateDate;
            UserId = input.UserId;

            return this;
        }
    }
}