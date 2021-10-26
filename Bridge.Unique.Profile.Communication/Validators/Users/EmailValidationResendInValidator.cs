using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using Bridge.Unique.Profile.Communication.Resources;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <inheritdoc />
    /// <summary>
    ///     Validador de reenvio de validação de e-mail
    /// </summary>
    public class EmailValidationResendInValidator : AbstractValidator<EmailValidationResendIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public EmailValidationResendInValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.InvalidRequestParameter)
                .EmailAddress()
                .WithState(EError.WRONG_EMAIL_FORMAT, Errors.WrongEmailFormat);
        }
    }
}