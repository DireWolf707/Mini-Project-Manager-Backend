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
    [Route("api/projects")]
    public class ProjectController(ApplicationDbContext _context) : ControllerBase
    {
        private readonly ApplicationDbContext db = _context;

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            var userId = GetUserId();
            var projects = await db.Projects
                .Where(p => p.UserId == userId)
                .Select(p => new ProjectDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    CreationDate = p.CreationDate
                })
                .ToListAsync();

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(int id)
        {
            var userId = GetUserId();
            var project = await db.Projects
                .Where(p => p.Id == id && p.UserId == userId)
                .Select(p => new ProjectDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    CreationDate = p.CreationDate,
                })
                .FirstOrDefaultAsync();

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDTO projectDTO)
        {
            var userId = GetUserId();
            var project = new Project
            {
                Title = projectDTO.Title,
                Description = projectDTO.Description,
                UserId = userId
            };

            db.Projects.Add(project);
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var userId = GetUserId();
            var rowsAffected = await db.Projects
                    .Where(p => p.Id == id && p.UserId == userId)
                    .ExecuteDeleteAsync();

            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }
    }
}