using Entities;
using ServiceContracts;
using ServiceContracts.DTO.Country;
using ServiceContracts.DTO.Person;
using ServiceContracts.Enums;
using ServiceContracts.Extensions;
using Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PersonService : IPersonsService
    {

        private readonly List<Person> _personList;
        private readonly ICountriesService _countriesService;

        public PersonService()
        {
            _personList = new List<Person>();
            _countriesService = new CountryService();
        }

        public PersonResponse AddEntity(PersonAddRequest? request)
        {
            ValidationHelper.ModelValidation(request);
            if (string.IsNullOrEmpty(request.PersonName)) throw new ArgumentException(nameof(request.PersonName));

            Person personToAdd = request.ToEntity();
            this._personList.Add(personToAdd);

            
            string? countryName = this._countriesService.GetEntityById(request.CountryID)?.CountryName;

            return personToAdd.ToResponse(countryName);
        }


        public List<PersonResponse> GetAllEntites()
        {
            List<PersonResponse> personResponses = new List<PersonResponse>();
            foreach (var item in _personList)
            {
                string? countryName = this._countriesService.GetEntityById(item.CountryID)?.CountryName;
                personResponses.Add(item.ToResponse(countryName));
            }
            return personResponses;
        }

        public PersonResponse? GetEntityById(Guid? personId)
        {
            if(personId == Guid.Empty || personId == null) return null;

            return this._personList.FirstOrDefault(p => p.PersonID ==  personId).ToResponse();
        }


        public List<PersonResponse> GetFilteredEntity(params Func<Person, bool>[] predicate)
        {
            var filteresList = this._personList;
            
            foreach (var filter in predicate)
            {
                    filteresList = filteresList.Where(p => p!= null && filter(p)).ToList();
            }

            return filteresList.Select(p => p.ToResponse()).ToList();
        }


        public List<PersonResponse> GetSortedEntities<TKey>(List<PersonResponse> entities, Func<PersonResponse, TKey> keySelector, SortOrderOptions sortOrder, bool nullLast = true)
        {
            switch (sortOrder)
            {
                case SortOrderOptions.ASC:
                    if (nullLast)
                    {
                        return entities.OrderBy(keySelector, Comparer<TKey>.Default.NullsLast()).ToList();
                    }
                    return entities.OrderBy(keySelector, Comparer<TKey>.Default).ToList();
                    
                    break;
                case SortOrderOptions.DESC:
                    if (nullLast)
                    {
                        return entities.OrderBy(keySelector, Comparer<TKey>.Default.NullsLast()).ToList();
                    }
                    return entities.OrderBy(keySelector, Comparer<TKey>.Default).ToList();
                    break;
                default:
                    break;
            }
            return entities;
        }

        public PersonResponse UpdateEntity(PersonUpdateRequest? request)
        {
            if(request is null) throw new ArgumentNullException(nameof(request));
            ValidationHelper.ModelValidation(request);
            
            PersonResponse? originalPerson = this.GetEntityById(request.PersonId);
            if (originalPerson is null) throw new KeyNotFoundException(nameof(request.PersonId));

            CountryResponse? countryResponse = this._countriesService.GetEntityById(request.CountryID);
                //?? throw new KeyNotFoundException(nameof(request.CountryID));
            return request.ToResponse();
        }
        
        
        public bool DeleteEntity(Guid? personId)
        {
            if (personId is null) throw new ArgumentNullException();
            PersonResponse? entityTodelete = this.GetEntityById(personId);

            if (entityTodelete is null) return false;

            this._personList.RemoveAll(temp => temp.PersonID == personId);
            
            return true;
        }
    }
}
