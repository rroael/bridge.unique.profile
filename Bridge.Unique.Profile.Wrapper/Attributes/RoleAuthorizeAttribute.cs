using System;
using System.Linq;
using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Exceptions;
using Bridge.Commons.System.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bridge.Unique.Profile.Wrapper.Attributes
{
    /// <summary>
    ///     Permissões por perfil
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleAuthorizeAttribute : ActionContextAttribute, IAuthorizationFilter
    {
        private readonly int[] _profileIds;

        /// <summary>
        /// </summary>
        public RoleAuthorizeAttribute(params int[] profileIds)
        {
            _profileIds = profileIds;
        }

        /// <summary>
        ///     Durante a authorização
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var profileId = (int)context.HttpContext.Items["profileId"];
            if (!_profileIds.Contains(profileId))
                throw new PermissionException((int)EBaseError.FORBIDDEN, BaseErrors.Forbidden);
        }
    }
}