using System.Collections.Generic;
using TasksApi.Models;
using TasksApi.Pagination;
using TasksApi.Repository.Generic;

namespace TasksApi.Repository.Task
{
    public interface ITasksRepository : IRepository<Tasks>
    {
        PagedList<Tasks> GetTasksPagination(QueryStringParameters taskParameters);
        IEnumerable<Tasks> GetTasksForName();
    } 
}