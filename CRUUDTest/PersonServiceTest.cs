using ServiceContracts;
using ServiceContracts.DTO.Person;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.DTO.Country;
using Xunit.Abstractions;
using Entities;
using Services.Helper;
using ServiceContracts.Extensions;

namespace CRUUDTest
{
    public class PersonServiceTest
    {
        private readonly IPersonsService _service;
        private readonly ICountriesService _countryService;
        private readonly ITestOutputHelper _testOutputHelper;

        public PersonServiceTest(ITestOutputHelper testOutputHelper)
        {
            this._service = new PersonService();
            this._countryService = new CountryService();
            this._testOutputHelper = testOutputHelper;
        }

        #region HelperMethods
        private List<PersonResponse> CreatePersonDataForTest()
        {
            CountryAddRequest countryAdd1 = new CountryAddRequest() { CountryName = "Brazil" };
            CountryAddRequest countryAdd2 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest countryAdd3 = new CountryAddRequest() { CountryName = "China" };

            CountryResponse countryResponse1 = this._countryService.AddEntity(countryAdd1);
            CountryResponse countryResponse2 = this._countryService.AddEntity(countryAdd2);
            CountryResponse countryResponse3 = this._countryService.AddEntity(countryAdd3);

            PersonAddRequest personAdd1 = new PersonAddRequest()
            {
                Address = "Example Addres1",
                ReceiveNewsLetters = true,
                PersonName = "Person Name Mariana",
                Gender = GenderOptions.Male,
                Email = "example1@email.com",
                CountryID = countryResponse1.CountryID,
                DateOfBirth = DateTime.Parse("2000-08-01")
            };
            PersonAddRequest personAdd2 = new PersonAddRequest()
            {
                Address = "Example Addres2",
                ReceiveNewsLetters = true,
                PersonName = "Person Name Francesco",
                Gender = GenderOptions.Male,
                Email = "example2@email.com",
                CountryID = countryResponse2.CountryID,
                DateOfBirth = DateTime.Parse("2000-08-01")
            };
            PersonAddRequest personAdd3 = new PersonAddRequest()
            {
                Address = "Example Addres3",
                ReceiveNewsLetters = true,
                PersonName = "Person Name FrMa",
                Gender = GenderOptions.Male,
                Email = "example3@email.com",
                CountryID = countryResponse3.CountryID,
                DateOfBirth = DateTime.Parse("2000-08-01")
            };

            List<PersonAddRequest> personRequestlist = new List<PersonAddRequest>() { personAdd1, personAdd2, personAdd3 };
            List<PersonResponse> personResponsesListExpected = new List<PersonResponse>();

            foreach (var request in personRequestlist)
            {
                PersonResponse person = this._service.AddEntity(request);
                personResponsesListExpected.Add(person);
            }
            return personResponsesListExpected;
        }
        #endregion

        #region AddPerson
        //When we supply null value as PersonAddRequest, it should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson()
        {
            //Arrange
            PersonAddRequest? request = null;

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => this._service.AddEntity(request));
            
        }

        //When we supply a PersonName as null value, it should throw ArgumentException
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            PersonAddRequest request = new PersonAddRequest() { PersonName = null};

            Assert.Throws<ArgumentException>(() => this._service.AddEntity(request));
        }

        //When we supply a proper PersonDeatils, it should insert the Person into the
        //person's list. And it should return a an object of the PersonResponse with the newly generated PersonID
        [Fact]
        public void AddPerson_ProperPersonDetails() 
        {
            PersonAddRequest request = new PersonAddRequest()
            {
                Address = "Example Addres",
                ReceiveNewsLetters = true,
                PersonName = "Person Name",
                Gender = GenderOptions.Male,
                Email = "example@email.com",
                CountryID = Guid.NewGuid(),
                DateOfBirth = DateTime.Parse("2000-08-01")
            };

            PersonResponse expectedResponse = this._service.AddEntity(request);
            List<PersonResponse> actualListOfPersonResponse = this._service.GetAllEntites();

            Assert.Contains(expectedResponse, actualListOfPersonResponse);
        }

        #endregion

        #region GetPersonById

        //If we supply null as PersonID, it should return null as PersonResponse
        //NullPersonID
        [Fact]
        public void GetPersonById_NullPersonID()
        {
            //Arrange
            Guid? id = null;

            PersonResponse? response = this._service.GetEntityById(id);
            //Act
            //Assert
            Assert.Null(response);
        }


        //If we supply a valid PersonID, it should return the valid person details as PersonResponse object
        //WithPersonID
        [Fact]
        public void GetPersonById_WithPersonID()
        {
            //Arrange
            CountryAddRequest countryRequest = new CountryAddRequest() { CountryName = "Brazil" };
            CountryResponse countryResponse = this._countryService.AddEntity(countryRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                Address = "Example Addres",
                ReceiveNewsLetters = true,
                PersonName = "Person Name",
                Gender = GenderOptions.Male,
                Email = "example@email.com",
                CountryID = countryResponse.CountryID,
                DateOfBirth = DateTime.Parse("2000-08-01")
            };

            //Act
            PersonResponse expectedPerson = this._service.AddEntity(personAddRequest);
            PersonResponse actualPerson = this._service.GetEntityById(expectedPerson.PersonID);  

            //Assert
            Assert.Equal(expectedPerson, actualPerson);
        }

        #endregion

        #region GetAllPersons

        //The GetAllPersons() should return a empty list by default
        //EmptyList
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            //Act
            List<PersonResponse> listPersonResponse = this._service.GetAllEntites();
            //Assert
            Assert.Empty(listPersonResponse);
        }


        //Firts, we will add few persons; and then when we call GetAllPersons(), it should return the same persons that were added
        //AddFewPersons()
        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            //Arrange
            
            List<PersonResponse> personResponsesListExpected = this.CreatePersonDataForTest();

            //Act

            List<PersonResponse> personResponsesListActual = this._service.GetAllEntites();

            //Assert
            foreach (var personResponse in personResponsesListExpected)
            {
                Assert.Contains(personResponse, personResponsesListActual);
            }


            //Logging
            this._testOutputHelper.WriteLine("Actual:");
            foreach (var item in personResponsesListActual)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }

            this._testOutputHelper.WriteLine("Expected:");
            foreach (var item in personResponsesListExpected)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }

        }

        #endregion

        #region GetFilteredPersons
        
        //If the search text is empty and seach by is "PersonName", it should return all persons
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            //Arrange
            List<PersonResponse> personResponsesListExpected = this.CreatePersonDataForTest();


            //Act
            List<PersonResponse> personResponsesListActual = this._service.GetFilteredEntity(p => p.PersonName.Contains("", StringComparison.OrdinalIgnoreCase));

            //Assert

            foreach (var person in personResponsesListExpected)
            {
                Assert.Contains(person, personResponsesListActual);
            }

            //Logging
            this._testOutputHelper.WriteLine("Actual:");
            foreach (var item in personResponsesListActual)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }

            this._testOutputHelper.WriteLine("Expected:");
            foreach (var item in personResponsesListExpected)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }
        }


        //Firts we will add a few persons; than we will search based on the "PersonName" with some search string. It Should return the matching persons
        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
        {
            //Arrange
            List<PersonResponse> personResponsesListExpected = this.CreatePersonDataForTest();
            personResponsesListExpected = personResponsesListExpected.Where(p => p.PersonName != null && p.PersonName.Contains("ma",StringComparison.OrdinalIgnoreCase)).Select(p => p).ToList();

            //Act
            List<PersonResponse> personResponsesListActual = this._service.GetFilteredEntity(p => p.PersonName.Contains("ma",StringComparison.OrdinalIgnoreCase));

            foreach (var person in personResponsesListExpected)
            {
                if(person.PersonName is not null)
                {
                    if (person.PersonName.Contains("ma", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(person, personResponsesListActual);
                    }
                }
            }

            //Logging
            this._testOutputHelper.WriteLine("Actual:");
            foreach (var item in personResponsesListActual)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }

            this._testOutputHelper.WriteLine("Expected:");
            foreach (var item in personResponsesListExpected)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }
        }

        #endregion

        #region GetSortedPersons
        //When we sort base on the "PersonName" in the DESC order, it should return the persons list in the descending order on "PersonName"
        [Fact]
        public void GetSortedPersons_SearchByPersonName()
        {
            //Arrange
            List<PersonResponse> personResponsesListExpected = this.CreatePersonDataForTest();

            //Act
            List<PersonResponse> sortedPersonResponsesListActual = this._service.GetSortedEntities(personResponsesListExpected, p => p.PersonName,SortOrderOptions.ASC);

            //Assert
            List<PersonResponse> sortesPersonResponseListExpected = personResponsesListExpected.OrderBy(p => p.PersonName, Comparer<string>.Default.NullsLast()).ToList();

            for (int i = 0; i < sortedPersonResponsesListActual.Count; i++)
            {
                Assert.Equal(sortesPersonResponseListExpected[i], sortedPersonResponsesListActual[i]);
            }
            //Logging
            this._testOutputHelper.WriteLine("Actual:");
            foreach (var item in sortedPersonResponsesListActual)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }

            this._testOutputHelper.WriteLine("Expected:");
            foreach (var item in sortesPersonResponseListExpected)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }
        }
        #endregion

        #region UpdatePersons
        //When we supply null as PersonUpdateRequest, it should throw ArgumentNullException
        //NullPerson
        [Fact]
        public void UpdatePersons_PersonUpdateRequest()
        {
            //Arrange
            PersonUpdateRequest? personUpdateRequest = null;

            //Asset
            Assert.Throws<ArgumentNullException>(() =>
            {
                this._service.UpdateEntity(personUpdateRequest);
            });
        }

        //When we supply invalid "PersonID", it should throw KeyNotFoundException
        //InvalidPersonID
        [Fact]
        public void UpdatePersons_InvalidPersonID() 
        {
            CountryAddRequest countryAdd = new CountryAddRequest()
            {
                CountryName = "Brazil"
            };
            CountryResponse countryResponse = this._countryService.AddEntity(countryAdd);
            //Arrange
            PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest() 
            {
                PersonId = Guid.NewGuid(),
                PersonName = "Test",
                Address = "Addres",
                DateOfBirth = DateTime.Now,
                Email = "mail@email.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true,
                CountryID = countryResponse.CountryID,
            };

            //Assert
            Assert.Throws<KeyNotFoundException>(() => 
            {
                //Act
                //Assert
                this._service.UpdateEntity(personUpdateRequest);
            });
        }

        //When "PersonName" is null, it should throw ArgumentException
        //PersonNameIsNull 
        [Fact]
        public void UpdatePersons_PersonNameIsNull()
        {
            //Arrange
            PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest() 
            {
                PersonName = null
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                this._service.UpdateEntity(personUpdateRequest);
            });

        }


        //Firts add a new Person, than update the "PersonName" and "Email"
        //PersonFullDetailsUpdation
        [Fact]
        public void UpdatePersons_PersonFullDetailsUpdation()
        {
            //Arrange
            List<PersonResponse> listOfData = this.CreatePersonDataForTest();
            PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest()
            {
                PersonId = listOfData[0].PersonID,
                PersonName = "Nome atualizado",
                Email = "email@valido.com",
                CountryID = listOfData[0].CountryID,
                Address = listOfData[0].Address,
                DateOfBirth= listOfData[0].DateOfBirth,
                Gender = Enum.Parse<GenderOptions>(listOfData[0].Gender),
                ReceiveNewsLetters = listOfData[0].ReceiveNewsLetters
            };

            //Act
            PersonResponse actualUpdatedPerson = this._service.UpdateEntity(personUpdateRequest);
            PersonResponse expectedUpdatedPerson = personUpdateRequest.ToResponse();

            //Assert
            Assert.Equal(expectedUpdatedPerson,actualUpdatedPerson);

        }

        #endregion


        #region DeletePerson
        //If you supply an invalid "PersonID", it should return false
        //InvalidPersonID
        [Fact]
        public void DeletePerson_InvalidPersonID()
        {
            bool isDeleted = this._service.DeleteEntity(Guid.NewGuid());
            Assert.False(isDeleted);
        }

        //If you supply a valid "PersonID", it should return true
        //ValidPersonID
        [Fact]
        public void DeletePeron_ValidPersonID()
        {
            CountryAddRequest requestCountry = new CountryAddRequest() { CountryName = "Brazil" };
            CountryResponse responseCountry = this._countryService.AddEntity(requestCountry);

            PersonAddRequest requestPerson = new PersonAddRequest()
            {
                PersonName = "Test",
                Address = "Addres",
                DateOfBirth = DateTime.Now,
                Email = "mail@email.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true,
                CountryID = responseCountry.CountryID,
            };

            PersonResponse responsePerson = this._service.AddEntity(requestPerson);
            bool isDeleted = this._service.DeleteEntity(responsePerson.PersonID);

            Assert.True(isDeleted);
        }
        #endregion
    }
}
