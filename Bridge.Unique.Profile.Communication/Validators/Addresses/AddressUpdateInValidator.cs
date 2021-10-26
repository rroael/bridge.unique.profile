using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Models.In.Addresses;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Addresses
{
    /// <inheritdoc />
    /// <summary>
    ///     Validador de endereço
    /// </summary>
    public class AddressUpdateInValidator : AbstractValidator<AddressUpdateIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public AddressUpdateInValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled);

            RuleFor(x => x).SetValidator(new AddressInValidator());
        }
    }
}