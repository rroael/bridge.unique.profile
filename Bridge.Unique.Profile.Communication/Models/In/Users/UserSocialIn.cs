using Bridge.Commons.System.Models;
using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de usuário
    /// </summary>
    public class UserSocialIn : BaseModel<int>, IValidationRequest
    {
        /// <summary>
        ///     Senha do usuário
        /// </summary>
        /// <example>example12345</example>
        public string Password { get; set; }

        /// <summary>
        ///     Nome de usuário (login)
        /// </summary>
        /// <example>Fulano</example>
        public string UserName { get; set; }

        /// <summary>
        ///     O nome do usuário
        /// </summary>
        /// <example>Fulano da Silva</example>
        public string Name { get; set; }

        /// <summary>
        ///     Endereço de e-mail
        /// </summary>
        /// <example>example@email.com</example>
        public string Email { get; set; }

        /// <summary>
        ///     Número de telefone
        /// </summary>
        /// <example>+5599988776655</example>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Identificador do perfil
        /// </summary>
        /// <example>110</example>
        public int ProfileId { get; set; }

        /// <summary>
        ///     Identificador da aplicação
        /// </summary>
        /// <example>1</example>
        public int ApiClientId { get; set; }

        /// <summary>
        ///     Se o usuário deve ter um nome de usuário.
        ///     Caso "false" e o UserName estiver vazio, um nome de usuário será gerado automaticamente.
        ///     Caso "true" e o UserName estiver vazio, gerará uma exceção.
        /// </summary>
        /// <example>true</example>
        public bool HasUserName { get; set; }

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
        ///     ClientSecret gerado dinamicamente no login
        /// </summary>
        /// <example>token_example_provider</example>
        public string ProviderClientSecret { get; set; }

        /// <summary>
        ///     Flag para identificar login web
        /// </summary>
        /// <example>false</example>
        public bool IsLoginWeb { get; set; } = false;

        /// <summary>
        ///     Buscar validador
        /// </summary>
        /// <returns></returns>
        public IValidator GetValidator()
        {
            return new UserSocialInValidator();
        }
    }
}