using System.ComponentModel.DataAnnotations;
using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Validators.Addresses;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Addresses
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de endereço
    /// </summary>
    public class AddressUpdateIn : AddressIn, IValidationRequest
    {
        /// <summary>
        ///     Identificador
        /// </summary>
        /// <example>14510</example>
        [Required]
        public long Id { get; set; }

        /// <summary>
        ///     Busca validador
        /// </summary>
        /// <returns></returns>
        public new IValidator GetValidator()
        {
            return new AddressUpdateInValidator();
        }
    }
}