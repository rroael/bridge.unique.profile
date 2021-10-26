using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Models.Base;
using Bridge.Unique.Profile.Communication.Validators;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de validação de cliente
    /// </summary>
    public class ClientEssentialUpdateIn : ClientEssentialBase, IValidationRequest
    {
        /// <summary>
        ///     Validador dos dados de cliente
        /// </summary>
        /// <returns></returns>
        public IValidator GetValidator()
        {
            return new ClientEssentialUpdateValidator();
        }
    }
}