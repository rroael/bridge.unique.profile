using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Models.In;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators
{
    /// <summary>
    ///     Validação de atualização de contato
    /// </summary>
    public class ContactUpdateInValidator : AbstractValidator<ContactUpdateIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public ContactUpdateInValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .GreaterThan(0)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);

            RuleFor(x => x).SetValidator(new ContactInValidator());
        }
    }
}