using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <inheritdoc />
    /// <summary>
    ///     Validador de usuário
    /// </summary>
    public class UserInValidator : AbstractValidator<UserIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public UserInValidator()
        {
            RuleFor(x => x).SetValidator(new UserBaseValidator());

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled)
                .MinimumLength(8)
                .WithState(EError.PASSWORD_MINIMUM_LENGTH, BaseErrors.InvalidRequestParameter);
        }
    }
}