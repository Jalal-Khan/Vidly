using Microsoft.AspNetCore.Mvc;

namespace Vidly.Controllers
{
    public class EmployeesController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
