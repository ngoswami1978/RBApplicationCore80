using System.ComponentModel.DataAnnotations;

namespace RBApplicationCore80.ViewModel
{
    public class SpeakerViewModel : EditImageViewModel
    {
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
        public byte[]? AadharNumber { get; set; }

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
