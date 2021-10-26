using Bridge.Commons.Location.Models;
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
    public class AddressInValidator : AbstractValidator<AddressIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public AddressInValidator()
        {
            RuleFor(x => x.Nickname)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled);

            RuleFor(x => x.Street)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled);

            RuleFor(x => x.StreetNumber)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled);

            RuleFor(x => x.Neighborhood)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled);

            RuleFor(x => x.City)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled);

            RuleFor(x => x.State)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled)
                .Length(2, 2)
                .WithState(EBaseError.FIELD_WRONG_LENGTH, BaseErrors.FieldWrongLength);

            RuleFor(x => x.Country)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled);

            RuleFor(x => x.ZipCode)
                .NotNull()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled);

            RuleFor(x => x.Location)
                .NotEmpty()
                .WithState(EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled)
                .Must(ValidateLocation)
                .WithState(EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);
        }

        private static bool ValidateLocation(LocationPoint loc)
        {
            if (loc != null)
                return loc.Latitude != 0d && loc.Longitude != 0d;

            return true;
        }
    }
}