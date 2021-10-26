using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de Reenvio de validação de e-mail
    /// </summary>
    public class EmailValidationResendIn : IValidationRequest
    {
        /// <summary>
        ///     Endereço de e-mail
        /// </summary>
        /// <example>example@email.com</example>
        public string Email { get; set; }

        /// <summary>
        ///     Buscar validador
        /// </summary>
        /// <returns></returns>
        public IValidator GetValidator()
        {
            return new EmailValidationResendInValidator();
        }
    }
}