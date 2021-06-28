using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeocodingApi.classes
{
    public class ApiAccess
    {
        /*
         * "INVALID_REQUEST" generally indicates that the query (address, components or latlng) is missing.
         * "REQUEST_DENIED" indicates that your request was denied.
         * "ZERO_RESULTS" indicates that the geocode was successful but returned no results. This may occur if the geocoder was passed a non-existent address.
         * "UNKNOWN_ERROR" indicates that the request could not be processed due to a server error. The request may succeed if you try again.
         */
        public enum Statuscodes { OK, REQUEST_DENIED, ZERO_RESULTS, UNKNOWN_ERROR }

        const string GEOCODINGAPI_BASE_URL = @"https://maps.googleapis.com/maps/api/geocode/";
        const string GEOCODINGAPI_LICENSEKEY = @"AIzaSyCgFXmMk4Po0QHunhAxBrjwu4vZN7Czfj0"; 

        /// <summary>
        /// build the the full endpoint of string for geocoding api. 
        /// </summary>
        /// <param name="address">the address of a location</param>
        /// <returns>the full endpoint for the geocoding api</returns>
        internal string GetGeocodingEndpoint(string address)
        {
            return GEOCODINGAPI_BASE_URL + "json?address=" + address + "&key=" + GEOCODINGAPI_LICENSEKEY;
        }

        /// <summary>
        /// The function compares the matching error-codes and returns a message as per the error-code .
        /// </summary>
        /// <param name="statuscode"></param>
        /// <param name="error_message"></param>
        /// <returns>the error message to be displayed to the user</returns>
        internal string VerifyErrorStatus(string statuscode, string error_message = null)
        {
            if (statuscode == Statuscodes.REQUEST_DENIED.ToString())
            {
                return $"{Statuscodes.REQUEST_DENIED.ToString()} - {error_message}";
            }
            else if (statuscode == Statuscodes.UNKNOWN_ERROR.ToString())
            {
                return $"{Statuscodes.UNKNOWN_ERROR.ToString()} - {error_message}";
            }
            else if (statuscode == Statuscodes.ZERO_RESULTS.ToString())
            {
                error_message = error_message == null ? "The geocode was successful but returned no results" : error_message;
                return $"{Statuscodes.ZERO_RESULTS.ToString()} - {error_message}";
            }
            else
            {
                return $"{statuscode} - Error status is unknown. Please check documentation";
            }
        }
        internal string VerifyInputParametersValues(string streetname,string streetnumber,string zipcode,string city,string country)
        {
            if (string.IsNullOrEmpty(streetname))
                return "Streetname cannot be empty";
            else if (string.IsNullOrEmpty(streetnumber))
                return "Street Number cannot be empty";
            else if (string.IsNullOrEmpty(zipcode))
                return "Zipcode cannot be empty";
            else if (string.IsNullOrEmpty(city))
                return "City cannot be empty";
            else if (string.IsNullOrEmpty(country))
                return "Country cannot be empty";
            else return null;
        }
    }
}
