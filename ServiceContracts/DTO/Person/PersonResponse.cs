using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Person
{
    /// <summary>
    /// Represent DTO record that is used as return type of most methods of Persons Service
    /// </summary>
    public record PersonResponse
    {
        public Guid PersonID { get; set; }

        [Display(Name ="Person Name")]
        public string? PersonName { get; set; }
        
        public string? Email { get; set; }

        [Display(Name ="Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        
        public string? Country { get; set; }
        
        public string? Address { get; set; }

        [Display(Name ="Receive News Letters")]
        public bool ReceiveNewsLetters { get; set; }
        
        public double? Age { get; set; }

    }
}
