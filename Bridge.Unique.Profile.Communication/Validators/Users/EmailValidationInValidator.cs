using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <summary>
    ///     Validator de validação de e-mail
    /// </summary>
    public class EmailValidationInValidator : AbstractValidator<EmailValidationIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public EmailValidationInValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .Length(56)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);
        }
    }
}