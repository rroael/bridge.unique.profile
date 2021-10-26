using Bridge.Commons.System.Models;

namespace Bridge.Unique.Profile.Communication.Models.Base
{
    /// <summary>
    ///     Base de transferência de dados de usuário com dados essenciais
    /// </summary>
    public class UserEssentialBase : BaseModel<int>
    {
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
        /// <example>email@example.com</example>
        public string Email { get; set; }

        /// <summary>
        ///     Número de telefone
        /// </summary>
        /// <example>+5599988776655</example>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Identificador do perfil
        /// </summary>
        /// <example>1</example>
        public int ProfileId { get; set; }

        /// <summary>
        ///     Identificador da aplicação
        /// </summary>
        /// <example>10</example>
        public int ApiClientId { get; set; }

        /// <summary>
        ///     Se o usuário deve ter um nome de usuário.
        ///     Caso "false" e o UserName estiver vazio, um nome de usuário será gerado automaticamente.
        ///     Caso "true" e o UserName estiver vazio, gerará uma exceção.
        /// </summary>
        /// <example>true</example>
        public bool HasUserName { get; set; }
    }
}