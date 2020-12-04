using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using TasksApi.Models;
using TasksApi.Repository;

namespace TasksApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly UnitOfWork _uof;
        private readonly ILogger _logger;

        public TasksController(UnitOfWork uof, ILogger<TasksController> logger)
        {
            _uof = uof;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tasks>> GetAll()
        {
            try
            {
                _logger.LogInformation(" ########## GET api/tasks ########## ");
                return _uof.TasksRepository.Get().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar acessar o servidor");
            }
        }

        [HttpGet("listforname")]
        public ActionResult<IEnumerable<Tasks>> GetTasksForName()
        {
            return _uof.TasksRepository.GetTasksForName().ToList();
        }

        [HttpGet("{id}", Name = "GetTask")]
        public IActionResult GetTask([BindRequired] int id)
        {
            try
            {
                var task = _uof.TasksRepository.GetById(t => t.Id == id);
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
        public IActionResult Create([FromBody] Tasks task)
        {
            try
            {
                if (task == null)
                    return BadRequest();

                _uof.TasksRepository.Add(task);
                _uof.Commit();

                return CreatedAtRoute("GetTask", new { id = task.Id }, task);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar criar uma nova tarefa!");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([BindRequired] int id, [FromBody] Tasks obj)
        {
            try
            {
                if (obj == null || obj.Id != id)
                    return BadRequest($"Não foi possível atualizar a tarefa com código {id}");

                _uof.TasksRepository.Update(obj);
                _uof.Commit();

                return new NoContentResult();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar atualizar uma tarefa!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([BindRequired] int id)
        {
            try
            {
                Tasks task = _uof.TasksRepository.GetById(t => t.Id == id);
                if (task == null)
                    return NotFound($"Não foi possível localizar a tarefa com código {id}");

                _uof.TasksRepository.Delete(task);
                _uof.Commit();

                return new NoContentResult();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir a tarefa com código {id}!");
            }
        }
    }
}