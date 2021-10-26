using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Models.In;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators
{
    /// <summary>
    ///     Validador da entrada de atualização de cliente
    /// </summary>
    public class ClientUpdateInValidator : AbstractValidator<ClientUpdateIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public ClientUpdateInValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter)
                .GreaterThan(0)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);

            RuleFor(x => x).SetValidator(new ClientInValidator());
        }
    }
}