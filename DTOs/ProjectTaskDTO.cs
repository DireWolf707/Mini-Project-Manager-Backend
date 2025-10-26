using System.ComponentModel.DataAnnotations;

namespace Mini_Project_Manager.DTOs
{
    public class CreateProjectTaskDTO
    {
        [Required]
        public string Title { get; set; } = null!;
        public DateTime? DueDate { get; set; }
    }

    public class UpdateProjectTaskDTO
    {
        [Required]
        public string Title { get; set; } = null!;
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class ProjectTaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int ProjectId { get; set; }
    }
}