using Bridge.Commons.System.Models;

namespace Bridge.Unique.Profile.Communication.Models.Base
{
    /// <summary>
    ///     Base de transferência de dados de contato
    /// </summary>
    public class ContactBase : BaseModel<long>
    {
        #region Properties

        /// <summary>
        ///     Id do Cliente
        /// </summary>
        /// <example>77</example>
        public int ClientId { get; set; }

        /// <summary>
        ///     Nome
        /// </summary>
        /// <example>Empresa X Varejista</example>
        public string Name { get; set; }

        /// <summary>
        ///     Endereço de e-mail
        /// </summary>
        /// <example>email@company.com</example>
        public string Email { get; set; }

        /// <summary>
        ///     Número de telefone
        /// </summary>
        /// <example>+5511999887766</example>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Tipo de contato
        /// </summary>
        /// <example>1</example>
        public int ContactType { get; set; }

        /// <summary>
        ///     CPF do contato
        /// </summary>
        /// <example>858.678.030-82</example>
        public string Document { get; set; }

        #endregion
    }
}