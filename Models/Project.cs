using System.ComponentModel.DataAnnotations;

namespace Mini_Project_Manager.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public List<ProjectTask> Tasks { get; set; } = [];
    }
}