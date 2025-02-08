using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Task_Management_API.Data;
using Task_Management_API.Models;

namespace Task_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskEntryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TaskEntryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create an entry
        [HttpPost]
        public async Task<IActionResult> TaskEntry([FromBody] TaskTemplate task)
        {
            if (task == null || string.IsNullOrWhiteSpace(task.Name))
            {
                return BadRequest("Task data is invalid.");
            }

            task.Id = 0;
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // Retrieve all tasks from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskTemplate>>> GetAllEntries()
        {
            return await _context.Tasks.ToListAsync();
        }

        // Retrieve a task with a certain IDS
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskTemplate>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        // Edit Task
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEntry(int id, [FromBody] TaskTemplate obj)
        {
            if (id != obj.Id)
            {
                return BadRequest("Task ID mismatch.");
            }

            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            try
            {
                _context.Entry(existingTask).CurrentValues.SetValues(obj);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "A concurrency issue occurred.");
            }
        }

        // Delete Task
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var entry = await _context.Tasks.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(entry);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}