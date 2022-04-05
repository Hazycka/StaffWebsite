using Microsoft.EntityFrameworkCore;
using RazorPagesGeneral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPagesGeneral.Services
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context=context;
        }

        public Employee Add(Employee newEmployee)
        {
            //context.Employees.Add(newEmployee);
            //context.SaveChanges();
            context.Database.ExecuteSqlRaw("spAddNewEmployee {0}, {1}, {2}, {3}", 
                                            newEmployee.Name, 
                                            newEmployee.Email, 
                                            newEmployee.PhotoPath, 
                                            newEmployee.Department);

            return newEmployee;
        }

        public Employee Delete(int id)
        {
            var employeeToDelete = context.Employees.Find(id);
            if (employeeToDelete != null)
            {
                context.Employees.Remove(employeeToDelete);
                context.SaveChanges();
            }
            return employeeToDelete;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = context.Employees;

            if (dept.HasValue)
                query = query.Where(x => x.Department == dept.Value);

            return query.GroupBy(x => x.Department)
                                .Select(x => new DeptHeadCount()
                                {
                                    Department = x.Key.Value,
                                    Count = x.Count()
                                }).ToList();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            //return context.Employees;
            return context.Employees
                          .FromSqlRaw<Employee>("SELECT * FROM Employees").ToList();
        }

        public Employee GetEmployee(int id)
        {
            //return context.Employees.Find(id);
            return context.Employees
                          .FromSqlRaw<Employee>("CodeFirstSpGetSpGetEmployeeById {0}", id)
                          .ToList().FirstOrDefault();
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (String.IsNullOrWhiteSpace(searchTerm))
                return context.Employees;
            return context.Employees.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())
                                            || x.Email.ToLower().Contains(searchTerm.ToLower()));
        }

        public Employee Update(Employee updatedEmployee)
        {
            var employee = context.Employees.Attach(updatedEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedEmployee;
        }
    }
}
