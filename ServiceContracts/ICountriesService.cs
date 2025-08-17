using ServiceContracts.DTO.Country;

namespace ServiceContracts
{
    /// <summary>
    /// Represent business logic for manipulating the Country Entity
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a Country object to the Table Country in the DB
        /// </summary>
        /// <param name="request">Country object to be added</param>
        /// <returns>Returns the country object after adding it (including new generated Country ID)</returns>
        CountryResponse AddEntity(CountryAddRequest? request);


        /// <summary>
        /// Returns all countries from DB
        /// </summary>
        /// <returns>Returns all countries from DB as a List of CountryResponse </returns>
        List<CountryResponse> GetAllEntites();



        /// <summary>
        /// Return a country object based on the given vountry id
        /// </summary>
        /// <param name="countryId">Country Id (guid) to search</param>
        /// <returns>Matching country as CountryResponse object</returns>
        CountryResponse? GetEntityById(Guid? countryId);


        CountryResponse? GetEntityByName(string name);
    }
}
