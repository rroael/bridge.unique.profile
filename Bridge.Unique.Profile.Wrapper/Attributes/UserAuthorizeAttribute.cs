using System;
using Bridge.Unique.Profile.Wrapper.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.Wrapper.Attributes
{
    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserAuthorizeAttribute : ServiceFilterAttribute
    {
        /// <summary>
        /// </summary>
        public UserAuthorizeAttribute() : base(typeof(UserAuthorizeFilter))
        {
        }
    }
}