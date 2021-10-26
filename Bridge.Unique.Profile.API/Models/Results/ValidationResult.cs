namespace Bridge.Unique.Profile.API.Models.Results
{
    /// <summary>
    ///     Resultado de validação de e-mail
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        ///     Se o e-mail foi validado
        /// </summary>
        public bool IsValidated { get; set; }

        /// <summary>
        ///     Mensagem de validação
        /// </summary>
        public string Message { get; set; }
    }
}