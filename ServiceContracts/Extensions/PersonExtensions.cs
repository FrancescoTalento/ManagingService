using Entities;
using ServiceContracts.DTO.Country;
using ServiceContracts.DTO.Person;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Extensions
{
    public static class PersonExtensions
    {

        /// <summary>
        /// Converts a PersonUpdateRequest DTO into a Person Entity
        /// </summary>
        /// <param name="personUpdateRequest"></param>
        /// <returns></returns>
        public static Person ToEntity(this PersonUpdateRequest personUpdateRequest)
        {
            return new Person
            {
                Address = personUpdateRequest.Address,
                CountryID = personUpdateRequest.CountryID,
                DateOfBirth = personUpdateRequest.DateOfBirth,
                Email = personUpdateRequest.Email,
                PersonID = personUpdateRequest.PersonId,
                PersonName = personUpdateRequest.PersonName,
                ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters,
                Gender =  personUpdateRequest.Gender.ToString(),
            };
        }
        /// <summary>
        /// Converts a PersonAddRequest DTO into a Person Entity
        /// </summary>
        /// <param name="personAddRequest"></param>
        /// <returns></returns>
        public static Person ToEntity(this PersonAddRequest personAddRequest)
        {
            return new Person
            {
                PersonName = personAddRequest.PersonName,
                Address = personAddRequest.Address,
                CountryID = personAddRequest.CountryID,
                DateOfBirth = personAddRequest.DateOfBirth,
                Email = personAddRequest.Email,
                Gender = personAddRequest.Gender.ToString(),
                PersonID = Guid.NewGuid(),
                ReceiveNewsLetters = personAddRequest.ReceiveNewsLetters,
            };
        }


        public static PersonResponse? ToResponse(this Person? person, CountryResponse? country = null)
        {
            if (person is null) return null;
            if (country is null)
            {
                return new PersonResponse()
                {
                    Address = person.Address,
                    DateOfBirth = person.DateOfBirth,
                    Email = person.Email,
                    PersonID = person.PersonID,
                    ReceiveNewsLetters = person.ReceiveNewsLetters,
                    Gender = person.Gender,
                    PersonName = person.PersonName,
                    Age = person.DateOfBirth != null ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null
                };
            }
            return new PersonResponse()
            {
                Address = person.Address,
                CountryID = country.CountryID,
                DateOfBirth = person.DateOfBirth,
                Email = person.Email,
                PersonID = person.PersonID,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Gender = person.Gender,
                PersonName = person.PersonName,
                Age = person.DateOfBirth != null ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null,
                Country = country.CountryName
            };
        }
        public static PersonResponse ToResponse(this PersonUpdateRequest request, string? CountryName = null)
        {
            if (CountryName == null)
            {
                return new PersonResponse()
                {
                    PersonName = request.PersonName,
                    Address = request.Address,
                    CountryID = request.CountryID,
                    DateOfBirth = request.DateOfBirth,
                    Email = request.Email,
                    ReceiveNewsLetters = request.ReceiveNewsLetters,
                    Gender = request.Gender.ToString(),
                    PersonID = request.PersonId,
                    Age = request.DateOfBirth != null ? Math.Round((DateTime.Now - request.DateOfBirth.Value).TotalDays / 365.25) : null,

                };
            }
            return new PersonResponse()
            {
                PersonName = request.PersonName,
                Address = request.Address,
                CountryID = request.CountryID,
                DateOfBirth = request.DateOfBirth,
                Email = request.Email,
                ReceiveNewsLetters = request.ReceiveNewsLetters,
                Gender = request.Gender.ToString(),
                PersonID = request.PersonId,
                Age = request.DateOfBirth != null ? Math.Round((DateTime.Now - request.DateOfBirth.Value).TotalDays / 365.25) : null,
                Country = CountryName,
            };
        }

        public static PersonUpdateRequest ToUpdateRequest(this Person person)
        {
            return new PersonUpdateRequest()
            {
                Address = person.Address,
                CountryID = person.CountryID,
                DateOfBirth = person.DateOfBirth,
                Email = person.Email,
                PersonId = person.PersonID,
                PersonName = person.PersonName,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Gender = Enum.Parse<GenderOptions>(person.Gender,true)
            };

        }
        public static PersonUpdateRequest ToUpdateRequest(this PersonResponse personResponse)
        {
            return new PersonUpdateRequest()
            {
                Address = personResponse.Address,
                CountryID = personResponse.CountryID,
                DateOfBirth = personResponse.DateOfBirth,
                Email = personResponse.Email,
                PersonId = personResponse.PersonID,
                PersonName = personResponse.PersonName,
                ReceiveNewsLetters = personResponse.ReceiveNewsLetters,
                Gender = Enum.Parse<GenderOptions>(personResponse.Gender,true)
            };

        }
    }
}
