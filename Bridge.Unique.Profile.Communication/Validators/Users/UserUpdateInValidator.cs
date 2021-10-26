using Bridge.Unique.Profile.Communication.Models.In.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Validators.Users
{
    /// <summary>
    ///     Validador de atualização de usuário
    /// </summary>
    public class UserUpdateInValidator : AbstractValidator<UserUpdateIn>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public UserUpdateInValidator()
        {
            RuleFor(x => x).SetValidator(new UserBaseValidator());
        }
    }
}