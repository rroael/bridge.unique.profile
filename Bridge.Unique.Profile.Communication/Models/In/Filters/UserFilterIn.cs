using System.Text.Json.Serialization;
using Bridge.Commons.System.Contracts.Filters;

namespace Bridge.Unique.Profile.Communication.Models.In.Filters
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de diltro
    /// </summary>
    public class UserFilterIn : IFilter
    {
        /// <summary>
        ///     Documento (CPF/CNPJ) do usuário
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        ///     O nome do usuário
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Endereço de e-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Telefone do usuário
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Identificador do cliente na API
        /// </summary>
        public int ApiClientId { get; set; }

        /// <summary>
        ///     Identificador da aplicação em que o entregador esta
        /// </summary>
        [JsonIgnore]
        public string ApiCode { get; set; }

        /// <summary>
        ///     Identificador do cliente
        /// </summary>
        [JsonIgnore]
        public int ClientId { get; set; }

        /// <summary>
        ///     Identificador do perfil
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        ///     Identificador do usuário logado
        /// </summary>
        public int UserId { get; set; }
    }
}