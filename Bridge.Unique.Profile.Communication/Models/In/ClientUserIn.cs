using Bridge.Unique.Profile.Communication.Models.In.Users;

namespace Bridge.Unique.Profile.Communication.Models.In
{
    /// <summary>
    ///     Objeto de entrega de cliente e usuário
    /// </summary>
    public class ClientUserIn
    {
        /// <summary>
        ///     Objeto de entrada de cliente
        /// </summary>
        public ClientIn Client { get; set; }

        /// <summary>
        ///     Objeto de entrada de usuário
        /// </summary>
        public UserIn User { get; set; }

        /// <summary>
        ///     Código da API a qual o usuário será associado (Por exemplo: PAP - API do Portal Turboshop)
        /// </summary>
        public string UserApiCodeToAssociate { get; set; }
    }
}