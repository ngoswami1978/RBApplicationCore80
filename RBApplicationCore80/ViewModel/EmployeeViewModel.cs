using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using RBApplicationCore80.ModelData;
using RBApplicationCore80.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace RBApplicationCore80.ViewModel
{    
    public class EmployeeViewModel: Employee
    {
        
        public List<Employee> employee;
        public List<SelectListItem> occupation;
        public List<SelectListItem> role_;

        [EnumDataType(typeof(Gender))]
        public Gender gender { get; set; }

        public EmployeeViewModel() {            
            employee = new List<Employee>();
            occupation = new List<SelectListItem>();
            role_ = new List<SelectListItem>();
        }
    }
}
