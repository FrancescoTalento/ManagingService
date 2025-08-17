using Entities;
using ServiceContracts;
using ServiceContracts.DTO.Country;
using ServiceContracts.Extensions;

namespace Services
{
    public class CountryService : ICountriesService
    {
        private readonly List<Country> _contriesList;

        public CountryService(bool initialize = true)
        {
            this._contriesList = new List<Country> { };
            if (initialize) 
            {
                this._contriesList.Add(new Country {
                    Id = Guid.Parse("6222957a-189c-44fd-b974-0d279ab9bd17"),
                    CountryName = "USA"
                });
                this._contriesList.Add(new Country {
                    Id = Guid.Parse("a0f9a66f-0896-4f88-aaac-3d43e031f281"),
                    CountryName = "UK"
                });
                this._contriesList.Add(new Country {
                    Id = Guid.Parse("17df7fd8-3645-4e59-96ce-44aba5919808"),
                    CountryName = "Italy"
                });
                this._contriesList.Add(new Country {
                    Id = Guid.Parse("ad852a33-4100-4d4d-83a9-73c26308355d"),
                    CountryName = "India"
                });
                this._contriesList.Add(new Country {
                    Id = Guid.Parse("bd930371-8859-44ce-9f27-b1729fd48b09"),
                    CountryName = "Australia"
                });
                this._contriesList.Add(new Country {
                    Id = Guid.Parse("ffca6bfd-6748-41ea-9625-b8c5dc9d546e"),
                    CountryName = "Brazil"
                });
            }
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

        public CountryResponse? GetEntityByName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException();

            return _contriesList.FirstOrDefault(
                c => c.CountryName!= null &&
                c.CountryName.Equals(name,StringComparison.OrdinalIgnoreCase))?
                .ToResponse();
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
