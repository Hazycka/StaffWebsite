using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesGeneral.Models;
using RazorPagesGeneral.Services;

namespace RazorPagesGeneral.Pages.Employees
{
    public class EmployeesModel : PageModel
    {
        private readonly IEmployeeRepository db;

        public EmployeesModel(IEmployeeRepository db)
        {
            this.db = db;
        }

        public IEnumerable<Employee> Employees { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        
        public void OnGet()
        {
            Employees = db.Search(SearchTerm);
        }
    }
}
