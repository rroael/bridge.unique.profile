namespace Bridge.Unique.Profile.Communication.Models.Out
{
    /// <summary>
    ///     Objeto de transferência de dados de saída de relacionamento API e Cliente
    /// </summary>
    public class ApiClientOut
    {
        /// <summary>
        ///     Identificador do relacionamento
        /// </summary>
        /// <example>5</example>
        public int Id { get; set; }

        /// <summary>
        ///     Objeto de API
        /// </summary>
        public ApiOut Api { get; set; }

        /// <summary>
        ///     Objeto de Cliente
        /// </summary>
        public ClientOut Client { get; set; }

        /// <summary>
        ///     Chave da api gateway
        /// </summary>
        /// <example>api_token_example</example>
        public string Token { get; set; }

        /// <summary>
        ///     Se a aplicação está ativa
        /// </summary>
        /// <example>true</example>
        public bool Active { get; set; }

        /// <summary>
        ///     Título de sms e email
        /// </summary>
        /// <example>Empresa:</example>
        public string Sender { get; set; }

        /// <summary>
        ///     Código
        /// </summary>
        public string Code { get; set; }
    }
}