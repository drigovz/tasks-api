using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasksApi.Data;
using TasksApi.Models;

namespace TasksApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TasksContext _context;
        private readonly ILogger _logger;

        public TasksController(TasksContext context, ILogger<TasksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation(" ########## GET api/tasks ########## ");
                return await _context.Tasks.AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar acessar o servidor");
            }
        }

        [HttpGet("{id}", Name = "GetTask")]
        public async Task<IActionResult> GetTaskAsync([BindRequired] int id)
        {
            try
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
                if (task == null)
                    return NotFound($"A categoria de código {id} não foi encontrada!");
                else
                    return new ObjectResult(task);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar acessar o servidor");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Tasks task)
        {
            try
            {
                if (task == null)
                    return BadRequest();

                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();

                return CreatedAtRoute("GetTask", new { id = task.Id }, task);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar criar uma nova tarefa!");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([BindRequired] int id, [FromBody] Tasks obj)
        {
            try
            {
                if (obj == null || obj.Id != id)
                    return BadRequest($"Não foi possível atualizar a tarefa com código {id}");

                Tasks task = _context.Tasks.FirstOrDefault(t => t.Id == id);
                if (task == null)
                    return NotFound($"Não foi possível localizar a tarefa com código {id}");

                task.IsCompleted = obj.IsCompleted;
                task.Name = obj.Name;

                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();

                return new NoContentResult();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar atualizar uma tarefa!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([BindRequired] int id)
        {
            try
            {
                Tasks task = _context.Tasks.FirstOrDefault(t => t.Id == id);
                if (task == null)
                    return NotFound($"Não foi possível localizar a tarefa com código {id}");

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

                return new NoContentResult();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir a tarefa com código {id}!");
            }
        }
    }
}