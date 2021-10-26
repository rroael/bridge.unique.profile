using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <summary>
    ///     Validador de login
    /// </summary>
    public class LoginInValidator : AbstractValidator<LoginIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public LoginInValidator()
        {
            RuleFor(x => x).SetValidator(new PasswordRecoverInValidator());

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .MinimumLength(8)
                .WithState(EError.PASSWORD_MINIMUM_LENGTH, BaseErrors.InvalidRequestParameter);
        }
    }
}