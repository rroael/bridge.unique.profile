using System.Text.Json.Serialization;
using Bridge.Commons.System.Models;
using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de atualização de senha
    /// </summary>
    public class UpdatePasswordIn : BaseModel<int>, IValidationRequest
    {
        /// <summary>
        ///     Senha antiga
        /// </summary>
        /// <example>example11223</example>
        public string OldPassword { get; set; }

        /// <summary>
        ///     Nova senha
        /// </summary>
        /// <example>example12345</example>
        public string NewPassword { get; set; }

        /// <summary>
        ///     Token de acesso
        /// </summary>
        /// <example>PSKXAP(S0cicps09fud9asfjfodfMPOdOLkOIoikLAM>j9p8dsd()diu-90adsladlma</example>
        public string AccessToken { get; set; }

        /// <summary>
        ///     Identificador da aplicação
        /// </summary>
        [JsonIgnore]
        public int ApplicationId { get; set; }

        /// <summary>
        ///     Buscar validador
        /// </summary>
        /// <returns></returns>
        public IValidator GetValidator()
        {
            return new UpdatePasswordInValidator();
        }
    }
}