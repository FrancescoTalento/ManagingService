using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.Person;
using Services.Helper;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace xTest1.Controllers
{
    [Controller]
    public class PersonController : Controller
    {
        private readonly IPersonsService _personsService;
        private readonly Dictionary<string, string> _searchField = new Dictionary<string, string>();

        public PersonController(IPersonsService personsService)
        {
            this._personsService = personsService;
            this.SetSearchField();
        }
        private void SetSearchField() 
        {
            PropertyInfo[] properties = typeof(PersonResponse).GetProperties();
            foreach (PropertyInfo prop in properties)
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

        [Route("/persons/filtered")]
        public IActionResult PersonFiltered(string searchBy, string? searchString)
        {   
            ViewBag.SearchFields = this._searchField;
            List<PersonResponse> personList = new List<PersonResponse>();

            
            if(!string.IsNullOrEmpty(searchString))
            {
                personList = this._personsService.GetFilteredEntity(searchBy,searchString);
            } 
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchValue = searchString;

            return View("PersonIndex", personList);
        }

        [Route("/")]
        [Route("/persons/index")]
        public IActionResult PersonIndex([FromForm] string searchBy, [FromForm] string? searchString)
        {
            ViewBag.SearchFields = this._searchField;


            List<PersonResponse> personList = this._personsService.GetAllEntites();


            return View("PersonIndex",personList);
        }
    }
}
