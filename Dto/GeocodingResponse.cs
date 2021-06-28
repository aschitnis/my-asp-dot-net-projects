using GeocodingApi.Models;
using GeocodingApi.Models.Json;
using System.Linq;

namespace GeocodingApi.Dto
{
    public class GeocodingResponse {

        public float Latitude {get; set;}

        public float Longitude {get; set;}

        public string Address {get; set;}

        public GeocodingResponse() 
        { 
            this.Latitude = 0.0f; 
            this.Longitude = 0.0f;  
            this.Address = null; 
        }

        public GeocodingResponse(JsonGeocodingBaseModel jsonmodel)
        {
            Latitude  = jsonmodel.Results.First().GeoGeometry.Location.Latitude;
            Longitude = jsonmodel.Results.First().GeoGeometry.Location.Longitude;
            Address = jsonmodel.Results.First().FormattedAddress;
        }

        public static implicit operator GeocodingResponse(JsonGeocodingBaseModel jsonmodel)
        {
            GeocodingResponse responsemodel = new GeocodingResponse(jsonmodel);
            return responsemodel;
        }
    }

}