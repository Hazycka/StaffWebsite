using Microsoft.AspNetCore.Mvc;
using RazorPagesGeneral.Models;
using RazorPagesGeneral.Services;

namespace RazorPagesGeneral.Pages.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IEmployeeRepository employeeRepository;

        public HeadCountViewComponent(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IViewComponentResult Invoke(Dept? dept = null)
        {
            var result = employeeRepository.EmployeeCountByDept(dept);
            return View(result);
        }
    }
}
