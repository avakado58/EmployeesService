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
        [HttpGet("{CompanyId?}")]
        public IActionResult Get(int CompanyId)
        {
            return new JsonResult(_repository.Get(CompanyId));
        }
        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            if (employee == null)
                return BadRequest();
            ;
            return new JsonResult(_repository.Create(employee));
        }
        

    }
}
