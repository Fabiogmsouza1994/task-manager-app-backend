using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AvanadeTaskManagerApplication.Models;

namespace AvanadeTaskManagerApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagerController : ControllerBase
    {
        private readonly TaskManagerTasksContext _context;

        public TaskManagerController(TaskManagerTasksContext context)
        {
            _context = context;
        }

        // GET: api/TaskManager
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskManagerTasks>>> GetTaskManagerTasks()
        {
            var taskManagerTasks = await _context.TaskManagerTasks.ToListAsync();

            if (taskManagerTasks == null)
            {
                return NotFound();
            }

            return taskManagerTasks;
        }

        // GET: api/TaskManager/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskManagerTasks>> GetTaskManagerTasks(int id)
        {
            var taskManagerTasks = await _context.TaskManagerTasks.FindAsync(id);

            if (taskManagerTasks == null)
            {
                return NotFound();
            }

            return taskManagerTasks;
        }

        // PUT: api/TaskManager/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskManagerTasks(int id, TaskManagerTasks taskManagerTasks)
        {
            if (id != taskManagerTasks.id)
            {
                return BadRequest();
            }

            _context.Entry(taskManagerTasks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskManagerTasksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaskManager
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskManagerTasks>> PostTaskManagerTasks(TaskManagerTasks taskManagerTasks)
        {
           
            if(_context.TaskManagerTasks == null)
            {
                return Problem("Entity set is null");
            };

            _context.TaskManagerTasks.Add(taskManagerTasks);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTaskManagerTasks", new { id = taskManagerTasks.id }, taskManagerTasks);
        }

        // DELETE: api/TaskManager/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskManagerTasks(int id)
        {
            var taskManagerTasks = await _context.TaskManagerTasks.FindAsync(id);
            if (taskManagerTasks == null)
            {
                return NotFound();
            }

            _context.TaskManagerTasks.Remove(taskManagerTasks);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskManagerTasksExists(int id)
        {
            return _context.TaskManagerTasks.Any(e => e.id == id);
        }
    }
}
