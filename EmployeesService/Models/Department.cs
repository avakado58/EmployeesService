using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesService.Models
{
    public class Department:PatchDtoBase
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
