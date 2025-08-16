using Entities;
using ServiceContracts.DTO.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Extensions
{
    public static class CountryExtensions
    {
        public static Country ToEntity(this CountryAddRequest countryAddRequest)
        {
            return new Country() 
            {
                CountryName = countryAddRequest.CountryName
            };
        }

        public static CountryResponse ToResponse(this Country country) 
        {
            return new CountryResponse()
            {
                CountryName = country.CountryName,
                CountryID = country.Id,
            };
        }
    }
}
