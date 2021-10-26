namespace Bridge.Unique.Profile.Communication.Models.In.Addresses
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de endereço.
    ///     Lista por array de identificadores
    /// </summary>
    public class AddressListByIdIn
    {
        /// <summary>
        ///     Array de identificadores
        /// </summary>
        public long[] Ids { get; set; }
    }
}