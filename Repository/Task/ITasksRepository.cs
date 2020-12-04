using System.Collections.Generic;
using TasksApi.Models;
using TasksApi.Repository.Generic;

namespace TasksApi.Repository.Task
{
    public interface ITasksRepository : IRepository<Tasks>
    {
        IEnumerable<Tasks> GetTasksForName();
    }
}