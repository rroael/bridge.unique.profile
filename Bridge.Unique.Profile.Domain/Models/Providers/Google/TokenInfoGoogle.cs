namespace Bridge.Unique.Profile.Domain.Models.Providers.Google
{
    public class TokenInfoGoogle
    {
        public string aud { get; set; } //key da aplicação na google
        public string sub { get; set; } //user_id
        public string email { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public string email_verified { get; set; }
        public string typ { get; set; }
    }
}