using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Models.Base;
using Bridge.Unique.Profile.Communication.Validators.Addresses;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Addresses
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de endereço
    /// </summary>
    public class AddressIn : AddressBase, IValidationRequest
    {
        /// <summary>
        ///     Busca validador
        /// </summary>
        /// <returns></returns>
        public IValidator GetValidator()
        {
            return new AddressInValidator();
        }
    }
}