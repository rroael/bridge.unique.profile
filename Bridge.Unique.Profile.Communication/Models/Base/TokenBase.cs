using Bridge.Commons.System.Models;

namespace Bridge.Unique.Profile.Communication.Models.Base
{
    /// <summary>
    ///     Base de transferência de dados de Token
    /// </summary>
    public class TokenBase : BaseModel<long>
    {
        #region Properties

        /// <summary>
        ///     Token de acesso (authorization)
        /// </summary>
        /// <example>PSKXAP(S0cicps09fud9asfjfodfMPOdOLkOIoikLAM>j9p8dsd()diu-90adsladlma</example>
        public string AccessToken { get; set; }

        /// <summary>
        ///     Token de atualização
        /// </summary>
        /// <example>SA(Dj9sadpm9sspdap9f8u3f8ijeiLJOISJAOI09dujsaidoLkLKSMDp309--)(ias9di</example>
        public string RefreshToken { get; set; }

        #endregion
    }
}