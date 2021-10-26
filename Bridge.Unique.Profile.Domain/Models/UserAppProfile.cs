namespace Bridge.Unique.Profile.Domain.Models
{
    public class UserAppProfile
    {
        public UserAppProfile()
        {
        }

        public UserAppProfile(string apiClientCode, int profile)
        {
            ApiClientCode = apiClientCode;
            Profile = profile;
        }

        public string ApiClientCode { get; set; }
        public int Profile { get; set; }
    }
}