using Microsoft.AspNetCore.Mvc;

namespace xTest1.Controllers
{
    [Controller]
    public class PersonController : Controller
    {

        [Route("/")]
        [Route("/persons/index")]
        public IActionResult PersonIndex()
        {
            return View("PersonIndex");
        }
    }
}
