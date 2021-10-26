namespace Bridge.Unique.Profile.System.Settings
{
    public class Jwt
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Key { get; set; }
        public string SecurityAlgorithm { get; set; }
    }
}