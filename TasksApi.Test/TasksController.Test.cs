using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasksApi.Controllers;
using TasksApi.Data;
using TasksApi.DTOs.Mappings;
using TasksApi.Repository;

namespace TasksApi.Test
{
    public class TasksControllerTest
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public static DbContextOptions<AppDbContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=DESKTOP-8SL6PE8; initial catalog=TasksDB; user id=sa; password=sa12345; Integrated Security=True";

        static TasksControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connectionString)
            .Options;
        }

        public TasksControllerTest(ILogger<TasksController> logger)
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            });

            _mapper = config.CreateMapper();
            var context = new AppDbContext(dbContextOptions);

            //DbUnitTestsMockInitializer db = new DbUnitTestsMockInitializer();
            //db.Seed(context);  

            _uof = new UnitOfWork(context);
        }

        // implementação dos testes 
    }
}