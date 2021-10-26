using Microsoft.AspNetCore.Http;

namespace Bridge.Unique.Profile.API.Helpers
{
    /// <summary>
    ///     Helper de Host
    /// </summary>
    public static class HostHelper
    {
        /// <summary>
        ///     Busca a url do host no qual a api está
        /// </summary>
        /// <param name="request">A requisição atual no contexto</param>
        /// <returns>Scheme + Host + port no formato protocol://host:port</returns>
        public static string GetHostUrl(HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}";
        }
    }
}