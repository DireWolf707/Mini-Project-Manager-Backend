using System.ComponentModel.DataAnnotations;

namespace Mini_Project_Manager.DTOs
{
    public class CreateProjectDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }
    }

    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}