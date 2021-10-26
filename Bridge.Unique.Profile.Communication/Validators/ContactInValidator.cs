using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Models.In;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators
{
    /// <summary>
    ///     Validação de inserção contato
    /// </summary>
    public class ContactInValidator : AbstractValidator<ContactIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public ContactInValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .MaximumLength(128)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .MaximumLength(254)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);

            RuleFor(x => x.ContactType)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);

            RuleFor(x => x.Document)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .MaximumLength(32)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .MaximumLength(32)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);
        }
    }
}