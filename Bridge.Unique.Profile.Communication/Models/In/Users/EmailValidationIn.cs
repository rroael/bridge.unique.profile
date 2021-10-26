using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de validação de e-mail
    /// </summary>
    public class EmailValidationIn : IValidationRequest
    {
        /// <summary>
        ///     Token de validação
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///     Buscar validador
        /// </summary>
        /// <returns></returns>
        public IValidator GetValidator()
        {
            return new EmailValidationInValidator();
        }
    }
}