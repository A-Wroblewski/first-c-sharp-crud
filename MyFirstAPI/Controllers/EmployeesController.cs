using Microsoft.AspNetCore.Mvc;
using MyFirstAPI.Data;
using MyFirstAPI.Models;
using MyFirstAPI.Models.Entities;

namespace MyFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public EmployeesController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(dbContext.Employees.ToList());
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            Employee employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
            };

            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();

            return Ok(employeeEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;

            dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
