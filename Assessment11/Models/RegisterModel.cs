using System.ComponentModel.DataAnnotations;

namespace Assessment11.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
