using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Models;

namespace Bridge.Unique.Profile.Domain.Models
{
    /// <summary>
    ///     Classe que representa a autenticação no serviço de cloud (Cognito ou AD-B2C)
    /// </summary>
    public class Token : BaseModel<int>, IBaseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}