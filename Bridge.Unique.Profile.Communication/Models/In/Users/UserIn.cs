using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Models.Base;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de usuário
    /// </summary>
    public class UserIn : UserBase, IValidationRequest
    {
        /// <summary>
        ///     Senha do usuário
        /// </summary>
        /// <example>example12345</example>
        public string Password { get; set; }

        /// <summary>
        ///     Provider de entrada
        /// </summary>
        /// <example>1</example>
        public int Provider { get; set; } = 0;

        /// <summary>
        ///     Id do usuário no Provider de entrada
        /// </summary>
        /// <example>345</example>
        public string ProviderUserId { get; set; }

        /// <summary>
        ///     Token do Provider de entrada
        /// </summary>
        /// <example>token_example_provider</example>
        public string ProviderToken { get; set; }

        /// <summary>
        ///     ClientId do Provider de entrada
        /// </summary>
        /// <example>26</example>
        public string ProviderClientId { get; set; }

        /// <summary>
        ///     ClientSecret do Provider de entrada
        /// </summary>
        /// <example>example54321</example>
        public string ProviderClientSecret { get; set; }

        /// <summary>
        ///     Buscar validador
        /// </summary>
        /// <returns></returns>
        public new IValidator GetValidator()
        {
            return new UserInValidator();
        }
    }
}