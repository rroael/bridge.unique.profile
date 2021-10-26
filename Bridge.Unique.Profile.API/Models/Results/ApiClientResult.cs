using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.Out;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Results
{
    /// <summary>
    ///     Resultado de aplicação
    /// </summary>
    public class ApiClientResult : ApiClientOut, IFromObjectMapper<ApiClient, ApiClientResult>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public ApiClientResult()
        {
        }

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="model">Modelo</param>
        public ApiClientResult(ApiClient model)
        {
            MapFrom(model);
        }

        /// <summary>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ApiClientResult MapFrom(ApiClient input)
        {
            Id = input.Id;
            Active = input.Active;
            Sender = input.Sender;
            Code = input.Code;
            Token = input.Token;
            Api = input.Api != null
                ? new ApiOut
                {
                    Id = input.Api.Id,
                    Active = input.Api.Active,
                    Description = input.Api.Description,
                    Name = input.Api.Name
                }
                : null;
            return this;
        }
    }
}