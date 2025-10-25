using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Mini_Project_Manager.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}