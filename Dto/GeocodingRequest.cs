namespace GeocodingApi.Dto
{
    public class GeocodingRequest
    {
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}