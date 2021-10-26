using System;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.API.Controllers
{
    /// <summary>
    ///     Checagem de integridade da api
    /// </summary>
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HealthController : Controller
    {
        /// <summary>
        ///     Checa a integridade da api
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return $"Ok! Compilado em: {Environment.GetEnvironmentVariable("BUILD_VERSION")}";
        }
    }
}