using System.ComponentModel.DataAnnotations;
namespace Mini_Project_Manager.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}