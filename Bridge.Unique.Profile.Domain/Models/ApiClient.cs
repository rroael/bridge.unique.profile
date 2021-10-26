using System;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Models;

namespace Bridge.Unique.Profile.Domain.Models
{
    public class ApiClient : BaseModel<int>, IBaseModel
    {
        public string Code { get; set; }
        public string Token { get; set; }
        public int ApiId { get; set; }
        public int ClientId { get; set; }
        public bool IsAdmin { get; set; }
        public string Sender { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Active { get; set; }
        public string ApiKeyId { get; set; }
        public bool NeedsExternalApproval { get; set; }
        public string ExternalApproversEmail { get; set; }
        public Api Api { get; set; }
        public Client Client { get; set; }
    }
}