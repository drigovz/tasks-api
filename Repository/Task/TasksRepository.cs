using System.Collections.Generic;
using System.Linq;
using TasksApi.Data;
using TasksApi.Models;
using TasksApi.Pagination;
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
            throw new System.NotImplementedException();
        }

        public PagedList<Tasks> GetTasksPagination(TaskParameters taskParameters)
        {
            return PagedList<Tasks>.ToPagedList(Get().OrderBy(o => o.Name), taskParameters.Page, taskParameters.Size);
        }
    }
}