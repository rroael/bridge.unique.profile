using System;
using Bridge.Unique.Profile.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.API.Attributes
{
    /// <summary>
    ///     Atributo de autenticação de aplicação
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminAuthorizeAttribute : ServiceFilterAttribute
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public AdminAuthorizeAttribute() : base(typeof(AdminAuthorizeFilter))
        {
        }
    }
}