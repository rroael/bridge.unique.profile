using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Objeto de atualização administrativa de usuário
    /// </summary>
    public class UserActionIn : IValidationRequest
    {
        /// <summary>
        ///     Identificador do usuário
        /// </summary>
        /// <example>3</example>
        public int UserId { get; set; }

        /// <summary>
        ///     True = Ativar/Aprovar; False = Desativar/Reprovar.
        /// </summary>
        /// <example>false</example>
        public bool? Action { get; set; }

        /// <summary>
        ///     Código da API
        /// </summary>
        public string ApiCode { get; set; }

        /// <summary>
        ///     Identificador do cliente
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        ///     Identificador do ApiClient
        /// </summary>
        public int ApiClientId { get; set; }

        /// <summary>
        ///     Buscar Validadores
        /// </summary>
        /// <returns></returns>
        public IValidator GetValidator()
        {
            return new UserActionInValidator();
        }
    }
}