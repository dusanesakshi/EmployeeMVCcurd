using System.ComponentModel.DataAnnotations;

namespace EmployeeMVCcurd.Models
{
    public class EmployeeModelView
    {
        [Key]
        public int EmpId { get; set; }
        [Required]
        public string EmpName { get; set; }
        [Required]
        public int EmpAge { get; set; }
        [Required]
        public decimal empSalary { get; set; }
        [Required]
        public string empDepartment { get; set; }
        [Required]
        public string empGender { get; set; }


    }
}
