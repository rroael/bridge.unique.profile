using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de Refresh de token
    /// </summary>
    public class RefreshTokenIn : IValidationRequest
    {
        /// <summary>
        ///     Token de refresh
        /// </summary>
        /// <example>PSKXAP(S0cicps09fud9asfjfodfMPOdOLkOIoikLAM>j9p8dsd()diu-90adsladlma</example>
        public string RefreshToken { get; set; }

        /// <summary>
        ///     Buscar validador
        /// </summary>
        /// <returns></returns>
        public IValidator GetValidator()
        {
            return new RefreshTokenInValidator();
        }
    }
}