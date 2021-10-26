using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <summary>
    ///     Validador de atualização de senha
    /// </summary>
    public class UpdatePasswordInValidator : AbstractValidator<UpdatePasswordIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public UpdatePasswordInValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled)
                .MinimumLength(8)
                .WithState(EError.PASSWORD_MINIMUM_LENGTH, BaseErrors.InvalidRequestParameter);

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled)
                .MinimumLength(8)
                .WithState(EError.PASSWORD_MINIMUM_LENGTH, BaseErrors.InvalidRequestParameter);
        }
    }
}