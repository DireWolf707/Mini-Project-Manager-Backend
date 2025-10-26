using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_Project_Manager.Data;
using Mini_Project_Manager.DTOs;
using Mini_Project_Manager.Models;

namespace Mini_Project_Manager.Controllers
{
    [Authorize]
    [ApiController]
    public class TaskController(ApplicationDbContext _context) : ControllerBase
    {
        private readonly ApplicationDbContext db = _context;

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        [HttpPost("api/projects/{projectId}/tasks")]
        public async Task<IActionResult> CreateTask(int projectId, [FromBody] CreateProjectTaskDTO taskDTO)
        {
            var userId = GetUserId();

            var projectExists = await db.Projects
                .AnyAsync(p => p.Id == projectId && p.UserId == userId);

            if (!projectExists)
                return NotFound(new { Message = "Project not found." });

            var task = new ProjectTask
            {
                Title = taskDTO.Title,
                DueDate = taskDTO.DueDate,
                ProjectId = projectId,
                IsCompleted = false
            };

            db.Tasks.Add(task);
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("api/tasks/{taskId}")]
        public async Task<IActionResult> UpdateTask(int taskId, [FromBody] UpdateProjectTaskDTO taskDto)
        {
            var userId = GetUserId();
            var task = await db.Tasks
                            .FirstOrDefaultAsync(t => t.Id == taskId && t.Project.UserId == userId);

            if (task == null)
                return NotFound();

            task.Title = taskDto.Title;
            task.DueDate = taskDto.DueDate;
            task.IsCompleted = taskDto.IsCompleted;

            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("api/tasks/{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var userId = GetUserId();

            var task = await db.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId && t.Project.UserId == userId);

            if (task == null)
                return NotFound();

            db.Tasks.Remove(task);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}