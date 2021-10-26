using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de validação de telefone
    /// </summary>
    public class TelephoneValidateIn : TelephoneValidationResendIn, IValidationRequest
    {
        /// <summary>
        ///     Código de validação enviado por SMS
        /// </summary>
        /// <example>998877</example>
        public string Code { get; set; }

        /// <summary>
        ///     Buscar validador
        /// </summary>
        /// <returns></returns>
        public new IValidator GetValidator()
        {
            return new TelephoneValidateInValidator();
        }
    }
}