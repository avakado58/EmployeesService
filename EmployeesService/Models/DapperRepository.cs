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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                //запрос на добавление нового работника с проверкой существования департамента
                //если такого департамента не существует, он создаётся 
                string sqlQuery = $"IF NOT EXISTS (SELECT Department.Name FROM Department WHERE Department.Name LIKE N'{employee.Department.Name}') " +
                    $"BEGIN INSERT INTO Department VALUES(N'{employee.Department.Name}','{employee.Department.Phone}') END " +
                    "INSERT INTO Employee (Name, Surname, Phone, CompanyId, Department) " +
                    $"VALUES(N'{employee.Name}', N'{employee.Surname}', '{employee.Phone}', {employee.CompanyId}, N'{employee.Department.Name}')" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";

                int? employeeId = db.Query<int>(sqlQuery).FirstOrDefault();
               
                sqlQuery = $"INSERT INTO Passport VALUES({employeeId.Value}, N'{employee.Pasport.Type}', '{employee.Pasport.Number}')";
                
                db.Query(sqlQuery);
                
                return employeeId.Value;
            }
            
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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "SELECT * FROM Employee " +
                    "Join Department ON Employee.Department = Department.Name " +
                    "JOIN Passport ON Employee.Id = Passport.Id " +
                    $"WHERE Employee.Department LIKE N'{departmentName}'";
                var employee = db.Query<Employee, Department, Pasport, Employee>(sqlQuery, (employee, department, pasport) =>
                {
                    employee.Department = department;
                    employee.Pasport = pasport;
                    return employee;
                }, splitOn: "Department,Id").ToList();

                return employee;
            }
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
