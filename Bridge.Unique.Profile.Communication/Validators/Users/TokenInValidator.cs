using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <summary>
    ///     Validador de token
    /// </summary>
    public class TokenInValidator : AbstractValidator<TokenIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public TokenInValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);
        }
    }
}