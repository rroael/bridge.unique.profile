namespace Bridge.Unique.Profile.Domain.Models
{
    public class ClientUser
    {
        public Client Client { get; set; }
        public User User { get; set; }
        public string UserApiCodeToAssociate { get; set; }
    }
}