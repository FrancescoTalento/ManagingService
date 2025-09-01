using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTO.Country;
using ServiceContracts.DTO.Person;
using ServiceContracts.Enums;
using ServiceContracts.Extensions;
using Services.Helper;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace xTest1.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class PersonsController : Controller
    {
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;

        private readonly Dictionary<string, string> _searchField = new Dictionary<string, string>();
        private readonly PropertyInfo[] _entityProperties;

        public PersonsController(IPersonsService personsService, ICountriesService countriesService)
        {
            this._personsService = personsService;
            _countriesService = countriesService;
            
            this._entityProperties = typeof(PersonResponse).GetProperties();
            this.SetSearchField();
        }
        private void SetSearchField()   
        {
            foreach (PropertyInfo prop in this._entityProperties)
            {
                var attribute = prop.GetCustomAttribute<DisplayAttribute>();
                if (attribute != null)
                {

                    this._searchField.Add(prop.Name, attribute.Name!);
                    continue;
                }
                if (prop.Name.Contains("id", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                this._searchField.Add(prop.Name, prop.Name);
            }

        }
        private void LoadCountries(Guid? selectedCountryId = null)
        {
            if(selectedCountryId != null) 
            {
                ViewBag.Countries = this._countriesService.GetAllEntites()
                    .Select(c => 
                    {
                        if(c.CountryID == selectedCountryId)
                        {
                            return new SelectListItem()
                            {
                                Selected = true,
                                Text = c.CountryName,
                                Value = c.CountryID.ToString()
                            };
                        }
                        return new SelectListItem()
                        {
                            Text = c.CountryName,
                            Value = c.CountryID.ToString()
                        };
                    });
            }

            ViewBag.Countries = this._countriesService.GetAllEntites()?
                .Select(c => new SelectListItem()
                {
                    Text = c.CountryName,
                    Value = c.CountryID.ToString()
                });
        }
        

        [Route("/")]
        [Route("[action]")]
        [Route("index")]
        public IActionResult PersonIndex(
            string searchBy, 
            string? filter,
            string? sortBy,
            SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {   
            
            List<PersonResponse> personList = this._personsService.GetAllEntites();


            if (!string.IsNullOrEmpty(filter))
            {
                personList = this._personsService.GetFilteredEntity(searchBy,filter);
            }
            if (!string.IsNullOrEmpty(sortBy))
            {
                personList = this._personsService.GetSortedEntities(
                    personList,
                    p => typeof(PersonResponse).GetProperty(sortBy)!.GetValue(p),
                    sortOrder
                    );
            }
           
            #region ViewBags
            ViewBag.SearchFields = this._searchField;
            ViewBag.PropertyInfo = this._entityProperties;

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchValue = filter;
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();
            #endregion

            return View("PersonIndex", personList);
        }


        [Route("[action]")]
        [Route("CreatePerson")]
        [HttpGet]
        public IActionResult InvokeCreatePersonPage()
        {
            List<CountryResponse> countries = this._countriesService.GetAllEntites().ToList();
            
            this.LoadCountries();

            return View("CreatePerson");
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult CreatePerson(PersonAddRequest request)
        {
            if (!ModelState.IsValid)
            {
                this.LoadCountries();

                ViewBag.Errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .SelectMany(e => e.ErrorMessage)
                    .ToList();
                    
                return View("CreatePerson",request);
            }

            this._personsService.AddEntity(request);
            return RedirectToAction("PersonIndex");
        }


        [Route("[action]/{personId}")]
        [Route("edit")]
        [HttpGet]
        public IActionResult EditPerson(Guid personId)
        {
            var personToEdit = this._personsService.GetEntityById(personId);

            if (personToEdit == null) { return RedirectToAction("PersonIndex"); }

            this.LoadCountries(personToEdit.CountryID);   

            return View("EditPerson",personToEdit.ToUpdateRequest());
        }

        [Route("[action]/{personId}")]
        [Route("edit")]
        [HttpPost]
        public IActionResult EditPerson(Guid personId, PersonUpdateRequest personUpdated)
        {
            if (ModelState.IsValid) 
            {
                this._personsService.UpdateEntity(personUpdated);
                return RedirectToAction("PersonIndex");
            }

            this.LoadCountries(personUpdated.CountryID);
            //ViewBag.Erros = ModelState.Values
            //    .SelectMany(e => e.Errors)
            //    .SelectMany(e => e.ErrorMessage)
            //    .ToList();
            //ViewBag.Countries = this._countriesService.GetAllEntites()
            //    .Select(c => 
            //    {
            //        if (c.CountryID == personUpdated.CountryID) 
            //        {
            //            return new SelectListItem()
            //            {
            //                Text = c.CountryName,
            //                Value = c.CountryID.ToString(),
            //                Selected = true

            //            };
            //        }
            //        return new SelectListItem()
            //        {
            //            Text = c.CountryName,
            //            Value = c.CountryID.ToString(),

            //        };
            //    });
            return View("EditPerson", personUpdated);
        }

        [Route("[action]/{personId}")]
        [Route("delete")]
        [HttpGet]
        public IActionResult DeletePerson(Guid personId)
        {
            var person = this._personsService.GetEntityById(personId);
            if (person == null) 
            {
                return RedirectToAction("PersonIndex");
            }

            return View("DeletePerson", person?.ToUpdateRequest());
        }

        [Route("[action]/{personid}")]
        [Route("delete")]
        [HttpPost]
        public IActionResult DeletePerson(PersonUpdateRequest personToDelete)
        {
            if (!this._personsService.DeleteEntity(personToDelete.PersonId))
            {
                return RedirectToAction("PersonIndex");
            }

            
            return RedirectToAction("PersonIndex");
        }
    }
}
