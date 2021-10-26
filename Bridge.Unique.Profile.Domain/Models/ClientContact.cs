using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Models;

namespace Bridge.Unique.Profile.Domain.Models
{
    public class ClientContact : BaseModel<int>, IBaseModel
    {
        //public int Id { get; set; }
        public int ClientId { get; set; }
        public long ContactId { get; set; }

        public Client Client { get; set; }
        public Contact Contact { get; set; }
    }
}