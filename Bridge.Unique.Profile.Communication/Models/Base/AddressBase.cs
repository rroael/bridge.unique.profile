using System.ComponentModel.DataAnnotations;
using Bridge.Commons.Location.Models;

namespace Bridge.Unique.Profile.Communication.Models.Base
{
    /// <summary>
    ///     Base de transferência de dados de endereço
    /// </summary>
    public class AddressBase
    {
        #region Properties

        /// <summary>
        ///     Apelido do endereço
        /// </summary>
        /// <example>Meu endereço</example>
        [Required]
        public string Nickname { get; set; }

        /// <summary>
        ///     Logradouro
        /// </summary>
        /// <example>Av. República Argentina</example>
        [Required]
        public string Street { get; set; }

        /// <summary>
        ///     Número predial
        /// </summary>
        /// <example>1228</example>
        [Required]
        public string StreetNumber { get; set; }

        /// <summary>
        ///     Bairro
        /// </summary>
        /// <example>Água Verde</example>
        [Required]
        public string Neighborhood { get; set; }

        /// <summary>
        ///     Cidade
        /// </summary>
        /// <example>Curitiba</example>
        [Required]
        public string City { get; set; }

        /// <summary>
        ///     Estado
        /// </summary>
        /// <example>Paraná</example>
        [Required]
        public string State { get; set; }

        /// <summary>
        ///     País
        /// </summary>
        /// <example>Brasil</example>
        [Required]
        public string Country { get; set; }

        /// <summary>
        ///     Complemento
        /// </summary>
        /// <example>24° Andar, Sala 2</example>
        public string Complement { get; set; }

        /// <summary>
        ///     Tipos de endereço
        /// </summary>
        /// <example>2</example>
        [Required]
        public int AddressTypes { get; set; }

        /// <summary>
        ///     Código postal (CEP)
        /// </summary>
        /// <example>80290970</example>
        [Required]
        public string ZipCode { get; set; }

        /// <summary>
        ///     Telefone
        /// </summary>
        /// <remarks>
        ///     Formato: + Código do país, Código da região, número.
        /// </remarks>
        /// <example>
        ///     +5511999887766
        /// </example>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     E-mail
        /// </summary>
        /// <example>email@company.com</example>
        public string Email { get; set; }

        /// <summary>
        ///     Nome do usuário, morador ou cliente, etc...
        /// </summary>
        /// <example>Fulano da Silva</example>
        public string Name { get; set; }

        /// <summary>
        ///     Localização do endereço
        /// </summary>
        [Required]
        public LocationPoint Location { get; set; }

        /// <summary>
        ///     Se o endereço está ativo
        /// </summary>
        /// <example>true</example>
        public bool Active { get; set; } = true;

        /// <summary>
        ///     Identificador do usuário
        /// </summary>
        /// <example>1</example>
        [Required]
        public int UserId { get; set; }

        #endregion
    }
}