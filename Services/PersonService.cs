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

        public PersonService(bool initialize = true)
        {
            _personList = new List<Person>();
            _countriesService = new CountryService();
            if (initialize) 
            {
                this._personList.Add(new Person
                {
                    CountryID = Guid.Parse("6222957a-189c-44fd-b974-0d279ab9bd17"),
                    PersonID = Guid.Parse("dc46c146-cbc9-4179-a399-bc2c4fe060e1"),
                    PersonName = "Zilvia",
                    Email = "zwickett0@networksolutions.com",
                    DateOfBirth = new DateTime(1924, 10, 20),
                    Gender = "Female",
                    Address = "7 Express Parkway",
                    ReceiveNewsLetters = true
                });
                this._personList.Add(new Person
                {
                    CountryID = Guid.Parse("a0f9a66f-0896-4f88-aaac-3d43e031f281"),
                    PersonID = Guid.Parse("8213238f-e4cb-4665-8c45-a4882f0cf02c"),
                    PersonName = "Rachele",
                    Email = "rprophet1@aboutads.info",
                    DateOfBirth = new DateTime(1934, 9, 9),
                    Gender = "Female",
                    Address = "5699 Redwing Cente",
                    ReceiveNewsLetters = true
                });
                this._personList.Add(new Person
                {
                    CountryID = Guid.Parse("17df7fd8-3645-4e59-96ce-44aba5919808"),
                    PersonID = Guid.Parse("5bee9ceb-3b03-40fe-a19e-5fd39f17a1e9"),
                    PersonName = "Filippo",
                    Email = "fditch0@networkadvertising.org",
                    DateOfBirth = new DateTime(1902,01,09),
                    Gender = "Male",
                    Address = "891 Center Lane",
                    ReceiveNewsLetters = true
                });
                this._personList.Add(
                    new Person
                    {
                        CountryID = Guid.Parse("ad852a33-4100-4d4d-83a9-73c26308355d"),
                        PersonID = Guid.Parse("a9935e54-4149-452d-8fb3-9c14dc7a8042"),
                        PersonName = "Cullan",
                        Email = "cfurley2@time.com",
                        DateOfBirth = new DateTime(1951, 3, 15),
                        Gender = "Male",
                        Address = "793 Columbus Park",
                        ReceiveNewsLetters = true
                    });
                this._personList.Add(new Person
                {
                    CountryID = Guid.Parse("bd930371-8859-44ce-9f27-b1729fd48b09"),
                    PersonID = Guid.Parse("70faccfc-be73-4799-b8ee-f036548321d1"),
                    PersonName = "Anthiathia",
                    Email = "aliddle3@yahoo.co.jp",
                    DateOfBirth = new DateTime(1936, 7, 10),
                    Gender = "Female",
                    Address = "05 Hansons Way",
                    ReceiveNewsLetters = true
                });
                this._personList.Add(new Person
                {
                    CountryID = Guid.Parse("ffca6bfd-6748-41ea-9625-b8c5dc9d546e"),
                    PersonID = Guid.Parse("c931f977-f5c7-43e7-93ff-affb5a75737f"),
                    PersonName = "Jenna",
                    Email = "jvlasov4@ca.gov",
                    DateOfBirth = new DateTime(1972, 3, 6),
                    Gender = "Female",
                    Address = "83585 Cherokee Parkway\t",
                    ReceiveNewsLetters = false
                });
                this._personList.Add(new Person
                {
                    CountryID = Guid.Parse("bd930371-8859-44ce-9f27-b1729fd48b09"),
                    PersonID = Guid.Parse("ec18002f-a7ab-4e3b-a8dd-7ca175ed1e71"),
                    PersonName = "Garner",
                    Email = "gbeaze5@mac.com",
                    DateOfBirth = new DateTime(1933, 10, 18),
                    Gender = "Male",
                    Address = "37601 Del Mar Road",
                    ReceiveNewsLetters = false
                });
                this._personList.Add(new Person
                {
                    CountryID = Guid.Parse("17df7fd8-3645-4e59-96ce-44aba5919808"),
                    PersonID = Guid.Parse("1818cc28-e03d-4156-a957-f602b7e44b5c"),
                    PersonName = "Fransisco",
                    Email = "fshowering6@ning.com",
                    DateOfBirth = new DateTime(1913, 2, 7),
                    Gender = "Male",
                    Address = "017 Wayridge Trail",
                    ReceiveNewsLetters = true
                });
                this._personList.Add(new Person
                {
                    CountryID = Guid.Parse("ffca6bfd-6748-41ea-9625-b8c5dc9d546e"),
                    PersonID = Guid.Parse("42736b10-e140-42b1-b50b-da2e788c8eeb"),
                    PersonName = "Caryl",
                    Email = "cfitzjohn7@omniture.com",
                    DateOfBirth = new DateTime(1912, 8, 28),
                    Gender = "Male",
                    Address = "017 Wayridge Trail",
                    ReceiveNewsLetters = true
                });
                this._personList.Add(new Person
                {
                    CountryID = Guid.Parse("a0f9a66f-0896-4f88-aaac-3d43e031f281"),
                    PersonID = Guid.Parse("2a43f521-267b-4e9d-b12c-a788b6851586"),
                    PersonName = "Ester",
                    Email = "emcgarrity8@timesonline.co.uk",
                    DateOfBirth = new DateTime(1959, 7, 24),
                    Gender = "Female",
                    Address = "85 Columbus Junction",
                    ReceiveNewsLetters = true
                });
                this._personList.Add(new Person
                {
                    CountryID = Guid.Parse("ad852a33-4100-4d4d-83a9-73c26308355d"),
                    PersonID = Guid.Parse("c249ff24-1035-46b3-95a5-83f8bc9e8521"),
                    PersonName = "Josselyn",
                    Email = "jswan9@exblog.jp",
                    DateOfBirth = new DateTime(1985, 6, 8),
                    Gender = "Female",
                    Address = "1646 Jenifer Court",
                    ReceiveNewsLetters = true
                });
            };
        }

        public PersonResponse AddEntity(PersonAddRequest? request)
        {
            if (string.IsNullOrEmpty(request?.PersonName)) throw new ArgumentException(nameof(request.PersonName));
            //ValidationHelper.ModelValidation(request);

            Person personToAdd = request.ToEntity();
            this._personList.Add(personToAdd);

            
            CountryResponse? countryRelated = this._countriesService.GetEntityById(request.CountryID);

            return personToAdd.ToResponse(countryRelated);
        }


        public List<PersonResponse> GetAllEntites()
        {
            List<PersonResponse> personResponses = new List<PersonResponse>();
            foreach (var item in _personList)
            {
                CountryResponse? countryRelated = this._countriesService.GetEntityById(item.CountryID);
                personResponses.Add(item.ToResponse(countryRelated));
            }
            return personResponses;
        }

        public PersonResponse? GetEntityById(Guid? personId)
        {
            if(personId == Guid.Empty || personId == null) return null;

            return this._personList.FirstOrDefault(p => p.PersonID ==  personId).ToResponse();
        }


        public List<PersonResponse> GetFilteredEntity(string propName, string filterValue)
        {
            Func<Person, bool> predicate;
            if (propName.Equals("Age",StringComparison.OrdinalIgnoreCase))
            {
                if (!int.TryParse(filterValue, out var age))
                { return new List<PersonResponse>(); }

                string targetYear = (DateTime.Now.Year - age).ToString();

                predicate = FilterHelper.MakePredicate<Person>(nameof(Person.DateOfBirth),targetYear);
            }
            else if (propName.Equals("Country"))
            {
                var country = this._countriesService.GetEntityByName(filterValue);
                if (country == null) return new List<PersonResponse>();
                predicate = FilterHelper.MakePredicate<Person>(nameof(Person.CountryID), country.CountryID.ToString());
            }
            else
            {
                predicate = FilterHelper.MakePredicate<Person>(propName, filterValue);
            }


            var filteresList = this._personList.Where(p => p!= null && predicate(p));  
          

            return filteresList
                .Select(
                    p => p.ToResponse(
                        this._countriesService.GetEntityById(p.CountryID))
                    )
                .ToList();
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
                        return entities.OrderByDescending(keySelector, Comparer<TKey>.Default.NullsLast()).ToList();
                    }
                    return entities.OrderByDescending(keySelector, Comparer<TKey>.Default).ToList();
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
            
            Person? originalPerson = this._personList.FirstOrDefault(p=> p.PersonID == request.PersonId);
            if (originalPerson is null) throw new KeyNotFoundException(nameof(request.PersonId));



            originalPerson.PersonName = request.PersonName;
            originalPerson.Email = request.Email;
            originalPerson.DateOfBirth = request.DateOfBirth;
            originalPerson.Gender = request.Gender.ToString();
            originalPerson.CountryID = request.CountryID;
            originalPerson.Address = request.Address;
            originalPerson.ReceiveNewsLetters = request.ReceiveNewsLetters;

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
