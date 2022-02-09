using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeesService.Models;
using System.Collections;

namespace EmployeesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ValuesController : ControllerBase
    {
        readonly IRepository _repository;

        public ValuesController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{CompanyId:int}")]
        public IActionResult Get(int CompanyId)
        {
            var employees = _repository.Get(CompanyId);
            if(employees.Count == 0)
                return NotFound();
            return new JsonResult(employees);
        }

        [HttpGet("{departmentName}")]
        public IActionResult Get(string departmentName)
        {
            var employees = _repository.Get(departmentName);

            if (employees.Count == 0)
                return NotFound();
            return new JsonResult(employees);
        }

        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            if (employee == null)
                return BadRequest();
            ;
            return new JsonResult(_repository.Create(employee));
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _repository.Delete(id);
            if(result == 0)
                return NotFound(); 
           
            return new JsonResult(new { result });
        }

        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, Employee employee)
        {
            if (employee == null)
                return BadRequest();

            Employee employeeFromDB = _repository.GetFerstOrDefault(id);
            if (employeeFromDB==null)
                return NotFound();
            //Проверяем, имелось ли поле с определённым именем в json запросе, если да, то устанавливаем значение из запроса, иначе не меняем
            employee.Name = employee.IsFieldPresent(nameof(employee.Name)) ? employee.Name : employeeFromDB.Name;
            employee.Surname = employee.IsFieldPresent(nameof(employee.Surname)) ? employee.Surname : employeeFromDB.Surname;
            employee.Phone = employee.IsFieldPresent(nameof(employee.Phone)) ? employee.Phone : employeeFromDB.Phone;
            employee.CompanyId = employee.IsFieldPresent(nameof(employee.CompanyId)) ? employee.CompanyId : employeeFromDB.CompanyId;
            
            if(employee.Department!=null)
            {
                employee.Department.Name = employee.Department.IsFieldPresent(nameof(employee.Department.Name)) ? employee.Department.Name : employeeFromDB.Department.Name;
                employee.Department.Phone = employee.Department.IsFieldPresent(nameof(employee.Department.Phone)) ? employee.Department.Phone : employeeFromDB.Department.Phone;
            }
            else
            {
                employee.Department = employeeFromDB.Department;
            }

            if(employee.Pasport!=null)
            {
                employee.Pasport.Type = employee.Pasport.IsFieldPresent(nameof(employee.Pasport.Type)) ? employee.Pasport.Type : employeeFromDB.Pasport.Type;
                employee.Pasport.Number = employee.Pasport.IsFieldPresent(nameof(employee.Pasport.Number)) ? employee.Pasport.Number : employeeFromDB.Pasport.Number;
            }
            else
            {
                employee.Pasport = employeeFromDB.Pasport;
            }
            
            _repository.Update(id, employee);
            return Ok();
        }
        

    }
}
