using Bridge.Commons.System.Contracts.Filters;

namespace Bridge.Unique.Profile.Communication.Models.In.Filters
{
    /// <summary>
    ///     Objeto de entrada de filtro de cliente
    /// </summary>
    public class ClientFilterIn : IFilter
    {
        /// <summary>
        ///     Código
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Nome
        /// </summary>
        public string Name { get; set; }
    }
}