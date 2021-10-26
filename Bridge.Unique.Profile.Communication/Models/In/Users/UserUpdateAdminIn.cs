using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Atualização do usuário
    /// </summary>
    public class UserUpdateAdminIn : UserUpdateIn, IValidationRequest
    {
        /// <summary>
        ///     Se o usuário está ativo
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        ///     Buscar validador
        /// </summary>
        /// <returns></returns>
        public new IValidator GetValidator()
        {
            return new UserUpdateAdminInValidator();
        }
    }
}