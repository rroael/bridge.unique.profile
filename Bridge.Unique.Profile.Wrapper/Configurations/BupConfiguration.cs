namespace Bridge.Unique.Profile.Wrapper.Configurations
{
    /// <summary>
    ///     Configuração essencial do serviço Bup
    /// </summary>
    public class BupConfiguration
    {
        /// <summary>
        ///     Url base do serviço
        /// </summary>
        public string ServiceBaseUrl { get; set; }

        /// <summary>
        ///     Token de autorização da sua aplicação cadastrada no Bup
        /// </summary>
        public string AppAuthorization { get; set; }
    }
}