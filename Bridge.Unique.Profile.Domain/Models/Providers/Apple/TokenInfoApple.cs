namespace Bridge.Unique.Profile.Domain.Models.Providers.Apple
{
    public class TokenInfoApple
    {
        /// <summary>
        ///     (Reserved for future use) A token used to access allowed data. Currently,
        ///     no data set has been defined for access.
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        ///     The amount of time, in seconds, before the access token expires.
        /// </summary>
        public long expires_in { get; set; }

        /// <summary>
        ///     A JSON Web Token that contains the user’s identity information.
        /// </summary>
        public string id_token { get; set; }

        /// <summary>
        ///     The refresh token used to regenerate new access tokens. Store this token securely on your server.
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        ///     The type of access token. It will always be bearer.
        /// </summary>
        public string token_type { get; set; }
    }
}