using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.System.Enums;

namespace Bridge.Unique.Profile.API.Models.Requests
{
    /// <summary>
    ///     Requisição completa de usuário
    /// </summary>
    public class UserRequest : UserIn, IToObjectMapper<User>
    {
        /// <summary>
        ///     Mapeia requisição para modelo
        /// </summary>
        /// <returns></returns>
        public User MapTo()
        {
            return new User
            {
                Document = Document,
                Email = Email.ToLower(),
                UserName = UserName,
                PhoneNumber = PhoneNumber,
                Name = Name,
                ImageUrl = ImageUrl,
                BirthDate = BirthDate,
                Gender = (EGender)Gender,
                ProfileId = ProfileId,
                Password = Password,
                ApiClientId = ApiClientId,
                Active = true,
                Provider = (EAuthProvider)Provider,
                ProviderToken = ProviderToken,
                ProviderUserId = ProviderUserId ?? "0"
            };
        }
    }
}