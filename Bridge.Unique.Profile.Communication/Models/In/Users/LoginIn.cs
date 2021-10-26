using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de login
    /// </summary>
    public class LoginIn : PasswordRecoverIn, IValidationRequest
    {
        /// <summary>
        ///     Buscar validador
        /// </summary>
        /// <returns></returns>
        public new IValidator GetValidator()
        {
            return new LoginInValidator();
        }
    }
}