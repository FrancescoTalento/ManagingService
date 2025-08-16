using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Country
{
    /// <summary>
    /// DTO record that is used as a return type for most of the CountriesService methods
    /// </summary>
    public record CountryResponse
    {
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }
    }
}
