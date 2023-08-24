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

        //This is add method
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
        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            ViewData["SearchString"] = searchString;
            if(DbContext.Employees==null)
            {
                return Problem("Entity set 'vidly.Context' is null.");
            }
            var employee = DbContext.Employees.ToList();
            var filteredResult = new List<Employee>();
            if (!string.IsNullOrEmpty(searchString))
            {
                filteredResult = (List<Employee>)employee.Where(e=>e.Name.Contains(searchString)||e.Email.Contains(searchString)).ToList();
            }
            return View("Index",filteredResult);              
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await DbContext.Employees.FirstOrDefaultAsync(X => X.Id == id);
            
            if(employee != null)
            {
                var viewModel = new EditEmployeeViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department,
                };
                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> View(EditEmployeeViewModel model)
        {
            var employee= await DbContext.Employees.FindAsync(model.Id);
            if(employee!=null) 
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;
                
                await DbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditEmployeeViewModel model)
        {
            var employee = await DbContext.Employees.FindAsync(model.Id);
            if(employee!=null)
            {
                DbContext.Employees.Remove(employee);
                await DbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
