using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesGeneral.Models;
using RazorPagesGeneral.Services;

namespace RazorPagesGeneral.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EditModel(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.employeeRepository=employeeRepository;
            this.webHostEnvironment=webHostEnvironment;
        }
        
        [BindProperty]
        public Employee Employee { get; set; }
        
        [BindProperty]
        public IFormFile? Photo { get; set; }
        [BindProperty]
        public bool Notify { get; set; }
        public string Message { get; set; }

        public IActionResult OnGet(int? id)
        {
            if(id.HasValue)
                Employee = employeeRepository.GetEmployee(id.Value);
            else
                Employee = new();
            if (Employee == null)
                return RedirectToPage("/NotFound");
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Employee.PhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", Employee.PhotoPath);
                        
                        if(Employee.PhotoPath != "noimage.png")
                            System.IO.File.Delete(filePath);
                    }
                    Employee.PhotoPath = ProcessUploadedFile();
                }

                if (Employee.Id > 0)
                {
                    Employee = employeeRepository.Update(Employee);
                    TempData["SuccessMessage"] = $"Update {Employee.Name} successful!"; // После 1 использования элемента TempData элемент стирается
                    return RedirectToPage("/Employees/Details", new { id = Employee.Id });
                }
                else
                {
                    Employee = employeeRepository.Add(Employee);
                    TempData["SuccessMessage"] = $"Adding {Employee.Name} successful!"; // После 1 использования элемента TempData элемент стирается
                    return RedirectToPage("/Employees/Employees");
                }
            }
            return Page();
        }

        public void OnPostUpdateNotificationPreferences(int id)
        {
            if (Notify)
                Message = "Thank you for turning on notification";
            else
                Message = "You have turned off email notifications";

            Employee = employeeRepository.GetEmployee(id);
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fs);
                }
            }
            return uniqueFileName;
        }
    }
}
