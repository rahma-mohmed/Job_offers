using Microsoft.Build.Framework;

namespace Job_offers.Models
{
    public class ContactModel
    {
        [Required]
        public string Name {  get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
