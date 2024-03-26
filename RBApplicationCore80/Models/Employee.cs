using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBApplicationCore80.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; } 
        public string? Address { get; set; }
        public string? City { get; set; }
        public int State { get; set; }
        public string? Country { get; set; }        
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailAddress { get; set; }
        public int Occupation { get; set; }
        public DateTime Dob { get; set; }                       
        public string? role { get; set; }

        [NotMapped]
        public string? OccupationName { get; set; }
        [NotMapped]
        public string? GenderName { get; set; }
        [NotMapped]
        public string? RoleName { get; set; }       
    }
}
