namespace Bridge.Unique.Profile.Communication.Models.Base
{
    /// <summary>
    ///     Base de transferência de dados de cliente
    /// </summary>
    public class ClientBase : ClientEssentialBase
    {
        #region Properties

        /// <summary>
        ///     Código único do cliente
        /// </summary>
        /// <example>EMX</example>
        public string Code { get; set; }

        /// <summary>
        ///     Se o cliente está ativo
        /// </summary>
        /// <example>true</example>
        public bool Active { get; set; }

        #endregion
    }
}