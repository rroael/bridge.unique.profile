using System.Collections.Generic;
using Bridge.Unique.Profile.Communication.Models.Base;

namespace Bridge.Unique.Profile.Communication.Models.Out
{
    /// <inheritdoc />
    /// <summary>
    ///     Objeto de transferência de dados de saída de cliente
    /// </summary>
    public class ClientOut : ClientBase
    {
        /// <summary>
        ///     Lista das apis vinculadas
        /// </summary>
        public List<ApiClientOut> ApiClients { get; set; }
    }
}