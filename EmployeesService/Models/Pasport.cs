using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesService.Models
{
    public class Pasport:PatchDtoBase
    {
        public string Type { get; set; }
        public string Number { get; set; }
    }
}
