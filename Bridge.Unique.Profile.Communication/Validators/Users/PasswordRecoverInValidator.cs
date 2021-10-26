using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <summary>
    ///     Validador de recuperação de senha
    /// </summary>
    public class PasswordRecoverInValidator : AbstractValidator<PasswordRecoverIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public PasswordRecoverInValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled)
                .EmailAddress()
                .WithState(EError.WRONG_EMAIL_FORMAT, BaseErrors.InvalidRequestParameter);
        }
    }
}