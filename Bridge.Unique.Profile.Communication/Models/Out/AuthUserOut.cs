namespace Bridge.Unique.Profile.Communication.Models.Out
{
    /// <summary>
    ///     Objeto de transferência de dados de saída de usuário autenticado
    /// </summary>
    public class AuthUserOut
    {
        /// <summary>
        ///     Identificador do usuário
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        ///     Identificador da aplicação
        /// </summary>
        /// <example>1</example>
        public int ApiClientId { get; set; }

        /// <summary>
        ///     Código do cliente
        /// </summary>
        public string ClientCode { get; set; }

        /// <summary>
        ///     Identificador do cliente
        /// </summary>
        /// <example>30</example>
        public int ClientId { get; set; }

        /// <summary>
        ///     Identificador do perfil (role)
        /// </summary>
        /// <example>30</example>
        public int ProfileId { get; set; }
    }
}