using TasksApi.Repository.Task;

namespace TasksApi.Repository
{
    public interface IUnitOfWork
    {
        ITasksRepository TasksRepository { get; }

        void Commit(); 
    }
}