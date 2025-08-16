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
    /// Acts as a DTO for inserting a new Person in the Table Person of the DB
    /// </summary>
    public record PersonAddRequest
    {
        [Required(ErrorMessage ="Person name can't be blank")]
        public string? PersonName { get; set; }

        [EmailAddress(ErrorMessage ="Email value should be a valid email")]
        [Required(ErrorMessage ="Email can't be blank")]
        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
    }
}
