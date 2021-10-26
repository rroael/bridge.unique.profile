namespace Bridge.Unique.Profile.Domain.Models.Providers.Facebook
{
    public class DebugTokenFacebook
    {
        public DataDebugTokenFacebook data { get; set; }
    }

    public class DataDebugTokenFacebook
    {
        public string app_id { get; set; }
        public string type { get; set; }
        public string application { get; set; }
        public bool is_valid { get; set; }
        public string user_id { get; set; }
    }
}