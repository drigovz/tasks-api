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
        public TasksRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Tasks>> GetTasksForName()
        {
            return await Get().OrderBy(o => o.Name).ToListAsync();
        }

        public async Task<PagedList<Tasks>> GetTasksPaginationAsync(QueryStringParameters taskParameters)
        {
            return await PagedList<Tasks>.ToPagedListAsync(Get().OrderBy(o => o.Name), taskParameters.Page, taskParameters.Size);
        }
    }
}