using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeesService.Models;
using System.Collections;

namespace EmployeesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IRepository _repository;

        public ValuesController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{CompanyId}")]
        public IEnumerable Get(int CompanyId)
        {
            return _repository.Get(CompanyId);
        }

    }
}
