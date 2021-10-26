using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.Out;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Results
{
    /// <summary>
    ///     Resultado de aplicação
    /// </summary>
    public class ApiResult : ApiOut, IFromObjectMapper<Api, ApiResult>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public ApiResult()
        {
        }

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="model">Modelo</param>
        public ApiResult(Api model)
        {
            MapFrom(model);
        }

        /// <summary>
        ///     Mapeia modelo para resultado
        /// </summary>
        /// <param name="input">Modelo</param>
        /// <returns></returns>
        public ApiResult MapFrom(Api input)
        {
            Id = input.Id;
            Name = input.Name;
            Description = input.Description;
            CreateDate = input.CreateDate;
            Active = input.Active;

            return this;
        }
    }
}