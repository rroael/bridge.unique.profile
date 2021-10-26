using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <summary>
    ///     Validador de atualização de token
    /// </summary>
    public class RefreshTokenInValidator : AbstractValidator<RefreshTokenIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public RefreshTokenInValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .MinimumLength(56)
                .WithState(EError.REFRESH_TOKEN_MINIMUM_LENGTH, BaseErrors.InvalidRequestParameter);
        }
    }
}