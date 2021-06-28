using System.ComponentModel.DataAnnotations.Schema;

namespace GeocodingApi.Models.Db {
    [Table("tblgeoaddress")]
    public class GeoAddress {
        [Column("id")]
        public long Id { get; set; }
        [Column("latitude")]
        public float Latitude { get; set; }
        [Column("longitude")]
        public float Longitude { get; set; }

        [Column("request_address")]
        public string RequestAddress { get; set; }

        [Column("response_address")]
        public string ResponseAddress { get; set; }
    }
}