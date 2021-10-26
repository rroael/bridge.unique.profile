using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <summary>
    ///     Validador de reenvio de validação de telefone
    /// </summary>
    public class TelephoneValidationResendInValidator : AbstractValidator<TelephoneValidationResendIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public TelephoneValidationResendInValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .MinimumLength(10)
                .WithState(EError.WRONG_PHONE_NUMBER_LENGTH, BaseErrors.InvalidRequestParameter);
        }
    }
}