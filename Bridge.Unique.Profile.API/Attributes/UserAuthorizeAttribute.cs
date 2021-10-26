using System;
using Bridge.Unique.Profile.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.API.Attributes
{
    /// <summary>
    ///     Atributo de autenticação de usuário
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserAuthorizeAttribute : ServiceFilterAttribute
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public UserAuthorizeAttribute() : base(typeof(UserAuthorizeFilter))
        {
        }
    }
}