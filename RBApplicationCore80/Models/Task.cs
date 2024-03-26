using System.ComponentModel.DataAnnotations;

namespace RBApplicationCore80.Models
{
    public class Task
    {
        public string? clientName { get; set; }
        public string? taskType { get; set; }
                
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? workingHr { get; set; }

        public List<Employee>? EmpList { get; set; }


    }
}
