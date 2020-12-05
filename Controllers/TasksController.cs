using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using TasksApi.DTOs;
using TasksApi.Models;
using TasksApi.Repository;

namespace TasksApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TasksController(IUnitOfWork uof, ILogger<TasksController> logger, IMapper mapper)
        {
            _uof = uof;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TasksDTO>> GetAll()
        {
            try
            {
                _logger.LogInformation(" ########## GET api/tasks ########## ");
                var tasks = _uof.TasksRepository.Get().ToList();
                var tasksDTO = _mapper.Map<List<TasksDTO>>(tasks);
                return tasksDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar acessar o servidor");
            }
        }

        [HttpGet("listforname")]
        public ActionResult<IEnumerable<TasksDTO>> GetTasksForName()
        {
            var task = _uof.TasksRepository.GetTasksForName().ToList();
            var tasksDTO = _mapper.Map<List<TasksDTO>>(task);
            return tasksDTO;
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
                {
                    var taskDTO = _mapper.Map<Tasks>(task);
                    return new ObjectResult(taskDTO);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar acessar o servidor");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] TasksDTO taskDTO)
        {
            try
            {
                if (taskDTO == null)
                    return BadRequest();

                var task = _mapper.Map<Tasks>(taskDTO);
                _uof.TasksRepository.Add(task);
                _uof.Commit();

                var resultTaskDTO = _mapper.Map<TasksDTO>(task);

                return CreatedAtRoute("GetTask", new { id = resultTaskDTO.Id }, resultTaskDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar criar uma nova tarefa!");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([BindRequired] int id, [FromBody] TasksDTO objDTO)
        {
            try
            {
                if (objDTO == null || objDTO.Id != id)
                    return BadRequest($"Não foi possível atualizar a tarefa com código {id}");

                var task = _mapper.Map<Tasks>(objDTO);
                _uof.TasksRepository.Update(task);
                _uof.Commit();

                return new OkResult();
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