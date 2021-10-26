using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <summary>
    ///     Validação da atualização de usuários
    /// </summary>
    public class UserActionInValidator : AbstractValidator<UserActionIn>
    {
        /// <summary>
        ///     Validação da atualização de usuários
        /// </summary>
        public UserActionInValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled);

            RuleFor(x => x.Action)
                .NotNull()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled);

            RuleFor(x => x.ApiCode)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled);
        }
    }
}