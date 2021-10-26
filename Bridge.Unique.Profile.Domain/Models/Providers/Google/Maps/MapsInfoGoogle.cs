using System.Collections.Generic;

namespace Bridge.Unique.Profile.Domain.Models.Providers.Google.Maps
{
    public class MapsInfoGoogle
    {
        public List<MapsInfoGoogleResult> results { get; set; }
    }

    public class MapsInfoGoogleResult
    {
        public GeometryGoogle geometry { get; set; }
    }
}