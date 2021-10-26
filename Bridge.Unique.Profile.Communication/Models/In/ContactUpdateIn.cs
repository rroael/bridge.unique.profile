using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Validators;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de validação de contato
    /// </summary>
    public class ContactUpdateIn : ContactIn, IValidationRequest
    {
        /// <summary>
        ///     Validador dos dados de contato
        /// </summary>
        /// <returns></returns>
        public new IValidator GetValidator()
        {
            return new ContactUpdateInValidator();
        }
    }
}