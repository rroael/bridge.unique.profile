using System.Collections.Generic;
using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Models.Base;
using Bridge.Unique.Profile.Communication.Validators;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de validação de cliente
    /// </summary>
    public class ClientIn : ClientBase, IValidationRequest
    {
        /// <summary>
        ///     Título de sms e email
        /// </summary>
        /// <example>Empresa:</example>
        public string Sender { get; set; }

        /// <summary>
        ///     Lista dos códigos das apis a serem vinculadas ao cliente
        /// </summary>
        /// <example>api_code_example</example>
        public List<string> ApiCodes { get; set; }

        /// <summary>
        ///     Validador dos dados de cliente
        /// </summary>
        /// <returns></returns>
        public IValidator GetValidator()
        {
            return new ClientInValidator();
        }
    }
}