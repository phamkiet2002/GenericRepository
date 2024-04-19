using System.ComponentModel.DataAnnotations;

namespace WebTutorial.Dtos.Account
{
    public class AccountDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
