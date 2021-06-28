using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeocodingApi.Models.Json
{
    public class JsonGeocodingBaseModel
    {
        [JsonProperty("results")]
        public List<JsonGeocodingResult> Results { get; set; }
        [JsonProperty("status")]
        public string JsonStatus { get; set; }
        [JsonProperty("error_message")]
        public string JsonErrorMessage { get; set; }
    }

    public class JsonGeocodingResult
    {
        [JsonProperty("geometry")]
        public JsonGeometryModel GeoGeometry { get; set; }
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; } // this is the response address received from the api.
    }
    public class JsonGeometryModel
    {
        [JsonProperty("location")]
        public JsonLocationModel Location { get; set; }
    }
    public class JsonLocationModel
    {
        [JsonProperty("lat")]
        public float Latitude { get; set; }
        [JsonProperty("lng")]
        public float Longitude { get; set; }
    }
}
