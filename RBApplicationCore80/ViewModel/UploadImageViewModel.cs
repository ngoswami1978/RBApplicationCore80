using System.ComponentModel.DataAnnotations;

namespace RBApplicationCore80.ViewModel
{
    public class UploadImageViewModel
    {
        [Required]
        [Display(Name = "Image")]
        public IFormFile? SpeakerPicture { get; set; }
    }
}
