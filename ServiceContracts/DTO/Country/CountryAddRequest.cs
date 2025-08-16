using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Country
{
    /// <summary>
    /// DTO record for adding a new Country
    /// </summary>
    public record CountryAddRequest
    {
        public string? CountryName { get; set; }

        //public Country ToEntity()
        //{
        //    return new Country { CountryName = CountryName };
        //}
    }
}
    