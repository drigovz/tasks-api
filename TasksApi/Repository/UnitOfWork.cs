using TasksApi.Data;
using TasksApi.Repository.Task;

namespace TasksApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private TasksRepository _taskRepository;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext tasksContext)
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

        public async System.Threading.Tasks.Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}