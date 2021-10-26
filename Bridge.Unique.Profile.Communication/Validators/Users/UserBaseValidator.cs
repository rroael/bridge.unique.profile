using System;
using Bridge.Commons.Extension;
using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Resources;
using Bridge.Commons.Validation.Extensions;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.Base;
using Bridge.Unique.Profile.Communication.Resources;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <summary>
    ///     Validação de usuário
    /// </summary>
    public class UserBaseValidator : AbstractValidator<UserBase>
    {
        /// <summary>
        ///     Validador de usuário
        /// </summary>
        public UserBaseValidator()
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

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithState((int)EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled)
                .GreaterThan(new DateTime(1900, 1, 1))
                .WithState((int)EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);

            RuleFor(x => x.Gender)
                .NotEmpty()
                .WithState((int)EBaseError.FIELD_MUST_BE_FILLED, BaseErrors.FieldMustBeFilled)
                .InclusiveBetween(0, 2)
                .WithState((int)EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);

            When(x => !string.IsNullOrEmpty(x.Document), () =>
            {
                RuleFor(x => x.Document)
                    .Must(ValidateDocument)
                    .WithState((int)EBaseError.INVALID_REQUEST_PARAMETER, BaseErrors.InvalidRequestParameter);
            });

            When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber), () =>
            {
                RuleFor(x => x.PhoneNumber)
                    .MaximumLength(15)
                    .WithState((int)EBaseError.FIELD_MAXIMUM_LENGTH, BaseErrors.FieldMaximumLength);
            });
        }

        private bool ValidateDocument(string document)
        {
            return document.IsValidCpfCnpj();
        }

        private bool ValidateCompleteName(string name)
        {
            return name.Contains(" ");
        }
    }
}