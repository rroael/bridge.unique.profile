using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Models.Base;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de criação de usuário
    /// </summary>
    public class UserEssentialIn : UserEssentialBase, IValidationRequest
    {
        /// <summary>
        ///     Senha do usuário
        /// </summary>
        /// <example>example12345</example>
        public string Password { get; set; }

        /// <summary>
        ///     Buscar validador
        /// </summary>
        /// <returns></returns>
        public IValidator GetValidator()
        {
            return new UserEssentialInValidator();
        }
    }
}