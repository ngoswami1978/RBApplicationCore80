using System.ComponentModel.DataAnnotations;

namespace RBApplicationCore80.Models
{
    public class Occupation
    {
        [Key]
        public int OccupationId { get; set; }
        public string? OccupationName { get; set; }
    }
}
