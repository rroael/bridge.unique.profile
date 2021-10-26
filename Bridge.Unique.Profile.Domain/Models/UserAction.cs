namespace Bridge.Unique.Profile.Domain.Models
{
    public class UserAction
    {
        public int UserId { get; set; }
        public bool Action { get; set; }
        public string ApiCode { get; set; }
        public int ClientId { get; set; }
        public int ApiClientId { get; set; }
    }
}