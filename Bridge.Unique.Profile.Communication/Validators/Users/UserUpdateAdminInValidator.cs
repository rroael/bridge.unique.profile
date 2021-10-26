using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <summary>
    ///     Atualização usuário admin
    /// </summary>
    public class UserUpdateAdminInValidator : AbstractValidator<UserUpdateAdminIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public UserUpdateAdminInValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled)
                .GreaterThan(0)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);

            RuleFor(x => x).SetValidator(new UserUpdateInValidator());
        }
    }
}