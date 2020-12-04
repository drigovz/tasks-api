using System.Collections.Generic;
using System.Linq;
using TasksApi.Data;
using TasksApi.Models;
using TasksApi.Repository.Generic;

namespace TasksApi.Repository.Task
{
    public class TasksRepository : Repository<Tasks>, ITasksRepository
    {
        public TasksRepository(TasksContext context) : base(context)
        {
        }

        public IEnumerable<Tasks> GetTasksForName()
        {
            return Get().OrderBy(t => t.Name).ToList();
        }
    }
}