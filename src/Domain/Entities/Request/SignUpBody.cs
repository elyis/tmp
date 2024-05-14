using System.ComponentModel.DataAnnotations;

namespace tmp.src.Domain.Entities.Request
{
    public class SignUpBody
    {
        // [EmailAddress]
        // [Required]
        public string Email { get; set; }

        // [Required]
        public string Password { get; set; }
    }
}