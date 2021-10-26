using System.Text.Json.Serialization;
using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de Reenvio de código de validação de telefone
    /// </summary>
    public class TelephoneValidationResendIn : IValidationRequest
    {
        /// <summary>
        ///     Identificador do usuário
        /// </summary>
        /// <example>1</example>
        [JsonIgnore]
        public int UserId { get; set; }

        /// <summary>
        ///     Número de telefone
        /// </summary>
        /// <example>+5599988776655</example>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Buscar validador
        /// </summary>
        /// <returns></returns>
        public IValidator GetValidator()
        {
            return new TelephoneValidationResendInValidator();
        }
    }
}