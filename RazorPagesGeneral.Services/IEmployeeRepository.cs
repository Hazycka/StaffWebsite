using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorPagesGeneral.Models;

namespace RazorPagesGeneral.Services
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Search(string searchTerm);
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        Employee Update(Employee updatedEmployee);
        Employee Add(Employee newEmployee);
        Employee Delete(int id);
        IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept);
    }
}
