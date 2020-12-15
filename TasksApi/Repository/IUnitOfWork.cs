using TasksApi.Repository.Task;

namespace TasksApi.Repository
{
    public interface IUnitOfWork
    {
        ITasksRepository TasksRepository { get; }

        System.Threading.Tasks.Task Commit(); 
    }
}