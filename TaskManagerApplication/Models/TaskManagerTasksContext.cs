using Microsoft.EntityFrameworkCore;

namespace AvanadeTaskManagerApplication.Models
{
    public class TaskManagerTasksContext:DbContext
    {
        public TaskManagerTasksContext(DbContextOptions<TaskManagerTasksContext> options) : base(options)
        {
                
        }

        public DbSet<TaskManagerTasks> TaskManagerTasks { get; set; }
    }
}
