using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GeocodingApi.Dto;
using GeocodingApi.Models;
using System.Net.Http;
using GeocodingApi.classes;
using Newtonsoft.Json;
using System.Threading.Tasks;
using GeocodingApi.Models.Json;
using GeocodingApi.Models.Db;
using System.Linq;
using GeocodingApi.Repository;

namespace GeocodingApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GeocodingController : ControllerBase
    {
        private readonly ILogger<GeocodingController> _logger;
        private readonly ApiAccess _apiaccess;
        private readonly GeoCodingRepository _dbRepository;

        public GeocodingController(ILogger<GeocodingController> logger, GeoCodingContext context = null)
        {
            _logger = logger;
            _apiaccess = new ApiAccess();
            _dbRepository = new GeoCodingRepository(context);
        }

        [HttpPost]
        public async Task<IActionResult> Get(GeocodingRequest request)
        {
            string message = _apiaccess.VerifyInputParametersValues(request.StreetName, request.StreetNumber, request.ZipCode, request.City, request.Country);
            if (message != null)
                return BadRequest($"Error - {message}");

            string requestAddress = (request.StreetName + " " + request.StreetNumber + "," + request.ZipCode + " " + request.City + "," + request.Country).ToLower();
            GeoAddress geocodingaddressFromDb = _dbRepository.FindAddress(requestAddress);

            // IF case 
            // Either of the following cases apply if the GeoAddress object is NULL 
            //  i) there are no records(empty) in the database table or
            // ii) the matching address in the database table is not found
            // ELSE case
            // i) the matching address string is found in the database. 
            //      In this case the geocoding webservice should not be called.
            if (geocodingaddressFromDb == null)
            {
                string geocodingRequestUriString = _apiaccess.GetGeocodingEndpoint(requestAddress);

                using (var client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(geocodingRequestUriString);
                        response.EnsureSuccessStatusCode(); //The EnsureSuccessStatusCode method throws an exception if the HTTP response was unsuccessful.

                        string jsonResult = await response.Content.ReadAsStringAsync();
                        JsonGeocodingBaseModel jsonObjectResult = JsonConvert.DeserializeObject<JsonGeocodingBaseModel>(jsonResult);

                        if (jsonObjectResult.JsonStatus == ApiAccess.Statuscodes.OK.ToString())
                        {
                            GeocodingResponse geocodingResponse = jsonObjectResult; // implicit conversion

                            _dbRepository.SaveNewAddress(geocodingResponse.Latitude, geocodingResponse.Longitude, requestAddress, geocodingResponse.Address);
                            return Ok(geocodingResponse);
                        }
                        else
                        {
                            string errormessage = _apiaccess.VerifyErrorStatus(jsonObjectResult.JsonStatus, jsonObjectResult.JsonErrorMessage);
                            return BadRequest($"{errormessage}");
                        }
                    }
                    catch (HttpRequestException httpRequestException)
                    {
                        return BadRequest($"Error getting Lat/Lon geo.cordinates from GeoCoding API: {httpRequestException.Message}");
                    }
                }
            }
            else
            {
                // get the address-data found in the database and display it to the user.
                GeocodingResponse geocodingResponse = new GeocodingResponse();
                geocodingResponse.Latitude = geocodingaddressFromDb.Latitude;
                geocodingResponse.Longitude = geocodingaddressFromDb.Longitude;
                geocodingResponse.Address = geocodingaddressFromDb.ResponseAddress;

                return Ok(geocodingResponse);
            }
        }
    }
}
