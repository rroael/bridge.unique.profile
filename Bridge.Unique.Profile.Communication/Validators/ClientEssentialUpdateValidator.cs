using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Models.In;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators
{
    /// <summary>
    ///     Validator de validação de cliente
    /// </summary>
    public class ClientEssentialUpdateValidator : AbstractValidator<ClientEssentialUpdateIn>
    {
        /// <summary>
        ///     Validador de dados do cliente
        /// </summary>
        public ClientEssentialUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .MaximumLength(128)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .MaximumLength(254)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);

            RuleFor(x => x.Document)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .MaximumLength(254)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);

            RuleFor(x => x.Segment)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .MaximumLength(254)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);
        }
    }
}