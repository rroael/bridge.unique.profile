using System.Text.Json.Serialization;
using Bridge.Commons.System.Models.Requests;
using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de recuperação de senha
    /// </summary>
    public class PasswordRecoverIn : BaseRequest<int>, IValidationRequest
    {
        /// <summary>
        ///     Identificador da aplicação
        /// </summary>
        /// <example>1</example>
        [JsonIgnore]
        public int ApiClientId { get; set; }

        /// <summary>
        ///     Código a API
        /// </summary>
        [JsonIgnore]
        public string ApiCode { get; set; }

        /// <summary>
        ///     Endereço de e-mail
        /// </summary>
        /// <example>example@email.com</example>
        public string Email { get; set; }

        /// <summary>
        ///     Nome de usuário (login)
        /// </summary>
        /// <example>Fulano</example>
        public string UserName { get; set; }

        /// <summary>
        ///     Senha do usuário
        /// </summary>
        /// <example>example12345</example>
        public string Password { get; set; }

        /// <summary>
        ///     Buscar validador
        /// </summary>
        /// <returns></returns>
        public IValidator GetValidator()
        {
            return new PasswordRecoverInValidator();
        }
    }
}