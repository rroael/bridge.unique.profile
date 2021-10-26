using Bridge.Commons.System.Models;

namespace Bridge.Unique.Profile.Communication.Models.Base
{
    /// <summary>
    ///     Base de transferência de dados de cliente
    /// </summary>
    public class ClientEssentialBase : BaseModel<int>
    {
        #region Properties

        /// <summary>
        ///     Nome
        /// </summary>
        /// <example>Empresa X</example>
        public string Name { get; set; }

        /// <summary>
        ///     Descrição
        /// </summary>
        /// <example>Empresa Varejista</example>
        public string Description { get; set; }

        /// <summary>
        ///     CNPJ do cliente
        /// </summary>
        /// <example>86.628.302/0001-24</example>
        public string Document { get; set; }

        /// <summary>
        ///     Seguimento do cliente
        /// </summary>
        /// <example>Varejo de moda, supermercado</example>
        public string Segment { get; set; }

        #endregion
    }
}