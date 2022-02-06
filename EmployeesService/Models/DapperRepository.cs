using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EmployeesService.Models
{
    public class DapperRepository : IRepository
    {
        readonly string _connectionString = null;

        public DapperRepository (string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Create(Employee employee)
        {
            throw new NotImplementedException();
        }       

        public List<Employee> Get(int companyId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "SELECT * FROM Employee " +
                    "Join Department ON Employee.Department = Department.Name " +
                    "JOIN Passport ON Employee.Id = Passport.Id " +
                    $"WHERE Employee.CompanyId = {companyId}";
                var employee = db.Query<Employee, Department,Pasport, Employee>(sqlQuery, (employee, department, pasport) => 
                {
                    employee.Department = department;
                    employee.Pasport = pasport;
                    return employee;
                }, splitOn: "Department,Id").ToList();

                return employee;
            }
        }

        public List<Employee> Get(string departmentName)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
