using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Requests
{
    /// <summary>
    ///     Requisição de criação de usuário com dados essenciais
    /// </summary>
    public class UserEssentialRequest : UserEssentialIn, IToObjectMapper<User>
    {
        /// <summary>
        ///     Mapeia requisição para modelo
        /// </summary>
        /// <returns></returns>
        public User MapTo()
        {
            return new User
            {
                Name = Name,
                Email = Email.ToLower(),
                PhoneNumber = PhoneNumber,
                ProfileId = ProfileId,
                Password = Password,
                UserName = UserName,
                HasUserName = HasUserName,
                Active = true
            };
        }
    }
}