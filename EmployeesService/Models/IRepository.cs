using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesService.Models
{
    public interface IRepository
    {
        int Create(Employee employee);
        int Delete(int id);
        List<Employee> Get(int CompanyId);
        List<Employee> Get(string DepartmentName);
        void Update(Employee employee);
    }
}
