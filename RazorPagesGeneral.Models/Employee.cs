using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPagesGeneral.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Fill the Name field")]
        [MaxLength(50),MinLength(2)]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please, enter a valid Email")]
        [MaxLength(50), MinLength(2)]
        public string Email { get; set; }
        public string? PhotoPath { get; set; }
        public Dept? Department { get; set; }
    }
}
