using System.Collections.Generic;


namespace EmployeesService.Models
{
    public abstract class PatchDtoBase
    {
        private HashSet<string> PropsInHttpRequest { get; set; } = new HashSet<string>();

        public bool IsFieldPresent(string propertyName)
        {
            return PropsInHttpRequest.Contains(propertyName.ToLowerInvariant());
        }

        public void SetHasProp(string propertyName)
        {
            PropsInHttpRequest.Add(propertyName.ToLowerInvariant());
        }
    }
}
