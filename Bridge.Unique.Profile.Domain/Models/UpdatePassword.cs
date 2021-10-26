namespace Bridge.Unique.Profile.Domain.Models
{
    public class UpdatePassword : Token
    {
        public int ApplicationId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}