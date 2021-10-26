using Bridge.Unique.Profile.Communication.Enums;

namespace Bridge.Unique.Profile.Domain.Models.Providers
{
    public class Provider
    {
        public EAuthProvider ProviderAuth { get; set; }
        public string ProviderUserId { get; set; }
        public string ProviderToken { get; set; }

        public string ProviderClientSecret { get; set; }
        public bool IsLoginWeb { get; set; }
    }
}