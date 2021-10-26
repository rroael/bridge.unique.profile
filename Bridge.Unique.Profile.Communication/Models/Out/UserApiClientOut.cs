using System;

namespace Bridge.Unique.Profile.Communication.Models.Out
{
    /// <summary>
    ///     Objeto de transferência de dados de saída
    /// </summary>
    public class UserApiClientOut
    {
        #region Properties

        /// <summary>
        ///     Identificação do usuário
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Identificação da ApiClient
        /// </summary>
        public int ApiClientId { get; set; }

        /// <summary>
        ///     Profile do usuário
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        ///     Termos de aceite
        /// </summary>
        public DateTime? TermsAcceptanceDate { get; set; }

        /// <summary>
        ///     Data e hora de criação
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        ///     Data e hora de atualização
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        ///     Se o usuário está ativo
        /// </summary>
        public bool Active { get; set; }

        #endregion
    }
}