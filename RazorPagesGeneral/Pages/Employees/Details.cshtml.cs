using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesGeneral.Models;
using RazorPagesGeneral.Services;

namespace RazorPagesGeneral.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly IEmployeeRepository employeeRepository;

        public DetailsModel(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository=employeeRepository;
        }

        public Employee Employee { get; private set; }
        public IActionResult OnGet(int id)
        {
            Employee = employeeRepository.GetEmployee(id);

            if(Employee == null)
                return RedirectToPage("/NotFound");
            
            return Page();
        }
    }
}
