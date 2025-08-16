using Entities;
using ServiceContracts;
using ServiceContracts.DTO.Country;
using ServiceContracts.Extensions;

namespace Services
{
    public class CountryService : ICountriesService
    {
        private readonly List<Country> _contriesList;

        public CountryService()
        {
            this._contriesList = new List<Country> { };
        }

        public CountryResponse AddEntity(CountryAddRequest? request)
        {
            if(request == null) throw new ArgumentNullException(nameof(request));

            if(request.CountryName is null) throw new ArgumentException(nameof(request.CountryName));

            if(_contriesList.Any(country => country.CountryName.Equals(request.CountryName)))
            {
                throw new ArgumentException(nameof(request.CountryName));
            }


            
            Country countryToAdd = request.ToEntity();


            countryToAdd.Id = Guid.NewGuid();


            _contriesList.Add(countryToAdd);

            return countryToAdd.ToResponse();

        }

        public List<CountryResponse> GetAllEntites()
        {
            return this._contriesList.Select(country => country.ToResponse()).ToList();
 
        }

        public CountryResponse? GetEntityById(Guid? countryId)
        {

            if(countryId is null)
            {
                return null;
            }
            return this._contriesList.FirstOrDefault(country => country.Id == countryId)?.ToResponse();
        }
    }
}
