using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Person
{
    /// <summary>
    /// Represent a DTO class that contains the person details to update 
    /// </summary>
    public record PersonUpdateRequest
    {
        [Required(ErrorMessage="Person ID can't be blank")]
        public Guid PersonId { get; set; }


        [Required(ErrorMessage = "Person name can't be blank")]
        public string? PersonName { get; set; }

        [EmailAddress(ErrorMessage = "Email value should be a valid email")]
        [Required(ErrorMessage = "Email can't be blank")]
        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
    }
}
