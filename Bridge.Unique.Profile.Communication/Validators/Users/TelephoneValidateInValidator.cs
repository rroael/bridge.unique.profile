using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <summary>
    ///     Validador de validação de telefone
    /// </summary>
    public class TelephoneValidateInValidator : AbstractValidator<TelephoneValidateIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public TelephoneValidateInValidator()
        {
            RuleFor(x => x).SetValidator(new TelephoneValidationResendInValidator());

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .Length(4)
                .WithState(EError.WRONG_PHONE_CODE_LENGTH, BaseErrors.InvalidRequestParameter);
        }
    }
}