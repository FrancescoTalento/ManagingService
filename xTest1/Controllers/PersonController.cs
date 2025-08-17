using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.Person;

namespace xTest1.Controllers
{
    [Controller]
    public class PersonController : Controller
    {
        private readonly IPersonsService _personsService;

        public PersonController(IPersonsService personsService)
        {
            this._personsService = personsService;
        }

        [Route("/")]
        [Route("/persons/index")]
        public IActionResult PersonIndex()
        {
            List<PersonResponse> personList = this._personsService.GetAllEntites();

            return View("PersonIndex",personList);
        }
    }
}
