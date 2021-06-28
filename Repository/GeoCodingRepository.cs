using GeocodingApi.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeocodingApi.Repository
{
    public class GeoCodingRepository
    {
        private readonly GeoCodingContext _dbcontext;

        #region constructors
        public GeoCodingRepository() { }
        public GeoCodingRepository(GeoCodingContext dbcontext)
        {
            _dbcontext = dbcontext ?? default;
        }
        #endregion

        #region 
        public void SaveNewAddress(float latitude,float longitude,string requestaddress,string responseaddress)
        {
            _dbcontext.GeoAddresses.Add(new GeoAddress() { Latitude = latitude, Longitude = longitude, RequestAddress = requestaddress, ResponseAddress = responseaddress });
            _dbcontext.SaveChanges();
        }

        public GeoAddress FindAddress(string address)
        {
           return _dbcontext.GeoAddresses.Count() == 0 ? null : _dbcontext.GeoAddresses.OrderBy(a => a.Id).Where(g => g.RequestAddress == address).FirstOrDefault();
        }
        #endregion
    }
}
