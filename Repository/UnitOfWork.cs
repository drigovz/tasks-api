using TasksApi.Data;
using TasksApi.Repository.Task;

namespace TasksApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private TasksRepository _taskRepository;
        public TasksContext _context;

        public UnitOfWork(TasksContext tasksContext)
        {
            _context = tasksContext;
        }

        public ITasksRepository TasksRepository
        {
            get
            {
                return _taskRepository = _taskRepository ?? new TasksRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}