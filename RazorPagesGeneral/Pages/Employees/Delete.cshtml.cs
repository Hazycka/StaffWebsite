using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesGeneral.Models;
using RazorPagesGeneral.Services;

namespace RazorPagesGeneral.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DeleteModel(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.employeeRepository=employeeRepository;
            this.webHostEnvironment=webHostEnvironment;
        }

        [BindProperty]
        public Employee Employee { get; set; }
        
        public IActionResult OnGet(int id)
        {
            Employee = employeeRepository.GetEmployee(id);
            if (Employee == null)
                return RedirectToPage("/NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            Employee deletedEmployee = employeeRepository.Delete(Employee.Id);

            if (deletedEmployee == null)
                return RedirectToPage("/NotFound");

            if (deletedEmployee.PhotoPath != null)
            {
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", deletedEmployee.PhotoPath);

                if (deletedEmployee.PhotoPath != "noimage.png")
                    System.IO.File.Delete(filePath);
            }
            return RedirectToPage("/Employees/Employees");
        }
    }
}
