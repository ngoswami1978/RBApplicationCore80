using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBApplicationCore80.Models
{
    public class SalesLeadEntity 
    {
        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Mobile { get; set; }
        [Required]
        public string? Email { get; set; }

        public string? Source { get; set; }
        [Display(Name = "PanImage")]
        public string? PanNumber { get; set; }

        [Display(Name = "AadharImage")]
        public string? AadharNumber { get; set; }

        [Display(Name = "UserImage")]
        public string? UserImage { get; set; }


        [Required]
        [Display(Name = "Name")]
        public string? SpeakerName { get; set; }

        [Required]
        public string? Qualification { get; set; }

        [Required]
        public int Experience { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime SpeakingDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Time")]
        public DateTime SpeakingTime { get; set; }

        [Required]
        public string? Venue { get; set; }

    }
}
