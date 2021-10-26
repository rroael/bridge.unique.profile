using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Models.Base;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Atualização de usuário
    /// </summary>
    public class UserUpdateIn : UserBase, IValidationRequest
    {
        /// <summary>
        ///     Provider de entrada
        /// </summary>
        /// <example>1</example>
        public int Provider { get; set; } = 0;

        /// <summary>
        ///     Id do usuário no Provider de entrada
        /// </summary>
        /// <example>345</example>
        public string ProviderUserId { get; set; } = "0";

        /// <summary>
        ///     Token do Provider de entrada
        /// </summary>
        /// <example>token_example_provider</example>
        public string ProviderToken { get; set; }

        /// <summary>
        ///     Buscar usuário
        /// </summary>
        /// <returns></returns>
        public new IValidator GetValidator()
        {
            return new UserUpdateInValidator();
        }
    }
}