using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Tasks>> GetTasksForName()
        {
            return await Get().OrderBy(o => o.Name).ToListAsync();
        }

        public PagedList<Tasks> GetTasksPagination(QueryStringParameters taskParameters)
        {
            return PagedList<Tasks>.ToPagedList(Get().OrderBy(o => o.Name), taskParameters.Page, taskParameters.Size);
        }
    }
}