using System;
using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Validators.Users;
using FluentValidation;

namespace Bridge.Unique.Profile.Communication.Models.Base
{
    /// <summary>
    ///     Base de transferência de dados de usuário
    /// </summary>
    public class UserBase : UserEssentialBase, IValidationRequest
    {
        /// <summary>
        ///     Documento (CPF/CNPJ) do usuário
        /// </summary>
        /// <example>01234567890</example>
        public string Document { get; set; }

        /// <summary>
        ///     Foto
        /// </summary>
        /// <example>/example/image/user/url</example>
        public string ImageUrl { get; set; }

        /// <summary>
        ///     Data de nascimento
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        ///     Gênero
        /// </summary>
        /// <example>0</example>
        public int Gender { get; set; }

        /// <summary>
        ///     Buscar validador
        /// </summary>
        /// <returns></returns>
        public IValidator GetValidator()
        {
            return new UserBaseValidator();
        }
    }
}