using System;
using System.Collections.Generic;
using Bridge.Unique.Profile.Communication.Models.Base;

namespace Bridge.Unique.Profile.Communication.Models.Out
{
    /// <inheritdoc />
    /// <summary>
    ///     Objeto de transferência de dados de saída de usuário
    /// </summary>
    public class UserOut : UserBase
    {
        #region Properties

        /// <summary>
        ///     Data e hora de criação
        /// </summary>
        public DateTimeOffset? CreateDate { get; set; }

        /// <summary>
        ///     Se o usuário está ativo
        /// </summary>
        /// <example>true</example>
        public bool Active { get; set; }

        /// <summary>
        ///     Token de autorização
        /// </summary>
        public TokenOut Token { get; set; }

        /// <summary>
        ///     Data e hora do aceite dos termos de uso e da política de privacidade
        /// </summary>
        public DateTimeOffset? TermsAcceptanceDate { get; set; }

        /// <summary>
        ///     Lista de endereços
        /// </summary>
        public List<AddressOut> Addresses { get; set; } = new List<AddressOut>();

        /// <summary>
        ///     Lista de ApiClients
        /// </summary>
        public List<UserApiClientOut> ApiClients { get; set; } = new List<UserApiClientOut>();

        /// <summary>
        ///     Se o usuário (entregador) foi aprovado
        /// </summary>
        /// <example>true</example>
        public bool IsExternalApproved { get; set; }

        /// <summary>
        ///     Se o telefone do usuário foi validado
        /// </summary>
        /// <example>true</example>
        public bool IsPhoneNumberValidated { get; set; }

        /// <summary>
        ///     Se o email do usuário foi validado
        /// </summary>
        /// <example>true</example>
        public bool IsEmailValidated { get; set; }

        #endregion
    }
}