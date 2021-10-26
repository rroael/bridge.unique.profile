using System.Collections.Generic;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Models;

namespace Bridge.Unique.Profile.Domain.Models
{
    public class Client : BaseModel<int>, IBaseModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Document { get; set; }
        public string Segment { get; set; }
        public bool Active { get; set; }
        public string Sender { get; set; }

        public List<Contact> Contacts { get; set; }
        public List<ApiClient> Apis { get; set; }
    }
}