using System;
using System.ComponentModel.DataAnnotations;

namespace For_MSBC.Models
{
    public class EmployeeViewModel
    {

        [Display(Name = "ID")]
        public int? EmpID { get; set; }
                
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "DeptName")]
        public string DeptName { get; set; }

        public Nullable<int> DeptId { get; set; }
        public virtual Dept Dept { get; set; }

    }

   
}
