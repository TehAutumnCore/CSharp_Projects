using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class ContactFormModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(150)]
        public string Subject { get; set; } = string.Empty;

        [Required, StringLength(1000)]
        public string Message { get; set; } = string.Empty;
    }
}