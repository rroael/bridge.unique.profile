using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using Bridge.Unique.Profile.Communication.Resources;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <summary>
    ///     Validação Social do usuário
    /// </summary>
    public class UserSocialInValidator : AbstractValidator<UserSocialIn>
    {
        /// <summary>
        ///     Validador social do usuário
        /// </summary>
        public UserSocialInValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled)
                .MinimumLength(3)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .Must(ValidateCompleteName)
                .WithState(EError.NAME_NOT_COMPLETE, Errors.NameNotComplete);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled)
                .EmailAddress()
                .WithState(EError.WRONG_EMAIL_FORMAT, BaseErrors.InvalidRequestParameter);

            When(x => x.HasUserName, () =>
            {
                RuleFor(x => x.UserName)
                    .NotEmpty()
                    .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled)
                    .MinimumLength(8)
                    .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);
            });

            RuleFor(x => x.ProfileId)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled)
                .GreaterThan(0)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);

            RuleFor(x => x.Provider)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled);

            RuleFor(x => x.ProviderUserId)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled);

            RuleFor(x => x.ProviderToken)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled);
        }

        private bool ValidateCompleteName(string name)
        {
            return name.Contains(" ");
        }
    }
}