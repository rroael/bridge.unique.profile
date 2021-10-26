using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Models.Providers;
using Bridge.Unique.Profile.System.Helpers;

namespace Bridge.Unique.Profile.API.Models.Requests
{
    /// <summary>
    ///     Requisição de login
    /// </summary>
    public class LoginSocialRequest : UserSocialIn, IToObjectMapper<IdentitySocial>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public LoginSocialRequest()
        {
            Password = RandomHelper.GetRandomString(8);
        }

        /// <summary>
        ///     Mapeia requisição para modelo
        /// </summary>
        /// <returns></returns>
        public IdentitySocial MapTo()
        {
            return new IdentitySocial
            {
                Email = Email.ToLower(),
                UserName = UserName,
                Password = Password,
                ApiClientId = ApiClientId,
                ApplicationToken = ApplicationToken,
                Name = Name,
                PhoneNumber = PhoneNumber,
                ProfileId = ProfileId,
                HasUserName = HasUserName,
                Provider = new Provider
                {
                    ProviderAuth = (EAuthProvider)Provider,
                    ProviderToken = ProviderToken,
                    ProviderUserId = ProviderUserId,
                    ProviderClientSecret = ProviderClientSecret,
                    IsLoginWeb = IsLoginWeb
                }
            };
        }
    }
}