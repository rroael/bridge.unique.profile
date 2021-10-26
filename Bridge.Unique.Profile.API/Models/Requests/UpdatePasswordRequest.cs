using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.In.Users;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Requests
{
    /// <summary>
    ///     Requisição de atualização de senha
    /// </summary>
    public class UpdatePasswordRequest : UpdatePasswordIn, IToObjectMapper<UpdatePassword>
    {
        /// <summary>
        ///     Mapeia requisição para modelo
        /// </summary>
        /// <returns></returns>
        public UpdatePassword MapTo()
        {
            return new UpdatePassword
            {
                Id = Id,
                AccessToken = AccessToken,
                ApplicationToken = ApplicationToken,
                NewPassword = NewPassword,
                ApplicationId = ApplicationId,
                OldPassword = OldPassword
            };
        }
    }
}