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
            return new JsonResult(_repository.Get(CompanyId));
        }

        [HttpGet("{departmentName}")]
        public IActionResult Get(string departmentName)
        {
            return new JsonResult(_repository.Get(departmentName));
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
        

    }
}
