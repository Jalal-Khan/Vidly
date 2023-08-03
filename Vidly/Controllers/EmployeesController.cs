using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.Models;
using Vidly.Models.Domain;

namespace Vidly.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly VidlyDbContext DbContext;
        //private readonly VidlyDbContext _DbContext;

        public EmployeesController(VidlyDbContext dbContext)
        {
            this.DbContext = dbContext;
            //_DbContext= dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await DbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id=Guid.NewGuid(),
                Name= addEmployeeRequest.Name,
                Email=addEmployeeRequest.Email,
                Salary=addEmployeeRequest.Salary,
                DateOfBirth=addEmployeeRequest.DateOfBirth,
                Department=addEmployeeRequest.Department,

            };
            await DbContext.Employees.AddAsync(employee);
            await DbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
