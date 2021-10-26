using System;

namespace Bridge.Unique.Profile.Communication.Models.Out
{
    /// <summary>
    ///     Objeto de transferência de dados de saída de aplicação
    /// </summary>
    public class ApiOut
    {
        #region Properties

        /// <summary>
        ///     Identificador da aplicação
        /// </summary>
        /// <example>10</example>
        public int Id { get; set; }

        /// <summary>
        ///     Nome
        /// </summary>
        /// <example>API X</example>
        public string Name { get; set; }

        /// <summary>
        ///     Descrição
        /// </summary>
        /// <example>API de acesso</example>
        public string Description { get; set; }

        /// <summary>
        ///     Data e hora de criação
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        ///     Se a aplicação está ativa
        /// </summary>
        /// <example>true</example>
        public bool Active { get; set; }

        #endregion
    }
}