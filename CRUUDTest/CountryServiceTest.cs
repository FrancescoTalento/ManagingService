using ServiceContracts;
using ServiceContracts.DTO.Country;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUUDTest
{
    public class CountryServiceTest
    {
        private readonly ICountriesService _service;

        //Constructor
        public CountryServiceTest()
        {
            _service = new CountryService(false);
        }
        
        
        #region CountryAdd
        //When the CountryAddRequest is null it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() => 
                {
            //Act
                    _service.AddEntity(request);
                }
            );



        }
        //When the CountryName is null it should throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            //Arrange
            CountryAddRequest request = new CountryAddRequest()
            {
                CountryName = null
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.AddEntity(request);
            });

        }

        //When the CountryName is duplicated it should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName() 
        {
            //Arange
            CountryAddRequest request1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest request2 = new CountryAddRequest() { CountryName = "USA" };


            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
            //Act
                _service.AddEntity(request1);
                _service.AddEntity(request2);
            });

        }



        //When u supply proper country name, it should insert (add) the Country to the existing list of countries;
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            //Arrange
            CountryAddRequest request = new CountryAddRequest() {CountryName = "Brazil" };



            //Act
            CountryResponse response = _service.AddEntity(request);
            List<CountryResponse> responseList = this._service.GetAllEntites();


            //Assert
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, responseList);
        }

        #endregion

        #region GetAllCountries

        //The list of countries should be empty by default (before adding aany countries)
        [Fact]
        public void GetAllCountries_EmptyList()
        {
            //Arrange
            //Act
            List<CountryResponse> listCountries = this._service.GetAllEntites();

            //Asssert
            Assert.Empty(listCountries);
        }


        //The list of countries should return the countries I added
        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            //Arrange
            List<CountryAddRequest> listToAdd = new List<CountryAddRequest>
            {
                new CountryAddRequest() { CountryName = "USA"},
                new CountryAddRequest() { CountryName = "Brazil"},
                new CountryAddRequest() { CountryName = "Italy"}
            };

            List<CountryResponse> actualListOfResonses = new List<CountryResponse>();

            //Act
            foreach (CountryAddRequest item in listToAdd)
            {
               actualListOfResonses.Add(this._service.AddEntity(item));
            }

            List<CountryResponse> listOfResponses= this._service.GetAllEntites();


            //Assert
            foreach (var country in listOfResponses)
            {
                Assert.Contains(country, actualListOfResonses);
            }
        }


        #endregion

        #region GetCountryById


        //If we supply a null contryId, it should return null as CountryResponse
        [Fact]
        public void GetCountryByCountryID_NullCountryID()
        {
            //Arrange
            //Act
            CountryResponse? country_getById_response  = this._service.GetEntityById(null);

            //Assert
            Assert.Null(country_getById_response);
        }

        //If we supply a valid country id, it should return the matching country details as CountryResponse object
        [Fact]
        public void GetCountryByCountryID_ValidCountryID()
        {

            //Arrange
            CountryAddRequest countryAdd = new CountryAddRequest() { CountryName = "Brazil" };
            CountryResponse country_response_from_add = this._service.AddEntity(countryAdd);

            //Act
            CountryResponse country_response_from_getId = this._service.GetEntityById(country_response_from_add.CountryID);

            //Asert
            Assert.Equal(country_response_from_add, country_response_from_getId);
            
        }

        #endregion

       

    }
}
