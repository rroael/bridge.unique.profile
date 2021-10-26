using System.Collections.Generic;
using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.Out;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Results
{
    /// <summary>
    ///     Resultado de cliente
    /// </summary>
    public class ClientResult : ClientOut, IFromObjectMapper<Client, ClientResult>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public ClientResult()
        {
        }

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="model">Modelo</param>
        public ClientResult(Client model)
        {
            MapFrom(model);
        }

        /// <summary>
        ///     Mapeia modelo para resultado
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ClientResult MapFrom(Client input)
        {
            Id = input.Id;
            Code = input.Code;
            Name = input.Name;
            Description = input.Description;
            Active = input.Active;
            ApiClients = new List<ApiClientOut>();
            Segment = input.Segment;
            Document = input.Document;

            if (input.Apis == null) return this;

            foreach (var item in input.Apis)
                ApiClients.Add(new ApiClientOut
                {
                    Active = item.Active,
                    Id = item.Id,
                    Sender = item.Sender,
                    Code = item.Code,
                    Token = item.Token,
                    Api = new ApiOut
                    {
                        Id = item.Api.Id,
                        Name = item.Api.Name,
                        Description = item.Api.Description,
                        Active = item.Api.Active
                    }
                });

            return this;
        }
    }
}