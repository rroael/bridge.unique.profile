using System;
using System.Collections.Generic;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Models;

namespace Bridge.Unique.Profile.Domain.Models
{
    public class Api : BaseModel<int>, IBaseModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan AccessTokenTtl { get; set; }
        public TimeSpan RefreshTokenTtl { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool Active { get; set; }

        public List<ApiClient> Clients { get; set; }
    }
}