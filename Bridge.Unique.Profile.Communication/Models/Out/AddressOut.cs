using Bridge.Unique.Profile.Communication.Models.Base;

namespace Bridge.Unique.Profile.Communication.Models.Out
{
    /// <summary>
    ///     Objeto de transferência de dados de saída de endereço
    /// </summary>
    public class AddressOut : AddressBase
    {
        /// <summary>
        ///     Identificador
        /// </summary>
        /// <example>14510</example>
        public long Id { get; set; }
    }
}