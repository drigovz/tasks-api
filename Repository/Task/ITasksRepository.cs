using System.Collections.Generic;
using System.Threading.Tasks;
using TasksApi.Models;
using TasksApi.Pagination;
using TasksApi.Repository.Generic;

namespace TasksApi.Repository.Task
{
    public interface ITasksRepository : IRepository<Tasks>
    {
        PagedList<Tasks> GetTasksPagination(QueryStringParameters taskParameters);
        Task<IEnumerable<Tasks>> GetTasksForName();
    } 
}